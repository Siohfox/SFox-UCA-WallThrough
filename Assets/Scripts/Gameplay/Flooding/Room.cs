using UnityEngine;

namespace WallThrough.Gameplay
{
    public class Room : MonoBehaviour
    {
        public GameObject waterPlane; // Reference to the water plane in this room
        public bool IsFlooding = false;

        public void SetWaterLevel(float height)
        {
            // Set the water level position
            Vector3 newPosition = waterPlane.transform.position;
            newPosition.y = height; // Adjust for the water plane's height
            waterPlane.transform.position = newPosition;
        }
    }
}
