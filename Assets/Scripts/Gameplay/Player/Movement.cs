using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    private Rigidbody _rb;

    public float moveSpeed;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void Move(float x, float z)
    {
        _rb.AddForce(new Vector3(x * moveSpeed, 0f, z * moveSpeed));
    }
}
