using System;
using UnityEngine;

public class PlugSocket : MonoBehaviour
{
    public Transform outlet;
    public float snapDistance = 0.5f;  // How close the plug needs to be to the outlet to snap
    private bool isPluggedIn = false;
    private Vector3 originalPosition;
    private Transform plugCube;

    public DragAndDrop dragAndDropScript;

    [SerializeField] WirePuzzle wirePuzzle;

    void Start()
    {
        originalPosition = transform.position;
        plugCube = transform;
        if (!dragAndDropScript)
        {
            dragAndDropScript = GetComponent<DragAndDrop>();
        }
        if (!wirePuzzle)
        {
            Debug.LogError("No Wire puzzle script found");
            return;
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

    public void SnapToOutlet()
    {
        // Snap the plug cube to the outlet's position
        plugCube.position = outlet.position;
        isPluggedIn = true;

        // Notify the drag and drop script to update its snapped state
        dragAndDropScript.SetPluggedInState(true);

        wirePuzzle.OnPuzzleComplete();
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
