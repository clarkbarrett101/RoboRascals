using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu( menuName = "EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public float mhp;
    public float attackDamage;
    public float moveSpeed;
    public float attackRange;
    public float attackCooldown;
}