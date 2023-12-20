using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Weapon/Claw")]
public class W_Claw : C_Weapon
{
    public float pullForce;
    public GameObject claw;
    public override void OnHit(I_Actor target)
    {
        base.OnHit(target);
        target.PushActor((target.transform.position - target.transform.position).normalized * pullForce);
    }

    public override void Action(A_Robot robot)
    {
        base.Action(robot);
        GameObject clawInstance = Instantiate(claw, robot.transform.position, robot.transform.rotation);
        clawInstance.GetComponent<ClawProjectile>().Setup(robot.transform.forward, robot.transform);
    }
}