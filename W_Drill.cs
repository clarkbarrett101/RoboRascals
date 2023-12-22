using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Weapon/Drill")]
public class W_Drill : C_Weapon
{

    public float momentum;
        public override void Action(A_Robot robot)
    {
        robot.PushActor(robot.transform.forward*momentum);
    }
}