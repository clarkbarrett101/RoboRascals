using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
   public HitBox hitBox;
   public C_Weapon component;
   public I_Actor owner;
   private void Start()
   {
      hitBox = GetComponentInChildren<HitBox>();
      hitBox.dmg = component.dmg/ component.hitCount;
      hitBox.attacker = owner;
   }
}