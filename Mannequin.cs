using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mannequin : MonoBehaviour
{
    GameObject rightWeapon;
    public C_Weapon rightCWeapon;
    public Transform rightHand;
    GameObject leftWeapon;
    public C_Weapon leftCWeapon;
    public Transform leftHand;
    public C_Weapon[] allWeapons;
    Animator anim;
    Animator rightAnim;
    Animator leftAnim;
    AudioSource audioSource;

    private void Start()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void SetRightWeapon(int index)
    {
        rightCWeapon = allWeapons[index];
        Destroy(rightWeapon);
        rightWeapon = Instantiate(rightCWeapon.prefab, rightHand);
        rightAnim = rightWeapon.GetComponent<Animator>();
        rightWeapon.transform.localScale = new Vector3(rightWeapon.transform.localScale.x, rightWeapon.transform.localScale.y*-1, rightWeapon.transform.localScale.z);
        rightAnim.SetTrigger("Attack");
        anim.SetTrigger("RAttack");
        audioSource.Play();
    }
    public void SetLeftWeapon(int index)
    {
        leftCWeapon = allWeapons[index];
        Destroy(leftWeapon);
        leftWeapon = Instantiate(leftCWeapon.prefab, leftHand);
        leftAnim = leftWeapon.GetComponent<Animator>();
        anim.SetTrigger("LAttack");
        leftAnim.SetTrigger("Attack");
        audioSource.Play();
    }

    public void FinalizeLoadout()
    {
        StaticVariables.weapons = new C_Weapon[2];
        StaticVariables.weapons[0] = rightCWeapon;
        StaticVariables.weapons[1] = leftCWeapon;
    }
}