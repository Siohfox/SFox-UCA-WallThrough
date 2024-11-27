using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    private Camera mainCamera;
    private float distanceToCamera;
    private bool isDragging = false;
    private Vector3 offset;
    private float originalY;  // Store the original Y position
    private bool isPluggedIn = false;  // Track if the plug is snapped to the outlet

    // Reference to the plug-socket logic
    public PlugSocket plugSocketScript;

    // Use InputActionReference to expose in the Inspector
    [SerializeField] private InputActionReference clickAndDragActionReference;

    private void Start()
    {
        // Cache the main camera
        mainCamera = Camera.main;
        // Set the initial distance to the camera (assuming the object is on a Z-plane of 0)
        distanceToCamera = Vector3.Distance(transform.position, mainCamera.transform.position);
        // Store the original Y position
        originalY = transform.position.y;
    }

    private void OnDisable()
    {
        // Disable the input action when the object is disabled
        clickAndDragActionReference.action.Disable();
    }

    private Vector3 GetMousePos()
    {
        // Get mouse position in world space
        return mainCamera.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, distanceToCamera));
    }

    private Vector3 GetTouchPos()
    {
        // Get touch position (assuming primary touch)
        if (Touchscreen.current.primaryTouch.press.isPressed)
        {
            return mainCamera.ScreenToWorldPoint(new Vector3(Touchscreen.current.primaryTouch.position.ReadValue().x, Touchscreen.current.primaryTouch.position.ReadValue().y, distanceToCamera));
        }
        return Vector3.zero;  // Return zero if no touch is detected
    }

    private void Update()
    {
        // Check if the ClickAndDrag action is performed (pressed)
        if (clickAndDragActionReference.action.ReadValue<float>() > 0)  // This will be > 0 when pressed
        {
            // If it's not already dragging, and we are clicking or touching on the object
            if (!isDragging && IsPointerOverObject() && !isPluggedIn)
            {
                StartDragging();
            }

            if (isDragging)
            {
                // Get the input position based on mouse or touch
                Vector3 inputPosition = Vector3.zero;
                if (clickAndDragActionReference.action.activeControl.device is Mouse)
                {
                    inputPosition = GetMousePos();
                }
                else if (clickAndDragActionReference.action.activeControl.device is Touchscreen)
                {
                    inputPosition = GetTouchPos();
                }

                // Update the position of the object while dragging, fixing the Y value
                transform.position = new Vector3(inputPosition.x + offset.x, originalY, inputPosition.z + offset.z);

                // Check if the plug is close enough to the outlet to snap
                if (!isPluggedIn && Vector3.Distance(transform.position, plugSocketScript.outlet.position) < plugSocketScript.snapDistance)
                {
                    plugSocketScript.SnapToOutlet();
                }

                // Check if the plug has moved too far away from the outlet
                if (isPluggedIn && Vector3.Distance(transform.position, plugSocketScript.outlet.position) > plugSocketScript.snapDistance)
                {
                    plugSocketScript.UnSnapPlug();
                }
            }
        }
        else if (isDragging)
        {
            StopDragging();
        }
    }

    private bool IsPointerOverObject()
    {
        // Use Raycast to check if the pointer is over the object
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform == transform;
        }

        return false;
    }

    private void StartDragging()
    {
        // Start dragging: Record the offset from the mouse/touch position to the object position
        Vector3 currentPosition = transform.position;
        Vector3 pointerPos = (clickAndDragActionReference.action.activeControl.device is Mouse)
            ? GetMousePos()
            : GetTouchPos();

        offset = currentPosition - pointerPos;
        isDragging = true;
    }

    private void StopDragging()
    {
        // Stop dragging when the mouse/touch is released
        isDragging = false;
    }

    // Method to be called by the PlugSocket script when snapped
    public void SetPluggedInState(bool pluggedIn)
    {
        isPluggedIn = pluggedIn;
    }
}
