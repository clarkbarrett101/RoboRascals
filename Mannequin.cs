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
    public Animator anim;
    Animator rightAnim;
    Animator leftAnim;
    public AudioSource audioSource;
    public BuilderController builderController;


    public void SetRightWeapon(int index)
    {
        rightCWeapon = allWeapons[index];
        if(rightWeapon!=null)
            Destroy(rightWeapon);
        rightWeapon = Instantiate(rightCWeapon.prefab, rightHand);
        rightAnim = rightWeapon.GetComponent<Animator>();
        rightWeapon.transform.localScale = new Vector3(rightWeapon.transform.localScale.x, rightWeapon.transform.localScale.y*-1, rightWeapon.transform.localScale.z);
        rightAnim.SetTrigger("Attack");
        anim.SetTrigger("RAttack");
        audioSource.Play();
        builderController.Build();
    }
    public void SetLeftWeapon(int index)
    {
        leftCWeapon = allWeapons[index];
        if(leftWeapon!=null)
            Destroy(leftWeapon);
        leftWeapon = Instantiate(leftCWeapon.prefab, leftHand);
        leftAnim = leftWeapon.GetComponent<Animator>();
        anim.SetTrigger("LAttack");
        leftAnim.SetTrigger("Attack");
        audioSource.Play();
        builderController.Build();
    }
    void SpinLock(){}
    void AttackAction(){}

}