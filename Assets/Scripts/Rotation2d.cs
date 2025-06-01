using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation2d : MonoBehaviour
{
    
    [SerializeField] private Transform transform;
    [SerializeField] private float x;
    [SerializeField] private float y;
    [SerializeField] private float z;

    private void FixedUpdate()
    {
        transform.Rotate(x, y, z, Space.Self);
    }
}
