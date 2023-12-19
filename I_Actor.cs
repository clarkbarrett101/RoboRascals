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
    public GameObject sparks;
    public List<I_StatusEffect> statusEffects = new List<I_StatusEffect>();
    public List<GameObject> effectPawns = new List<GameObject>();
    public Vector2 push;
    public virtual void Awake()
    {
        anim = anim ? anim : GetComponent<Animator>();
    }

    public virtual void ApplyDamage(I_Actor attacker, float dmg)
    {
        chp -= dmg;
    }
    public virtual void ApplyStatusEffect(I_StatusEffect effect)
    {
        if(statusEffects.Contains(effect))
        {
            statusEffects.Find(x => x.name == effect.name).duration = effect.duration;
            return;
        }

        effectPawns.Add(Instantiate(effect.pawn, transform.position, Quaternion.identity));
        effect.ApplyEffect(this);
        statusEffects.Add(effect);
    }
    public virtual void Update()
    {
        Vector3 pushMod = new Vector3(push.x*2, 0, push.y*2)*Time.deltaTime;
        transform.position += pushMod;
        push = new Vector2(push.x-pushMod.x, push.y-pushMod.z);
        if(push.magnitude < .1f)
        {
            push = Vector2.zero;
        }
        foreach(I_StatusEffect effect in statusEffects)
        {
            effect.UpdateEffect(this);
            effect.duration -= Time.deltaTime;
            if(effect.duration <= 0)
            {
                effect.RemoveEffect(this);
                effectPawns.RemoveAt( statusEffects.FindIndex(x => x.name == effect.name));
                statusEffects.Remove(effect);
            }
        }
    }
    public void PushActor(Vector2 direction)
    {
        push += direction;
    }

    public virtual void Die()
    {

    }
}