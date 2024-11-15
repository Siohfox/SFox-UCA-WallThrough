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

            if (cell.RoomType == RoomType.Collectable)
            {
                PlaceCollectable(3);
            }
        }

        private void PlaceCollectable(int amount)
        {
            float padding = 4f;

            if (collectablePrefab)
            {
                for (int i = 0; i < amount; i++)
                {
                    // Random position within room boundaries (from the corner to the max size)
                    float xPos = UnityEngine.Random.Range(transform.position.x + padding, transform.position.x + roomSize.x - padding);
                    float zPos = UnityEngine.Random.Range(transform.position.z - roomSize.z + padding, transform.position.z - padding); // negative z direction

                    // Set y position to a fixed height (2 as per your previous code)
                    Vector3 collectablePosition = new Vector3(xPos, 2, zPos);

                    // Instantiate the collectable at the random position
                    Instantiate(collectablePrefab, collectablePosition, Quaternion.identity, transform);
                }
            }
            else
            {
                Debug.LogWarning("No collectable prefab assigned to RoomBehaviour");
            }
        }

        public Vector3 GetRoomCentre() => new(transform.position.x + roomSize.x / 2, 0, transform.position.z - roomSize.z / 2);
    }
}