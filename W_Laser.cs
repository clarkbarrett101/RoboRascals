using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Weapon/Laser")]
public class W_Laser : C_Weapon
{
    public GameObject projetile;
    public float projectileSpeed;
    public override void Action(A_Robot robot)
    {
        base.Action(robot);
        for(int i = 0; i < hitCount; i++)
        {
            GameObject projectileInstance = Instantiate(projetile, robot.transform.position, robot.transform.rotation);
            Projectile projectile = projectileInstance.GetComponent<Projectile>();

        }
    }
}