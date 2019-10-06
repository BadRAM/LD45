using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float rate;
    [SerializeField] private Vector3 axis;

    private void Update()
    {
        transform.Rotate(axis, rate * Time.deltaTime);
    }
}
