using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class BuilderController : MonoBehaviour
{
    public InputActionReference leftButton;
    public InputActionReference rightButton;
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
    void Start()
    {
        leftButton.action.started += ctx =>
        {
            leftPressed = true;

        };
        leftButton.action.canceled += ctx =>
        {
            leftPressed = false;

        };
        rightButton.action.started += ctx =>
        {
            rightPressed = true;
        };
        rightButton.action.canceled += ctx =>
        {
            rightPressed = false;

        };
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
        mannequin.FinalizeLoadout();
        SceneManager.LoadScene(1);
    }
}