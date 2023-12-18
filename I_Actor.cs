using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_Actor : MonoBehaviour
{
    public float mhp;
    public float chp;
    public float moveSpeed;
    public float moveAccel;
    public Animator anim;

    private void Awake()
    {
        anim = anim ? anim : GetComponent<Animator>();
    }

    public virtual void ApplyDamage(I_Actor attacker, float dmg)
    {
        chp -= dmg;
    }

    public virtual void Die()
    {

    }
}