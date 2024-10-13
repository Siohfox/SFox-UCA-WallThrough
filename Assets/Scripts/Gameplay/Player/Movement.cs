using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    private Rigidbody _rb;

    public float moveSpeed = 5f;
    public float acceleration = 10f;
    public float deceleration = 10f;
    private Vector3 currentVelocity;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        currentVelocity = Vector3.zero; // Start with no movement
    }

    public void Move(float x, float z)
    {
        // Calculate target velocity based on input
        Vector3 targetVelocity = new Vector3(x, 0, z).normalized * moveSpeed;

        // Smoothly transition to the target velocity using acceleration/deceleration
        if (targetVelocity.magnitude > 0)
        {
            currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        }
        else
        {
            currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, deceleration * Time.fixedDeltaTime);
        }

        // Apply the calculated velocity to the Rigidbody
        _rb.velocity = new Vector3(currentVelocity.x, _rb.velocity.y, currentVelocity.z);
    }
}