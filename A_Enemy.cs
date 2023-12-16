using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class A_Enemy : I_Actor
{
    public List<GameObject> bits = new List<GameObject>(16);
    public GameObject bitPrefab;
    public GameObject sparks;
    public float bitSpeed;
    public override void ApplyDamage(I_Actor attacker, float dmg)
    {
        base.ApplyDamage(attacker, dmg);
        anim.SetTrigger("Hit");
        Vector3 hitDir = transform.position - attacker.transform.position;
        Instantiate(sparks,transform.position,Quaternion.LookRotation(hitDir));
        while(bits.Count>(chp/mhp)*bits.Capacity)
        {
            int bit = Random.Range(0, bits.Count);
            ShootBit(bits[bit],hitDir);
            bits.Remove(bits[bit]);
        }
    }
    void ShootBit(GameObject bit,Vector3 dir)
    {
        bit.SetActive(false);
        Vector3 newdir = new Vector3(dir.x*Random.value,1,dir.z*Random.value);
        GameObject newbit = Instantiate(bitPrefab,transform.position,Quaternion.identity);
        newbit.GetComponent<Rigidbody>().AddForce(newdir*bitSpeed);
        Destroy(newbit,2);
    }
}