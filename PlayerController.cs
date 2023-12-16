using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public I_Actor actor;
    Vector2 move;
    public float maxAccel = 1.5f;
    public float turnSpeed = 10;
    float locktimer = 0;
    public Animator drillA;
    public Animator drillB;
    void Start()
    {
        actor = GetComponent<I_Actor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (locktimer > 0)
        {
            locktimer -= Time.deltaTime;
        }
        else if(move.magnitude > 0)
        {
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
            actor.moveAccel = 1;
        }
    }

    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }
    public void OnAttackA()
    {
        if(locktimer <= 0)
        {
            actor.anim.SetTrigger("RAttack");
            drillA.SetTrigger("Attack");
        }
    }
    public void OnAttackB()
    {
        if (locktimer <= 0)
        {
            actor.anim.SetTrigger("LAttack");
            drillB.SetTrigger("Attack");
        }
    }
    public void SpinLock()
    {
        locktimer = 0.5f;
    }
}