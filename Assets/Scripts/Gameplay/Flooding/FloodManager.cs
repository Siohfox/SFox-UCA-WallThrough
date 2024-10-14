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
        public float transitionSpeed = 0.5f; // Speed of transition when balancing water levels higher is faster

        // This one should not be exposed to inspector
        private int test3;

        private void Start()
        {
            foreach(Room room in rooms)
            {
                room.SetWaterLevel(initialWaterHeight);
            }

            // Start flooding only the first room
            if (rooms.Length > 0)
            {
                rooms[0].IsFlooding = true; // Mark the first room as flooded
                rooms[0].SetWaterLevel(initialWaterHeight); // Set initial water height
                StartCoroutine(FloodRooms()); // Start the flooding coroutine
            }
        }

        private IEnumerator FloodRooms()
        {
            while (true)
            {
                foreach (Room room in rooms)
                {
                    if (room.IsFlooding)
                    {
                        float targetWaterHeight = room.GetCurrentWaterHeight() + floodRate * Time.deltaTime;

                        // Clamp the target water height to max
                        if (targetWaterHeight < maxWaterHeight)
                        {
                            room.SetWaterLevel(targetWaterHeight);
                        }
                        else
                        {
                            room.SetWaterLevel(maxWaterHeight); // Cap at max water height
                        }
                    }
                }

                yield return null; // Wait for the next frame
            }
        }

        public void OpenDoor(Room room)
        {
            room.IsFlooding = true; // Start flooding the room when the door is opened
            StartCoroutine(BalanceWaterLevels(room));
        }

        private IEnumerator BalanceWaterLevels(Room newRoom)
        {
            // Gather the current water heights of all flooded rooms
            float totalWaterHeight = 0f;
            int floodedRoomCount = 0;

            foreach (Room room in rooms)
            {
                if (room.IsFlooding)
                {
                    totalWaterHeight += room.GetCurrentWaterHeight();
                    floodedRoomCount++;
                }
            }

            // Calculate the new average water height if there are flooded rooms
            if (floodedRoomCount > 0)
            {
                float averageWaterHeight = totalWaterHeight / floodedRoomCount;

                // Set the target height for the new room and the average height for previous rooms
                float targetNewRoomHeight = Mathf.Min(averageWaterHeight, maxWaterHeight);
                float targetPreviousHeight = averageWaterHeight;

                // Gradually adjust water levels
                while (Mathf.Abs(newRoom.GetCurrentWaterHeight() - targetNewRoomHeight) > 0.01f ||
                       Mathf.Abs(GetLowestFloodedRoomHeight() - targetPreviousHeight) > 0.01f)
                {
                    // Raise the new room's water level
                    float newHeight = Mathf.MoveTowards(newRoom.GetCurrentWaterHeight(), targetNewRoomHeight, transitionSpeed * Time.deltaTime);
                    newRoom.SetWaterLevel(newHeight);

                    // Lower previous rooms' water levels
                    foreach (Room room in rooms)
                    {
                        if (room != newRoom && room.IsFlooding)
                        {
                            float previousHeight = Mathf.MoveTowards(room.GetCurrentWaterHeight(), targetPreviousHeight, transitionSpeed * Time.deltaTime);
                            room.SetWaterLevel(previousHeight);
                        }
                    }

                    yield return null; // Wait for the next frame
                }
            }
        }

        private float GetLowestFloodedRoomHeight()
        {
            float lowestHeight = float.MaxValue;

            foreach (Room room in rooms)
            {
                if (room.IsFlooding && room.GetCurrentWaterHeight() < lowestHeight)
                {
                    lowestHeight = room.GetCurrentWaterHeight();
                }
            }

            return lowestHeight;
        }
    }
}