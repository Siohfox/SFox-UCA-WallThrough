using System.Collections.Generic;
using UnityEngine;

namespace WallThrough.Gameplay
{
    public class Room : MonoBehaviour
    {
        public List<Room> adjacentRooms; // List of connected rooms
        public bool IsFlooded { get; set; } = false;

        public List<Room> GetAdjacentRooms()
        {
            return adjacentRooms;
        }
    }
}
