using System.Collections.Generic;
using UnityEngine;

namespace WallThrough.Gameplay
{
    public class Room : MonoBehaviour
    {
        public List<Room> adjacentRooms; // List of connected rooms
        public bool IsFlooded { get; set; } = false;

        // Call this method to set the water level in the room
        public void SetWaterLevel(float fillAmount)
        {
            // Implement the logic to visually represent the water level
            // For example, change the scale of a water GameObject or adjust a material's property
        }

        public List<Room> GetAdjacentRooms()
        {
            return adjacentRooms;
        }
    }
}
