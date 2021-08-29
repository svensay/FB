using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalMovement : MonoBehaviour
{

    private Vector3 pos = default;
    private float yBase = 0;

    private void Start()
    {
        yBase = transform.position.y;
    }

    private void Update()
    {
        pos = transform.position;
        transform.position = new Vector3(pos.x, yBase + Mathf.Sin(Time.time * 2.0f) / 2.0f);
    }
}
