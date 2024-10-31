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
        public float rotationSpeed = 720f; // Degrees per second

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void Move(Vector2 moveDir)
        {
            // Use a threshold to prevent very small inputs from affecting movement
            if (moveDir.magnitude < 0.1f)
            {
                // Stop the player's movement when there is no input
                _rb.velocity = new Vector3(0, _rb.velocity.y, 0);
                return;
            }

            // Calculate the target direction based on input
            Vector3 targetDirection = new Vector3(moveDir.x, 0, moveDir.y).normalized;

            // Rotate towards the target direction
            RotateTowards(targetDirection);

            // Move in the direction the player is facing
            Vector3 forward = transform.forward;
            _rb.velocity = new Vector3(forward.x * moveSpeed, _rb.velocity.y, forward.z * moveSpeed);
        }


        private void RotateTowards(Vector3 direction)
        {
            if (direction.magnitude > 0)
            {
                // Calculate the target rotation
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                // Smoothly rotate towards the target rotation
                float step = rotationSpeed * Time.fixedDeltaTime; // Calculate the step size
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
            }
        }


        public Vector3 GetCurrentVelocity() => _rb.velocity;
    }
}
