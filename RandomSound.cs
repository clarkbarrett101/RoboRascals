using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomSound : MonoBehaviour
{
    public AudioClip[] clips;
    float timer = 0;
    AudioSource audioSource;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clips[Random.Range(0, clips.Length)];
        timer = Random.Range(0, .2f);
        audioSource.pitch = Random.Range(0.8f, 1.2f);
        audioSource.volume = Random.Range(0.8f, 1.2f);
    }

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            return;
        }
        timer=100;
        audioSource.Play();
    }
}