using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public float dmg =1;
    public I_Actor attacker;
    public float hitPush;
    public float recoil;
    public void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Hit");
        if (collision.gameObject.tag == tag)
        {
            return;
        }
        if (collision.gameObject.TryGetComponent<I_Actor>(out I_Actor actor))
        {
            actor.ApplyDamage(attacker, dmg);
            actor.PushActor( attacker.transform.forward * hitPush);
            attacker.PushActor( attacker.transform.forward * recoil);
            attacker.OnHit(actor);
        }
    }
}