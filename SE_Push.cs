using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SE_Push : I_StatusEffect
{
    public Vector2 direction;
    public float distance;
    public float speed;

    public override void ApplyEffect(I_Actor actor)
    {
    }

    public override void UpdateEffect(I_Actor actor)
    {
        pawn = actor.gameObject;
        distance -= speed * Time.deltaTime;
        if (distance <= 0)
        {
            RemoveEffect(actor);
        }
    }
    public override void RemoveEffect(I_Actor actor)
    {
        pawn = actor.gameObject;
        pawn.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        Destroy(this);
    }
}