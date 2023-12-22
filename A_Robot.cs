using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class A_Robot : I_Actor
{
    public float hpRegen;
    PlayerController playerController;
    GameObject rightWeapon;
    public C_Weapon rightCWeapon;
    public Transform rightHand;
    GameObject leftWeapon;
    public C_Weapon leftCWeapon;
    public Transform leftHand;
    public C_Weapon cGun;
    public Transform gunHand;
    public GameObject gunObj;
    public float rcooldown;
    public float lcooldown;
    public float gcooldown;
    float hurtTimer = 0;
    public Image hpMeter;
    Color mainColor;
    public AttackType attackType;
    public GameObject[] cooldownMetersR;
    public GameObject[] cooldownMetersL;

    public enum AttackType
    {
        Right,
        Left,
        Gun
    }
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
       // cGun = StaticVariables.weapons[2];
        playerController = GetComponent<PlayerController>();
        rightWeapon = Instantiate(rightCWeapon.prefab, rightHand);
        rightWeapon.transform.localScale = new Vector3(rightWeapon.transform.localScale.x, rightWeapon.transform.localScale.y*-1, rightWeapon.transform.localScale.z);
        leftWeapon = Instantiate(leftCWeapon.prefab, leftHand);
        gunObj = Instantiate(cGun.prefab, gunHand);
        rightWeapon.GetComponent<Weapon>().owner = this;
        leftWeapon.GetComponent<Weapon>().owner = this;
        gunObj.GetComponent<Weapon>().owner = this;
        playerController.weaponAnimA = rightWeapon.GetComponent<Animator>();
        playerController.weaponAnimB = leftWeapon.GetComponent<Animator>();
        playerController.weaponAnimC = gunObj.GetComponent<Animator>();
    }

    private void Update()
    {
        base.Update();
        if(chp < mhp)
        {
            chp += hpRegen * Time.deltaTime*(mhp-chp)/mhp;
        }else
        {
            chp = mhp;
        }
        if(gcooldown> 0)
        {
            gcooldown -= Time.deltaTime;
        }
        if(rcooldown > 0)
        {
            rcooldown -= Time.deltaTime;
            if(rcooldown < rightCWeapon.cooldown*.3f)
            {
                cooldownMetersR[0].SetActive(true);
                cooldownMetersR[1].SetActive(false);
                cooldownMetersR[2].SetActive(false);
            }
            else if(rcooldown < rightCWeapon.cooldown*.7f)
            {
                cooldownMetersR[0].SetActive(false);
                cooldownMetersR[1].SetActive(true);
                cooldownMetersR[2].SetActive(false);
            }else
            {
                cooldownMetersR[0].SetActive(false);
                cooldownMetersR[1].SetActive(false);
                cooldownMetersR[2].SetActive(false);
            }
        }
        else
        {
            cooldownMetersR[0].SetActive(false);
            cooldownMetersR[1].SetActive(false);
            cooldownMetersR[2].SetActive(true);
        }
        if(lcooldown > 0)
        {
            lcooldown -= Time.deltaTime;
            if(lcooldown < leftCWeapon.cooldown*.3f)
            {
                cooldownMetersL[0].SetActive(true);
                cooldownMetersL[1].SetActive(false);
                cooldownMetersL[2].SetActive(false);
            }
            else if(lcooldown < leftCWeapon.cooldown*.7f)
            {
                cooldownMetersL[0].SetActive(false);
                cooldownMetersL[1].SetActive(true);
                cooldownMetersL[2].SetActive(false);
            }
            else
            {
                cooldownMetersL[0].SetActive(false);
                cooldownMetersL[1].SetActive(false);
                cooldownMetersL[2].SetActive(false);
            }
        }
        else
        {
            cooldownMetersL[0].SetActive(false);
            cooldownMetersL[1].SetActive(false);
            cooldownMetersL[2].SetActive(true);
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
        switch (attackType)
        {
            case AttackType.Right:
                rightCWeapon.OnHit(target);
                break;
            case AttackType.Left:
                leftCWeapon.OnHit(target);
                break;
            case AttackType.Gun:
                cGun.OnHit(target);
                break;
        }
    }
}