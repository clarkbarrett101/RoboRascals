using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTransition : MonoBehaviour
{


    public void StartLevel(int i)
    {
        SceneManager.LoadScene(i);
    }
}