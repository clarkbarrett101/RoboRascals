using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_StatusEffect : ScriptableObject
{
    public float duration;
    public GameObject pawn;
    public virtual void ApplyEffect(I_Actor actor)
    {

    }
    public virtual void UpdateEffect(I_Actor actor)
    {

    }
    public virtual void RemoveEffect(I_Actor actor)
    {

    }
}