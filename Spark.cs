using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : MonoBehaviour
{
    void Start()
    {
        GetComponent<AudioSource>().pitch = Random.Range(0.8f, 1.2f);
    }
}