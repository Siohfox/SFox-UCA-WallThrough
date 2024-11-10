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
        public Vector3 roomSize = new Vector3(24, 0, 24);

        // Update is called once per frame
        public void UpdateRoom(bool[] status, Direction doorSpawnDirection)
        {
            for(int i = 0; i < status.Length; i++)
            {
                doors[i].SetActive(status[i]);
                walls[i].SetActive(!status[i]);
                arches[i].SetActive(status[i]);

                if (status[i] && doorSpawnDirection == (Direction)i)
                {
                    if (quickTimeWallPrefab)
                    {
                        GameObject quickTimeWall = Instantiate(quickTimeWallPrefab, doors[i].transform.position + new Vector3(0,2,0), doors[i].transform.rotation);
                        quickTimeWall.transform.SetParent(transform);
                    } 
                }
            }
        }

        public Vector3 GetRoomCentre() => new Vector3(transform.position.x + roomSize.x / 2, 0, transform.position.z - roomSize.z / 2);
    }
}