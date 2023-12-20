using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawProjectile : MonoBehaviour
{
    public float speed;
    public float timer;
    float maxTimer= 0.6f;
    public Vector3 direction;
    public Transform target;
    void Start()
    {
        transform.rotation = Quaternion.LookRotation(direction);
        timer = maxTimer;
    }
    public void Setup(Vector3 direction, Transform target)
    {
        this.direction = direction;
        this.target = target;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer > maxTimer / 2)
            {
                transform.position += direction * (speed * Time.deltaTime);
            }
            else
            {
                transform.position -= (target.position - transform.position).normalized * (Time.deltaTime/(maxTimer/2));
            }
        }
        else
        {
            Destroy(gameObject);
        }
        transform.position += direction * (speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(TryGetComponent<A_Enemy>(out A_Enemy enemy))
        {
            enemy.PushActor((enemy.transform.position - target.position).normalized * 20);
        }
    }
}