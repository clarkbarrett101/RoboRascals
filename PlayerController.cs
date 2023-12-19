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
    float locktimer = 0;
    float attackLock = 0;
    public Animator weaponAnimA;
    public Animator weaponAnimB;
    void Start()
    {
        actor = GetComponent<A_Robot>();
    }

    // Update is called once per frame
    void Update()
    {
        if(attackLock > 0)
        {
            attackLock -= Time.deltaTime;
        }
        if (locktimer > 0)
        {
            locktimer -= Time.deltaTime;
        }
        else if(move.magnitude > 0)
        {
            actor.anim.SetBool("Walking", true);
            transform.position += new Vector3(move.x, 0, move.y) * (actor.moveAccel * actor.moveSpeed * Time.deltaTime);
            if(actor.moveAccel <= maxAccel)
            {
               actor.moveAccel += Time.deltaTime;
            }
            Vector3 lookDir = Quaternion.LookRotation(new Vector3(move.x, 0, move.y)).eulerAngles;
            float lookDiff = lookDir.y - transform.eulerAngles.y;
            if(lookDiff > 180)
            {
                lookDiff -= 360;
            }
            transform.eulerAngles += new Vector3(0,lookDiff,0) * (Time.deltaTime * turnSpeed);
        }else
        {
            actor.anim.SetBool("Walking", false);
            actor.moveAccel = 1;
        }
    }

    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }
    public void OnAttackA()
    {
        if(actor.rcooldown > 0)
        {
            return;
        }
        if(locktimer <= 0 && attackLock <= 0)
        {
            attackLock = 1.5f;
            actor.anim.SetTrigger("RAttack");
            weaponAnimA.SetTrigger("Attack");
            actor.rcooldown = actor.rightCWeapon.cooldown;
        }
    }
    public void OnAttackB()
    {
        if (actor.lcooldown > 0)
        {
            return;
        }
        if (locktimer <= 0 && attackLock <= 0)
        {
            attackLock = 1.5f;
            actor.anim.SetTrigger("LAttack");
            weaponAnimB.SetTrigger("Attack");
            actor.lcooldown = actor.leftCWeapon.cooldown;
        }
    }
    public void SpinLock()
    {
        locktimer = 0.5f;
    }
}