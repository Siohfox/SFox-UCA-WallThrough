using UnityEngine;
using UnityEngine.InputSystem;

public class DragAndDrop : MonoBehaviour
{
    private Camera mainCamera;
    private float distanceToCamera;
    private bool isDragging = false;
    private Vector3 offset;
    private float originalY; // Fixed Y position for dragging

    [SerializeField] private PlugSocket plugSocketScript; // Reference to the plug-socket logic
    [SerializeField] private InputActionReference clickAndDragActionReference; // Drag input action


    private void Start()
    {
        mainCamera = Camera.main;
        distanceToCamera = Vector3.Distance(transform.position, mainCamera.transform.position);
        originalY = transform.position.y;
    }

    private void Update()
    {
        if (clickAndDragActionReference.action.ReadValue<float>() > 0) // Mouse or touch input detected
        {
            if (!isDragging && IsPointerOverObject())
            {
                StartDragging();
            }

            if (isDragging)
            {
                HandleDragging();
            }
        }
        else if (isDragging)
        {
            StopDragging();
        }
    }

    private bool IsPointerOverObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.transform == transform;
        }

        return false;
    }

    private void StartDragging()
    {
        Vector3 currentPosition = transform.position;
        Vector3 pointerPos = GetPointerPosition();

        offset = currentPosition - pointerPos;
        isDragging = true;
    }

    private void HandleDragging()
    {
        // Get pointer position
        Vector3 pointerPos = GetPointerPosition();
        Vector3 newPosition = new Vector3(pointerPos.x + offset.x, originalY, pointerPos.z + offset.z);

        // Check snapping/unsnapping condition
        plugSocketScript.CheckSnap(newPosition);

        // Update the position of the plug
        if (plugSocketScript.IsPluggedIn())
        {
            // Keep it snapped to the outlet
            transform.position = plugSocketScript.GetOutletPosition();
        }
        else
        {
            // Follow the pointer if not snapped
            transform.position = newPosition;
        }
    }

    private void StopDragging()
    {
        isDragging = false;
    }

    private Vector3 GetPointerPosition()
    {
        if (clickAndDragActionReference.action.activeControl.device is Mouse)
        {
            return mainCamera.ScreenToWorldPoint(new Vector3(Mouse.current.position.ReadValue().x, Mouse.current.position.ReadValue().y, distanceToCamera));
        }
        else if (clickAndDragActionReference.action.activeControl.device is Touchscreen)
        {
            return mainCamera.ScreenToWorldPoint(new Vector3(Touchscreen.current.primaryTouch.position.ReadValue().x, Touchscreen.current.primaryTouch.position.ReadValue().y, distanceToCamera));
        }

        return Vector3.zero;
    }
}
