using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class C_Weapon : I_Component
{
    public float dmg;
    public float cooldown;
    public int hitCount;
    public float hitPush;
    public float recoil;
    public virtual void Anticipation(A_Robot robot)
    {

    }

    public virtual void Action(A_Robot robot)
    {

    }

    public virtual void Recovery(A_Robot robot)
    {

    }

    public virtual void OnHit(I_Actor target)
    {

    }
}