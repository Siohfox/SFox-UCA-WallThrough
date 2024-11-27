using UnityEngine;

public class PlugSocket : MonoBehaviour
{
    public Transform outlet;  // Reference to the outlet object (the "wall")
    public float snapDistance = 0.5f;  // How close the plug needs to be to the outlet to snap
    private bool isPluggedIn = false;
    private Vector3 originalPosition;
    private Transform plugCube;  // Reference to the plug cube

    // Reference to DragAndDrop script
    public DragAndDrop dragAndDropScript;

    void Start()
    {
        originalPosition = transform.position;  // Store the original position of the plug
        plugCube = transform;  // This script should be attached to the plug (cube)
        if (!dragAndDropScript)
        {
            dragAndDropScript = GetComponent<DragAndDrop>();
        }
    }

    void Update()
    {
        // Calculate the distance between the plug and the outlet
        float distance = Vector3.Distance(plugCube.position, outlet.position);

        // Check if the plug cube is close enough to the outlet to snap
        if (!isPluggedIn && distance < snapDistance)
        {
            SnapToOutlet();
        }
        // If the plug is plugged in and we drag it away, unsnap it
        else if (isPluggedIn && distance > snapDistance)
        {
            UnSnapPlug();
        }
    }

    // Make these methods public to be accessible from other scripts
    public void SnapToOutlet()
    {
        // Snap the plug cube to the outlet's position
        plugCube.position = outlet.position;
        isPluggedIn = true;

        // Notify the drag and drop script to update its snapped state
        dragAndDropScript.SetPluggedInState(true);
    }

    public void UnSnapPlug()
    {
        // Reset the position of the plug cube to its original position
        plugCube.position = originalPosition;
        isPluggedIn = false;

        // Notify the drag and drop script to update its snapped state
        dragAndDropScript.SetPluggedInState(false);
    }

    public Vector3 GetOriginalPosition()
    {
        return originalPosition;
    }
}
