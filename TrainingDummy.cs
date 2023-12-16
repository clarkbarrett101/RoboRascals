using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingDummy : I_Actor
{

    public override void ApplyDamage(I_Actor attacker, float dmg)
    {
        base.ApplyDamage(attacker, dmg);
        anim.SetTrigger("Hit");
    }
}