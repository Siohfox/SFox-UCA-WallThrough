using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using WallThrough.Core;

namespace WallThrough.Gameplay.Pawn
{
    [RequireComponent(typeof(Movement))]
    public class PlayerController : MonoBehaviour
    {
        private Movement _movement;
        private PlayerStats playerStats;
        [SerializeField] GameObject optionsMenu;

        [SerializeField] private InputActionReference moveActionToUse;
        [SerializeField] private InputActionReference escapeAction;
        [SerializeField] private InputActionReference anyKey;
        [SerializeField] private InputActionReference shiftKey;
        [SerializeField] private bool debugControls;
        [SerializeField] private bool isSprinting;
        [SerializeField] private GameObject virtualJoystick;

        private void Start()
        {
            _movement = GetComponent<Movement>();
            playerStats = GetComponent<PlayerStats>();

            if(playerStats == null)
            {
                Debug.LogWarning("Couldn't get playerstats component for some fucking reason");
            }
        }

        private void OnEnable()
        {
            escapeAction.action.performed += OnEscapePressed;
            escapeAction.action.Enable();
            anyKey.action.performed += RestartGame;
            anyKey.action.Enable();

            shiftKey.action.started += OnSprintStarted;
            shiftKey.action.canceled += OnSprintStopped;
            shiftKey.action.Enable();
        }

        private void OnDisable()
        {
            escapeAction.action.performed -= OnEscapePressed;
            escapeAction.action.Disable();
            anyKey.action.performed -= RestartGame;
            anyKey.action.Disable();

            shiftKey.action.started -= OnSprintStarted;
            shiftKey.action.canceled -= OnSprintStopped;
            shiftKey.action.Disable();
        }

        private void FixedUpdate()
        {
            Vector2 moveDirection;

            
            // Check if the application is running on PC or mobile
            if (debugControls)
            {
                // Use virtual joystick in debug mode
                Debug.Log("Debug controls active");
                if (virtualJoystick) virtualJoystick.SetActive(true);
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
                if (virtualJoystick) virtualJoystick.SetActive(false);
                moveDirection = moveActionToUse.action.ReadValue<Vector2>();
            }
            else // mobile
            {
                // Use virtual joystick for mobile
                if (virtualJoystick) virtualJoystick.SetActive(true);
                moveDirection = moveActionToUse.action.ReadValue<Vector2>();
            }

            if(playerStats.AliveState == true)
            {
                _movement.Move(moveDirection, isSprinting);
            }
        }

        private void OnEscapePressed(InputAction.CallbackContext context)
        {
            // if player is dead, return
            ToggleMenuAndPause();
        }

        public void ToggleMenuAndPause()
        {
            if(GameManager.Instance.currentGameState == GameManager.GameState.Playing)
            {
                GameManager.Instance.currentGameState = GameManager.GameState.Paused;
                optionsMenu.SetActive(true);
            }
            else
            {
                GameManager.Instance.currentGameState = GameManager.GameState.Playing;
                optionsMenu.SetActive(false);
            }
        }

        private void RestartGame(InputAction.CallbackContext context)
        {
            if (playerStats == null)
            {
                Debug.LogError("playerStats is null");
            }
            if (!playerStats.AliveState)
            {
                LevelManager.Instance.ReloadCurrentScene();
            }
        }

        // Called when Shift is pressed down (start sprinting)
        private void OnSprintStarted(InputAction.CallbackContext context)
        {
            isSprinting = true;
        }

        // Called when Shift is released (stop sprinting)
        private void OnSprintStopped(InputAction.CallbackContext context)
        {
            isSprinting = false;
        }
    }
}