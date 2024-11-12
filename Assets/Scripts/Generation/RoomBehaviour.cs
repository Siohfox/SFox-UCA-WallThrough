using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WallThrough.Generation
{
    public enum Direction { Up, Down, Right, Left };

    public class RoomBehaviour : MonoBehaviour
    {
        public GameObject[] walls;
        public GameObject[] doors;
        public GameObject[] arches;
        public GameObject quickTimeWallPrefab;
        public GameObject exitPortalPrefab;
        public Vector3 roomSize = new(24, 0, 24);

        // Update is called once per frame
        //public void UpdateRoom(bool[] status, Direction doorSpawnDirection)
        public void UpdateRoom(DungeonGenerator.Cell cell)
        {
            for (int i = 0; i < cell.status.Length; i++)
            {
                doors[i].SetActive(cell.status[i]);
                walls[i].SetActive(!cell.status[i]);
                arches[i].SetActive(cell.status[i]);

                // Check if quick-time wall should be instantiated
                if (cell.status[i] && cell.doorSpawnDirection == (Direction)i && !cell.isFinalRoom)  // Check for final room
                {
                    if (quickTimeWallPrefab)
                    {
                        GameObject quickTimeWall = Instantiate(quickTimeWallPrefab, doors[i].transform.position + new Vector3(0, 2, 0), doors[i].transform.rotation);
                        quickTimeWall.transform.SetParent(transform);
                    }
                }
            }

            if (cell.isFinalRoom)
            {
                PlaceExitPortal();
            }
        }


        private void PlaceExitPortal()
        {
            // This method can be used in RoomBehaviour to place the boss within the room itself
            Vector3 portalPosition = GetRoomCentre() + new Vector3(0, 4); // Use room's center position for boss placement
            Instantiate(exitPortalPrefab, portalPosition, Quaternion.identity, transform);
        }

        public Vector3 GetRoomCentre() => new(transform.position.x + roomSize.x / 2, 0, transform.position.z - roomSize.z / 2);
    }
}