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

        // Update is called once per frame
        public void UpdateRoom(bool[] status)
        {
            for(int i = 0; i < status.Length; i++)
            {
                doors[i].SetActive(status[i]);
                walls[i].SetActive(!status[i]);
                arches[i].SetActive(status[i]);
            }
        }
    }
}