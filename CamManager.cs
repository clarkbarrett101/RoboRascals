using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    public float moveSpeed;
    public static List<Transform> players = new List<Transform>();
    Vector3 offset;
    void Start()
    {
        offset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 averagePosition = Vector3.zero;
       foreach (Transform player in players)
       {
           averagePosition += player.position;
       }
       averagePosition /= players.Count;
        transform.position= Vector3.Lerp(transform.position , averagePosition+ offset, Time.deltaTime*moveSpeed);
    }
}