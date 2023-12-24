using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class A_Robot : I_Actor
{
    public float hpRegen;
    PlayerController playerController;
    [FormerlySerializedAs("legs")] public int heroId;
    public C_Weapon rightCWeapon;
    public C_Weapon leftCWeapon;
    public C_Weapon cGun;
    public Transform gunHand;
    public GameObject gunObj;
    public float rcooldown;
    public float lcooldown;
    public float gcooldown;
    float hurtTimer = 0;
    Color mainColor;
    public AttackType attackType;
    public Hero[] heroes;
    public Hero hero;
    public bool dead;

    public enum AttackType
    {
        Right,
        Left,
        Gun
    }
    public override void Awake()
    {
        base.Awake();
        playerController = GetComponent<PlayerController>();
    }
    void Start()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();
        for(int i=0; i<BuilderController.builds.Length; i++)
        {
            if(BuilderController.builds[i].device.deviceId == playerInput.devices[0].deviceId)
            {
                SetUpRobot(BuilderController.builds[i]);
                break;
            }
        }
        foreach(Hero h in heroes)
        {
            if (!h.Equals(hero))
            {
                Destroy(h.gameObject);
            }
        }
        anim = hero.GetComponent<Animator>();
        anim.SetInteger("Legs", heroId);
    }

    public void SetUpRobot(BuilderController.BuildData buildData)
    {
        heroId = buildData.hero;
        hero = heroes[heroId];
        mainColor = hero.hpMeter.color;
        rightCWeapon = buildData.rightWeapon;
        leftCWeapon = buildData.leftWeapon;
        hero.rightWeapon = Instantiate(rightCWeapon.prefab, hero.rightHand);
        hero.rightWeapon.transform.localScale = new Vector3(hero.rightWeapon.transform.localScale.x, hero.rightWeapon.transform.localScale.y*-1, hero.rightWeapon.transform.localScale.z);
        hero.leftWeapon = Instantiate(leftCWeapon.prefab, hero.leftHand);
        hero.rightWeapon.GetComponent<Weapon>().owner = this;
        hero.leftWeapon.GetComponent<Weapon>().owner = this;
        playerController.weaponAnimA = hero.rightWeapon.GetComponent<Animator>();
        playerController.weaponAnimB = hero.leftWeapon.GetComponent<Animator>();
        foreach(A_Enemy enemy in FindObjectsOfType<A_Enemy>())
        {
            enemy.players.Add(this);
        }
        CamManager.players.Add(transform);
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
                hero.cooldownMetersR[0].SetActive(true);
                hero.cooldownMetersR[1].SetActive(false);
                hero.cooldownMetersR[2].SetActive(false);
            }
            else if(rcooldown < rightCWeapon.cooldown*.7f)
            {
                hero.cooldownMetersR[0].SetActive(false);
                hero.cooldownMetersR[1].SetActive(true);
                hero.cooldownMetersR[2].SetActive(false);
            }else
            {
                hero.cooldownMetersR[0].SetActive(false);
                hero.cooldownMetersR[1].SetActive(false);
                hero.cooldownMetersR[2].SetActive(false);
            }
        }
        else
        {
            hero.cooldownMetersR[0].SetActive(false);
            hero.cooldownMetersR[1].SetActive(false);
            hero.cooldownMetersR[2].SetActive(true);
        }
        if(lcooldown > 0)
        {
            lcooldown -= Time.deltaTime;
            if(lcooldown < leftCWeapon.cooldown*.3f)
            {
                hero.cooldownMetersL[0].SetActive(true);
                hero.cooldownMetersL[1].SetActive(false);
                hero.cooldownMetersL[2].SetActive(false);
            }
            else if(lcooldown < leftCWeapon.cooldown*.7f)
            {
                hero.cooldownMetersL[0].SetActive(false);
                hero.cooldownMetersL[1].SetActive(true);
                hero.cooldownMetersL[2].SetActive(false);
            }
            else
            {
                hero.cooldownMetersL[0].SetActive(false);
                hero.cooldownMetersL[1].SetActive(false);
                hero.cooldownMetersL[2].SetActive(false);
            }
        }
        else
        {
            hero.cooldownMetersL[0].SetActive(false);
            hero.cooldownMetersL[1].SetActive(false);
            hero.cooldownMetersL[2].SetActive(true);
        }
        if(hurtTimer > 0)
        {
            hurtTimer -= Time.deltaTime;
            hero.hpMeter.color = Color.Lerp(Color.yellow, mainColor, hurtTimer);
        }else
        {
            hero.hpMeter.color = mainColor;
        }
        hero.hpMeter.fillAmount = chp / mhp;
        if (dead)
        {
            hero.hpMeter.color = Color.white;
            chp+=hpRegen*Time.deltaTime*2;
            if (chp > mhp / 2)
            {
                dead = false;
                anim.SetBool("Dead", false);
                foreach(A_Enemy enemy in FindObjectsOfType<A_Enemy>())
                {
                    enemy.players.Add(this);
                }
            }

        }
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
        dead = true;
        anim.SetBool("Dead", true);
        anim.SetBool("Walking", false);
        foreach(A_Enemy enemy in FindObjectsOfType<A_Enemy>())
        {
            enemy.players.Remove(this);
        }
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