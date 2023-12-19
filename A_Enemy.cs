using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class A_Enemy : I_Actor
{
    public List<GameObject> bits = new List<GameObject>(16);
    public GameObject bitPrefab;
    public float bitSpeed;
    public GameObject explosion;
    public StateMachine state;
    public EnemyStats stats;
    List<A_Robot> players = new List<A_Robot>();
    A_Robot closestPlayer;
    float hitstun;
    float cooldown;
    public HitBox hitBox;
    private void Start()
    {
        mhp = stats.mhp;
        chp = mhp;
        moveSpeed = stats.moveSpeed;
        hitBox.attacker = this;
        hitBox.dmg = stats.attackDamage;
        players.AddRange(FindObjectsOfType<A_Robot>());
        ScoreBoard.enemies.Add(this);
        hitBox.hitPush = stats.attackPush;
        hitBox.recoil = stats.attackRecoil;
    }

    public enum StateMachine
    {
        Idle,
        Attack,
        Move,
        Dead
    }
    public override void ApplyDamage(I_Actor attacker, float dmg)
    {
        base.ApplyDamage(attacker, dmg);
        hitstun = .5f;
        anim.SetTrigger("Hit");
        Vector3 hitDir = transform.position - attacker.transform.position;
        Destroy(Instantiate(sparks,transform.position+Vector3.up,Quaternion.LookRotation(hitDir)),2);
        try
        {
            while (bits.Count > (chp / mhp) * bits.Capacity)
            {
                int bit = Random.Range(0, bits.Count);
                ShootBit(bits[bit], hitDir);
                bits.Remove(bits[bit]);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }

        if (chp <= 0)
        {
            Die();
        }
    }
    void ShootBit(GameObject bit,Vector3 dir)
    {
        bit.SetActive(false);
        Vector3 newdir = new Vector3(dir.x*Random.value,.1f,dir.z*Random.value);
        GameObject newbit = Instantiate(bitPrefab,transform.position,Random.rotation);
        newbit.GetComponent<Rigidbody>().AddForce(newdir*bitSpeed);
        Destroy(newbit,2);
    }
    public override void Die()
    {
        Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 1);
       ScoreBoard.enemies.Remove(this);
        Destroy(gameObject);
    }

    private void Update()
    {
        base.Update();
        foreach(RaycastHit hit in Physics.SphereCastAll(transform.position,1,transform.forward,1))
        {
            if(hit.collider.gameObject != gameObject && hit.collider.gameObject.tag == gameObject.tag)
            {
                transform.position += (transform.position - hit.collider.transform.position).normalized * (moveSpeed * Time.deltaTime);
            }
        }
        if(hitstun > 0)
        {
            hitstun -= Time.deltaTime;
            return;
        }
        closestPlayer = FindClosestPlayer();
        switch (state)
        {
            case StateMachine.Idle:
                Idle();
                break;
            case StateMachine.Attack:
                Attack();
                break;
            case StateMachine.Move:
                Move();
                break;
            case StateMachine.Dead:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    void Idle()
    {

    }
    void Attack()
    {
        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
            return;
        }
        if(Vector3.Distance(transform.position,closestPlayer.transform.position) > stats.attackRange)
        {
            state = StateMachine.Move;
            return;
        }
        anim.SetTrigger("Attack");
        cooldown = stats.attackCooldown;
    }
    void Move()
    {
        if(Vector3.Distance(transform.position,closestPlayer.transform.position) > stats.attackRange)
        {
            transform.position += (closestPlayer.transform.position - transform.position).normalized * (moveSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x,-2.5f,transform.position.z);
            transform.LookAt(closestPlayer.transform);
            transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
        }else
        {
            state = StateMachine.Attack;
        }
    }
    A_Robot FindClosestPlayer()
    {
        A_Robot closest = players[0];
        float closestDist = Vector3.Distance(transform.position, closest.transform.position);
        foreach (A_Robot player in players)
        {
            float dist = Vector3.Distance(transform.position, player.transform.position);
            if (dist < closestDist)
            {
                closest = player;
                closestDist = dist;
            }
        }
        return closest;
    }

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("Collision");
        transform.position += (transform.position - collision.transform.position).normalized * (moveSpeed * Time.deltaTime);
    }
}