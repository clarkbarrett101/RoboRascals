using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealTimeLine : MonoBehaviour
{
    LineRenderer lineRenderer;
    public Transform[] points;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, points[0].position);
        lineRenderer.SetPosition(1, points[1].position);
    }
}