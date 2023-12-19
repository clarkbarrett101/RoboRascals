using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class A_Robot : I_Actor
{
    PlayerController playerController;
    GameObject rightWeapon;
    public C_Weapon rightCWeapon;
    public Transform rightHand;
    GameObject leftWeapon;
    public C_Weapon leftCWeapon;
    public Transform leftHand;
    public float rcooldown;
    public float lcooldown;
    float hurtTimer = 0;
    public Image hpMeter;
    Color mainColor;
    public bool attackingRight = false;

    public override void Awake()
    {
        base.Awake();
        mainColor = hpMeter.color;
        playerController = GetComponent<PlayerController>();
    }
    void Start()
    {
        rightCWeapon = StaticVariables.weapons[0];
        leftCWeapon = StaticVariables.weapons[1];
        playerController = GetComponent<PlayerController>();
        rightWeapon = Instantiate(rightCWeapon.prefab, rightHand);
        rightWeapon.transform.localScale = new Vector3(rightWeapon.transform.localScale.x, rightWeapon.transform.localScale.y*-1, rightWeapon.transform.localScale.z);
        leftWeapon = Instantiate(leftCWeapon.prefab, leftHand);
        rightWeapon.GetComponent<Weapon>().owner = this;
        leftWeapon.GetComponent<Weapon>().owner = this;
        playerController.weaponAnimA = rightWeapon.GetComponent<Animator>();
        playerController.weaponAnimB = leftWeapon.GetComponent<Animator>();

    }

    private void Update()
    {
        base.Update();
        if(rcooldown > 0)
        {
            rcooldown -= Time.deltaTime;
        }
        if(lcooldown > 0)
        {
            lcooldown -= Time.deltaTime;
        }
        if(hurtTimer > 0)
        {
            hurtTimer -= Time.deltaTime;
            hpMeter.color = Color.Lerp(Color.yellow, mainColor, hurtTimer);
        }else
        {
            hpMeter.color = mainColor;
        }
        hpMeter.fillAmount = chp / mhp;
    }

    public override void ApplyDamage(I_Actor attacker, float dmg)
    {
        base.ApplyDamage(attacker, dmg);
        hurtTimer = .5f;
        Destroy(Instantiate(sparks,transform.position+Vector3.up,Quaternion.LookRotation(attacker.transform.forward)),2);
        anim.SetTrigger("Hit");
        playerController.SpinLock();
        if(chp <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }

    public override void OnHit(I_Actor target)
    {
        base.OnHit(target);
        if (attackingRight)
        {
            rightCWeapon.OnHit(target);
        }else
        {
            leftCWeapon.OnHit(target);
        }
    }
}