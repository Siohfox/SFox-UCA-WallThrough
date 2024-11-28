using UnityEngine;

public class PlugSocket : MonoBehaviour
{
    [SerializeField] private Transform plugTransform; // Reference to the plug's transform
    [SerializeField] private Transform outlet; // The socket position
    public float snapDistance = 0.5f; // Distance threshold for snapping
    private bool isPluggedIn = false; // Tracks if the plug is snapped
    

    [SerializeField] private WirePuzzle wirePuzzle; // Reference to the wire puzzle logic

    void Start()
    {
        if (wirePuzzle == null)
        {
            Debug.LogError("WirePuzzle script is not assigned.");
        }
    }

    public void CheckSnap(Vector3 currentPosition)
    {
        float distance = Vector3.Distance(currentPosition, outlet.position);

        if (!isPluggedIn && distance < snapDistance)
        {
            SnapToOutlet();
        }
        else if (isPluggedIn && distance > snapDistance)
        {
            UnSnapPlug();
        }
    }

    private void SnapToOutlet()
    {
        // Snap the plug to the outlet position
        plugTransform.position = outlet.position;
        isPluggedIn = true;

        // Notify the wire puzzle that the plug is snapped in place
        //wirePuzzle?.OnPuzzleComplete();
    }

    private void UnSnapPlug()
    {
        // Mark the plug as unsnapped
        isPluggedIn = false;
    }

    public bool IsPluggedIn()
    {
        return isPluggedIn;
    }

    public Vector3 GetOutletPosition()
    {
        return outlet.position;
    }
}
