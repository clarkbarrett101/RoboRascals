using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public A_Robot actor;
    Vector2 move;
    public float maxAccel = 1.5f;
    public float turnSpeed = 10;
    public Animator weaponAnimA;
    public Animator weaponAnimB;
    public Animator weaponAnimC;
    public float jetCooldown = 1;
    public float jetTimer = 0;
    public float jetForce = 1;
    void Start()
    {
        actor = GetComponent<A_Robot>();
        CamManager.players.Add(transform);
    }

    // Update is called once per frame
    void Update()
    {
        if(actor.dead)
        {
            return;
        }
        if(jetTimer > 0)
            jetTimer -= Time.deltaTime;
        if (actor.moveLock <= 0)
        {
            if (move.magnitude > 0)
            {
                actor.anim.SetBool("Walking", true);

                Vector3 target =  new Vector3(move.x, 0, move.y)*(actor.moveAccel * actor.moveSpeed * Time.deltaTime);
                if(!Physics.Raycast(transform.position, target, out RaycastHit hit, target.magnitude, LayerMask.GetMask("Wall")))
                {
                    transform.position += target;
                }

                if (actor.moveAccel <= maxAccel)
                {
                    actor.moveAccel += Time.deltaTime;
                }

                Vector3 lookDir = Quaternion.LookRotation(new Vector3(move.x, 0, move.y)).eulerAngles;
                float lookDiff = lookDir.y - transform.eulerAngles.y;
                if (lookDiff > 180)
                {
                    lookDiff -= 360;
                }

                transform.eulerAngles += new Vector3(0, lookDiff, 0) * (Time.deltaTime * turnSpeed);
            }
            else
            {
                actor.anim.SetBool("Walking", false);
                actor.moveAccel = 1;
            }
        }
    }

    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }
    public void OnAttackA()
    {
        if (actor.dead)
        {
            return;
        }
        if(actor.rcooldown > 0)
        {
            return;
        }
        if(actor.moveLock <= 0 && actor.attackLock <= 0)
        {
            actor.AttackLock(1);
            actor.anim.SetTrigger("RAttack");
            weaponAnimA.SetTrigger("Attack");
            actor.rcooldown = actor.rightCWeapon.cooldown;
            actor.attackType = A_Robot.AttackType.Right;
            actor.rightCWeapon.Anticipation(actor);
        }
    }
    public void OnAttackB()
    {
        if (actor.dead)
        {
            return;
        }
        if (actor.lcooldown > 0)
        {
            return;
        }
        if (actor.moveLock <= 0 && actor.attackLock <= 0)
        {
            actor.AttackLock(1);
            actor.anim.SetTrigger("LAttack");
            weaponAnimB.SetTrigger("Attack");
            actor.lcooldown = actor.leftCWeapon.cooldown;
            actor.attackType = A_Robot.AttackType.Left;
            actor.leftCWeapon.Anticipation(actor);
        }
    }
    public void SpinLock()
    {
        actor.MoveLock(.5f);
    }
    public void AttackAction()
    {
        switch (actor.attackType)
        {
            case A_Robot.AttackType.Right:
                actor.rightCWeapon.Action(actor);
                break;
            case A_Robot.AttackType.Left:
                actor.leftCWeapon.Action(actor);
                break;
            case A_Robot.AttackType.Gun:
                actor.cGun.Action(actor);
                break;
        }
    }
    public void OnJet()
    {
        if (actor.dead)
        {
            return;
        }
        if(jetTimer > 0 || actor.moveLock > 0 || actor.attackLock > 0)
        {
            return;
        }
        jetTimer = jetCooldown;
        actor.anim.SetTrigger("Jet");
        actor.PushActor(transform.forward * jetForce);
    }
   /* public void OnAttackC()
    {
        if (actor.gcooldown > 0)
        {
            return;
        }
        if (actor.moveLock <= 0 && actor.attackLock <= 0)
        {
            actor.AttackLock(1.5f);
            actor.anim.SetTrigger("Shoot");
            weaponAnimC.SetTrigger("Attack");
            actor.gcooldown = actor.cGun.cooldown;
            actor.attackType = A_Robot.AttackType.Gun;
            actor.cGun.Anticipation(actor);
        }
    }

    public void OnStart()
    {

    }
    */
}