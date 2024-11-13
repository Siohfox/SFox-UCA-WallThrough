using System;
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
        public GameObject collectablePrefab;
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
                if (cell.status[i] && cell.doorSpawnDirection == (Direction)i && cell.RoomType != RoomType.Final)  // Check for final room
                {
                    if (quickTimeWallPrefab)
                    {
                        GameObject quickTimeWall = Instantiate(quickTimeWallPrefab, doors[i].transform.position + new Vector3(0, 2, 0), doors[i].transform.rotation);
                        quickTimeWall.transform.SetParent(transform);
                    }
                }
            }

            if (cell.RoomType == RoomType.Final)
            {
                PlaceExitPortal();
            }

            if (cell.RoomType == RoomType.Collectable)
            {
                PlaceCollectable();
            }
        }

        private void PlaceCollectable()
        {
            if (collectablePrefab)
            {
                Vector3 collectablePosition = GetRoomCentre() + new Vector3(0, 4);
                Instantiate(collectablePrefab, collectablePosition, Quaternion.identity, transform);
            }
            else
            {
                Debug.LogWarning("No collectable prefab assigned to RoomBehaviour");
            } 
        }

        private void PlaceExitPortal()
        {
            if (exitPortalPrefab)
            {
                // This method can be used in RoomBehaviour to place the boss within the room itself
                Vector3 portalPosition = GetRoomCentre() + new Vector3(0, 4); // Use room's center position for boss placement
                Instantiate(exitPortalPrefab, portalPosition, Quaternion.identity, transform);
            }
            else
            {
                Debug.LogWarning("No exit portal prefab assigned to RoomBehaviour");
            }
            
        }

        public Vector3 GetRoomCentre() => new(transform.position.x + roomSize.x / 2, 0, transform.position.z - roomSize.z / 2);
    }
}