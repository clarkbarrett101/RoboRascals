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
    public Vector3 push;
    public float attackLock;
    public float moveLock;

    public virtual void Awake()
    {
        anim = anim ? anim : GetComponent<Animator>();
    }

    public virtual void ApplyDamage(I_Actor attacker, float dmg)
    {
        chp -= dmg;
        attacker.StartCoroutine(HitStop());
    }

    public virtual void ApplyStatusEffect(I_StatusEffect effect)
    {
        if (statusEffects.Contains(effect))
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
        if(moveLock > 0)
            moveLock -= Time.deltaTime;
        if(attackLock > 0)
            attackLock -= Time.deltaTime;
        if (push.magnitude > .1f)
        {
            Vector3 pushMod = new Vector3(push.x * 2, 0, push.z * 2) * Time.deltaTime;
            if(!Physics.Raycast(transform.position, pushMod, pushMod.magnitude, LayerMask.GetMask("Wall")))
            {transform.position += pushMod;
            push = new Vector3(push.x - pushMod.x, 0, push.z - pushMod.z);
        }}
        else
        {
            push = Vector3.zero;
        }

        foreach (I_StatusEffect effect in statusEffects)
        {
            effect.UpdateEffect(this);
            effect.duration -= Time.deltaTime;
            if (effect.duration <= 0)
            {
                effect.RemoveEffect(this);
                effectPawns.RemoveAt(statusEffects.FindIndex(x => x.name == effect.name));
                statusEffects.Remove(effect);
            }
        }
    }

    public void PushActor(Vector3 direction)
    {
        push += direction;
    }

    public virtual void Die()
    {
    }

    public virtual void OnHit(I_Actor target)
    {
    }
    public void AttackLock(float time)
    {
        if(attackLock < time)
            attackLock = time;
    }
    public void MoveLock(float time)
    {
        if(moveLock < time)
            moveLock = time;
    }
    IEnumerator HitStop()
    {
        Time.timeScale = .25f;
        yield return new WaitForSecondsRealtime(.1f);
        Time.timeScale = 1;
    }
}