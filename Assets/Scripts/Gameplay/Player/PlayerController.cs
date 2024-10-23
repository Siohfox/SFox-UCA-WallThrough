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
        private PlayerStats playerStats;

        [SerializeField] private InputActionReference moveActionToUse;
        [SerializeField] private bool debugControls;
        [SerializeField] private GameObject virtualJoystick;

        private void Start()
        {
            _movement = GetComponent<Movement>();
            playerStats = GetComponent<PlayerStats>();
        }

        private void FixedUpdate()
        {
            Vector2 moveDirection;

            // Check if the application is running on PC or mobile
            if (debugControls)
            {
                // Use virtual joystick in debug mode
                Debug.Log("Debug controls active");
                virtualJoystick.SetActive(true);
                moveDirection = moveActionToUse.action.ReadValue<Vector2>();
            }
            else if (Application.platform == RuntimePlatform.WindowsPlayer ||
                     Application.platform == RuntimePlatform.LinuxPlayer ||
                     Application.platform == RuntimePlatform.OSXPlayer ||
                     Application.platform == RuntimePlatform.OSXEditor ||
                     Application.platform == RuntimePlatform.WindowsEditor ||
                     Application.platform == RuntimePlatform.LinuxEditor)
            {
                // Use keyboard controls on PC
                virtualJoystick.SetActive(false);
                moveDirection = moveActionToUse.action.ReadValue<Vector2>();
            }
            else // mobile
            {
                // Use virtual joystick for mobile
                virtualJoystick.SetActive(true);
                moveDirection = moveActionToUse.action.ReadValue<Vector2>();
            }

            if(playerStats.AliveState == true)
            {
                _movement.Move(moveDirection);
            }
        }
    }
}