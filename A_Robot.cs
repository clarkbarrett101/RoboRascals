using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Robot : I_Actor
{
    PlayerController playerController;
    GameObject rightWeapon;
    public C_Weapon rightCWeapon;
    public Transform rightHand;
    GameObject leftWeapon;
    public C_Weapon leftCWeapon;
    public Transform leftHand;
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        rightWeapon = Instantiate(rightCWeapon.prefab, rightHand);
        leftWeapon = Instantiate(leftCWeapon.prefab, leftHand);
        rightWeapon.GetComponent<Weapon>().owner = this;
        leftWeapon.GetComponent<Weapon>().owner = this;
        playerController.weaponAnimA = rightWeapon.GetComponent<Animator>();
        playerController.weaponAnimB = leftWeapon.GetComponent<Animator>();
    }

}