using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackActions : MonoBehaviour
{
    public bool robotActive;
    PlayerController robot;
    void Start()
    {
        if(robotActive)
            robot = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
   void AttackAction()
    {
        if (robotActive)
        {
            robot.AttackAction();
        }
    }
   void SpinLock()
    {
        if (robotActive)
        {
            robot.SpinLock();
        }
    }
}