using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPrefabSwapper : PlayerInputManager
{
    public PlayerInputManager inputManager;
    public GameObject[] heros;

    private void Awake()
    {
        inputManager = GetComponent<PlayerInputManager>();
        inputManager.onPlayerJoined += OnPlayerJoined;
    }

    void OnPlayerJoined(PlayerInput input)
    {

    }

}