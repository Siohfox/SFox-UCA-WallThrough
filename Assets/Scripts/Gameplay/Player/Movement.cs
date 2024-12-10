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
        public Transform cameraTransform; // Reference to the camera transform

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

            // Get the camera's forward and right vectors (projected to the XZ plane)
            Vector3 cameraForward = cameraTransform.forward;
            cameraForward.y = 0; // Ignore the vertical component (we don't want to move up/down)
            cameraForward.Normalize();

            Vector3 cameraRight = cameraTransform.right;
            cameraRight.y = 0; // Ignore the vertical component
            cameraRight.Normalize();

            // Calculate the movement direction relative to the camera's orientation
            Vector3 targetDirection = cameraForward * moveDir.y + cameraRight * moveDir.x;

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
