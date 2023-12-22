using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private Material mat;
    public Color color;
    public float speed;
    private float offset;
    private void Start()
    {
        mat = GetComponent<MeshRenderer>().material;
        mat.SetColor("_EmissionColor", color);
    }

    private void Update()
    {
        if (offset < 1)
        {
            offset += speed * Time.deltaTime;
        }
        else
        {
            offset -= 1;
        }
        mat.mainTextureOffset = new Vector2(-offset, 0);
    }
}