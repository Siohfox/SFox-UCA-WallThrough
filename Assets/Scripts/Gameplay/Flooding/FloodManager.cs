using System.Collections;
using UnityEngine;

namespace WallThrough.Gameplay
{
    public class FloodManager : MonoBehaviour
    {
        public Room[] rooms; // Array of all rooms in the scene
        public float initialWaterHeight = 0f; // Initial water height for the first room
        public float floodRate = 0.1f; // Rate at which water rises per second
        public float maxWaterHeight = 10f; // Maximum water height for the room
        private float targetWaterHeight;

        private void Start()
        {
            // Start flooding only the first room
            if (rooms.Length > 0)
            {
                rooms[0].IsFlooding = true; // Mark the first room as flooded
                targetWaterHeight = initialWaterHeight; // Set the initial target water height
                StartCoroutine(FloodRooms()); // Start the flooding coroutine
            }
        }

        private IEnumerator FloodRooms()
        {
            while (true) // Keep flooding indefinitely or set a condition
            {
                foreach (Room room in rooms)
                {
                    if (room.IsFlooding)
                    {
                        // Check if the target water height is less than the maximum height
                        if (targetWaterHeight < maxWaterHeight)
                        {
                            room.SetWaterLevel(targetWaterHeight);
                        }
                        else
                        {
                            // Mark the room as flooding once max height is reached
                            room.IsFlooding = false; // or you might want a different flag
                            room.IsFlooding = true; // Assuming you add this flag in Room
                        }
                    }
                }

                // Increase water height gradually
                targetWaterHeight += floodRate * Time.deltaTime;
                yield return null; // Wait for the next frame
            }
        }

        public void OpenDoor(Room room)
        {
            room.IsFlooding = true; // Start flooding the room when the door is opened
        }
    }
}
