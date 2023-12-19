using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu( menuName = "Component")]
public class C_Weapon : I_Component
{
    public float dmg;
    public float cooldown;
    public int hitCount;
    public float hitPush;
    public float recoil;
    public float momentum;
}