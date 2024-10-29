using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WallThrough.Gameplay.Pawn
{
    [RequireComponent(typeof(Rigidbody))]
    public class Movement : MonoBehaviour
    {
        private Rigidbody _rb;

        public float moveSpeed = 5f;
        public float acceleration = 10f;
        public float deceleration = 10f;
        public float rotationSpeed = 720f; // Degrees per second
        private Vector3 currentVelocity;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            currentVelocity = Vector3.zero; // Start with no movement
        }

        public void Move(Vector2 moveDir)
        {
            // Calculate target velocity based on input
            Vector3 targetVelocity = new Vector3(moveDir.x, 0, moveDir.y).normalized * moveSpeed;

            // Smoothly transition to the target velocity using acceleration/deceleration
            if (targetVelocity.magnitude > 0)
            {
                // Accelerate towards the target velocity
                currentVelocity = Vector3.MoveTowards(currentVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
                RotateTowards(currentVelocity);
            }
            else
            {
                // If no input, decelerate to zero
                currentVelocity = Vector3.MoveTowards(currentVelocity, Vector3.zero, deceleration * Time.fixedDeltaTime);
            }

            // Apply the calculated velocity to the Rigidbody
            _rb.velocity = new Vector3(currentVelocity.x, _rb.velocity.y, currentVelocity.z);
        }

        private void RotateTowards(Vector3 direction)
        {
            if (direction.magnitude > 0)
            {
                // Calculate the target rotation
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // Smoothly rotate towards the target rotation
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
            }
        }

        public Vector3 GetCurrentVelocity() => currentVelocity;
    }
}