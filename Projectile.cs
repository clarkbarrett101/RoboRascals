using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : HitBox
{
    public GameObject expolsion;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == tag)
        {
            return;
        }
       GameObject ex = Instantiate(expolsion, transform.position, Quaternion.identity);
       HitBox hitBox = ex.GetComponent<HitBox>();
       hitBox.dmg = dmg;
       hitBox.hitPush = hitPush;
       hitBox.recoil = recoil;
       hitBox.attacker = attacker;
       Destroy(ex,1);
       Destroy(gameObject);
    }

}