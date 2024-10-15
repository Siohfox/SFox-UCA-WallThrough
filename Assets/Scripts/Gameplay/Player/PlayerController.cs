using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace WallThrough.Gameplay.Pawn
{
    [RequireComponent(typeof(Movement))]
    public class PlayerController : MonoBehaviour
    {
        private Movement _movement;
        [SerializeField]
        private InputActionReference moveActionToUse;
        [SerializeField]
        private bool debugControls = false;
        [SerializeField]
        private GameObject virtualJoystick;

        private void Start()
        {
            _movement = GetComponent<Movement>();
        }

        private void FixedUpdate()
        {
            Vector2 moveDirection;

            // Check if the application is running on PC or mobile
            if (Application.platform == RuntimePlatform.WindowsPlayer ||
                Application.platform == RuntimePlatform.LinuxPlayer ||
                Application.platform == RuntimePlatform.OSXPlayer ||
                Application.platform == RuntimePlatform.OSXEditor ||
                Application.platform == RuntimePlatform.WindowsEditor ||
                Application.platform == RuntimePlatform.LinuxEditor)
            {
                moveDirection.x = Input.GetAxis("Horizontal");
                moveDirection.y = Input.GetAxis("Vertical");
            }
            else if(debugControls)
            {
                moveDirection = moveActionToUse.action.ReadValue<Vector2>();
            }
            else
            {
                moveDirection = moveActionToUse.action.ReadValue<Vector2>();
            }

            _movement.Move(moveDirection);
        }
    }
}