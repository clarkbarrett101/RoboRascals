using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BuilderController : MonoBehaviour
{
    bool leftPressed = false;
    bool rightPressed = false;
    Vector2 move;
    public GameObject screen;
    public Transform[] screenpos;
    public Animator[] buttons;
    public Mannequin mannequin;
    private int rselection = 0;
    private int lselection = 0;
    float cooldown = 0;
    public GameObject[] mannequins;
    int mannequinIndex = 0;
    public GameObject currentMannequin;
    static int playerid = 0;
    public static BuildData[] builds = new BuildData[2];
    int myid = 0;
    void Start()
    {
        myid = playerid;
        transform.parent.position += playerid * Vector3.left * 15;
        playerid++;
        mannequin.builderController = this;
        mannequin.SetLeftWeapon(0);
        mannequin.SetRightWeapon(0);
    }


    void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
    }
        if (leftPressed)
        {
            screen.SetActive(true);
            screen.transform.position = screenpos[0].position;
            buttons[lselection].SetBool("SideA", true);
        }
        else if (rightPressed)
        {
            screen.SetActive(true);
            screen.transform.position = screenpos[1].position;
            buttons[rselection].SetBool("SideA", true);
        }
        else
        {
            screen.SetActive(false);
        }
        if(move.y>0)
        {
            cooldown = .5f;
            if (leftPressed)
            {
                buttons[lselection].SetBool("SideA", false);
                move.y = 0;
                lselection--;
                if (lselection < 0)
                    lselection = mannequin.allWeapons.Length - 1;
                buttons[lselection].SetBool("SideA", true);
                mannequin.SetLeftWeapon(lselection);
            }
            else if (rightPressed)
            {
                buttons[rselection].SetBool("SideA", false);
                move.y = 0;
                rselection--;
                if (rselection < 0)
                    rselection = mannequin.allWeapons.Length - 1;
                buttons[rselection].SetBool("SideA", true);
                mannequin.SetRightWeapon(rselection);
            }
        }
        else if(move.y<0)
        {
            cooldown = .5f;
            if (leftPressed)
            {
                buttons[lselection].SetBool("SideA", false);;
                move.y = 0;
                lselection++;
                if (lselection >= mannequin.allWeapons.Length)
                    lselection = 0;
                buttons[lselection].SetBool("SideA", true);;
                mannequin.SetLeftWeapon(lselection);
            }else if (rightPressed)
            {
                buttons[rselection].SetBool("SideA", false);
                move.y = 0;
                rselection++;
                if(rselection >= mannequin.allWeapons.Length)
                    rselection = 0;
                buttons[rselection].SetBool("SideA", true);;
                mannequin.SetRightWeapon(rselection);
            }
        }
    }
    public void OnAttackA()
    {
        leftPressed = !leftPressed;
        if(rightPressed)
            rightPressed = false;
    }
    public void OnAttackB()
    {
        rightPressed = !rightPressed;
        if(leftPressed)
            leftPressed = false;
    }
    void OnMove(InputValue value)
    {
        if(cooldown > 0)
        {
            return;
        }
        move = value.Get<Vector2>();
    }
    public void OnStart()
    {
        Build();
        SceneManager.LoadScene(1);
    }
    public void Build()
    {
        BuildData buildData = new BuildData();
        buildData.leftWeapon = mannequin.leftCWeapon;
        buildData.rightWeapon = mannequin.rightCWeapon;
        buildData.hero = mannequinIndex;
        PlayerInput p = GetComponent<PlayerInput>();
        buildData.device = p.devices[0];
        builds[myid] = buildData;
    }

    void OnCycleDown()
    {
        Destroy(currentMannequin);
        if(mannequinIndex>0)
        {
            mannequinIndex--;
        }
        else
        {
            mannequinIndex = mannequins.Length - 1;
        }
        currentMannequin = Instantiate(mannequins[mannequinIndex],transform);
        mannequin = currentMannequin.GetComponent<Mannequin>();
        mannequin.builderController = this;
        mannequin.SetLeftWeapon(lselection);
        mannequin.SetRightWeapon(rselection);
    }

    void OnCycleUp()
    {
        Destroy(currentMannequin);
        if (mannequinIndex < mannequins.Length-1)
        {
            mannequinIndex++;
        }
        else
        {
            mannequinIndex = 0;
        }
        currentMannequin = Instantiate(mannequins[mannequinIndex], transform);
        mannequin = currentMannequin.GetComponent<Mannequin>();
        mannequin.builderController = this;
        mannequin.SetLeftWeapon(lselection);
        mannequin.SetRightWeapon(rselection);
    }

    public struct BuildData
    {
        public C_Weapon leftWeapon;
        public C_Weapon rightWeapon;
        public int hero;
        public InputDevice device;
    }

}