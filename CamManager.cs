using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamManager : MonoBehaviour
{
    public float moveSpeed;
    public static List<Transform> players = new List<Transform>();
    Vector3 offset;
    Camera cam;
    public float maxFOV;
    public float minFOV;
    void Start()
    {
        offset = transform.position;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
       Vector3 averagePosition = Vector3.zero;
       if (players.Count > 0)
       {
           foreach (Transform player in players)
           {
               averagePosition += player.position;
           }

           averagePosition /= players.Count;
       }

       transform.position= Vector3.Lerp(transform.position , averagePosition+ offset, Time.deltaTime*moveSpeed);
       bool shrunk = false;
       foreach (Transform player in players)
       {
          if(cam.WorldToScreenPoint(player.transform.position).x < 0 || cam.WorldToScreenPoint(player.transform.position).x > Screen.width || cam.WorldToScreenPoint(player.transform.position).y < 0 || cam.WorldToScreenPoint(player.transform.position).y > Screen.height-100)
          {
              shrunk = true;

              cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, maxFOV, Time.deltaTime );
          }
       }

       if (!shrunk)
           cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, minFOV, Time.deltaTime/2 );
    }
}