using System.Collections;
using UnityEngine;

namespace WallThrough.Gameplay
{
    public class WaterLevelManager : MonoBehaviour
    {
        public float waterLevel; // Current water level
        public float floodRate = 0.1f; // Rate at which the water level rises
        public GameObject waterPrefab; // Prefab for the water
        private GameObject currentWater; // Instance of the water

        public GameObject room; // Reference to the room

        void Start()
        {
            if (room != null)
            {
                // Get the room size from the collider
                Vector3 roomSize = room.GetComponent<Collider>().bounds.size; // Should return (20, 20, 20)
                waterLevel = 0;

                if (waterPrefab != null)
                {
                    // Instantiate the water as a child of the room
                    currentWater = Instantiate(waterPrefab, room.transform);

                    // Adjust scale based on room dimensions
                    currentWater.transform.localScale = new Vector3(roomSize.x * 0.1f, 1, roomSize.z * 0.1f);

                    // Position the water plane at the base of the room
                    currentWater.transform.localPosition = new Vector3(0, 0, 0); // Adjust if needed

                    StartFlooding();
                }
                else
                {
                    Debug.LogError("Water prefab is not assigned in the inspector!");
                }
            }
            else
            {
                Debug.LogError("Room reference is not assigned in the inspector!");
            }
        }

        void Update()
        {
            if (currentWater != null)
            {
                // Update water height based on waterLevel
                currentWater.transform.localScale = new Vector3(currentWater.transform.localScale.x, waterLevel, currentWater.transform.localScale.z);
                currentWater.transform.localPosition = new Vector3(0, waterLevel / 2, 0);
            }
        }

        public void StartFlooding()
        {
            StartCoroutine(Flood());
        }

        private IEnumerator Flood()
        {
            while (waterLevel < 5) // Max water level (adjust as needed)
            {
                waterLevel += floodRate * Time.deltaTime;
                yield return null; // Wait for the next frame
            }

            // Optionally, flood adjacent rooms if needed
            FloodAdjacentRoom();
        }

        private void FloodAdjacentRoom()
        {
            // Logic to find adjacent room and trigger flooding
            // Example implementation here
        }
    }
}