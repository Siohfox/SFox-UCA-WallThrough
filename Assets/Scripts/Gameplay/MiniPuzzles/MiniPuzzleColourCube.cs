using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace WallThrough.Gameplay
{
    public class MiniPuzzleColourCube : MonoBehaviour
    {
        [SerializeField] private GameObject parentPrefabHolder; // Empty GameObject to hold parent prefabs
        [SerializeField] private GameObject parentPrefab; // Prefab for the parent object holding the images
        [SerializeField] private GameObject imagePrefab; // Prefab for the colored images (UI)
        [SerializeField] private Transform[] pressurePlateLocations; // Array of Transforms for pressure plate locations
        [SerializeField] private GameObject pressurePlatePrefab; // Prefab for the pressure plate
        private GameObject parentObject; // Reference to the instantiated parent object

        public void InstantiateCubes(int[] colourCodes)
        {
            // Create a new parent object for the images under the parentPrefabHolder
            parentObject = Instantiate(parentPrefab, parentPrefabHolder.transform); // Instantiate the parent prefab under the holder

            // Clear existing images in the parent (if needed)
            foreach (Transform child in parentObject.transform)
            {
                Destroy(child.gameObject);
            }

            // Instantiate images based on the colourCodes length
            for (int i = 0; i < colourCodes.Length; i++)
            {
                // Instantiate the image as a child of the parent object
                Color color = ObjectiveManager.Instance.GetColourData(colourCodes[i]).colour; // Get the color from the manager
                GameObject image = Instantiate(imagePrefab, parentObject.transform); // Instantiate image under the parent object

                // Set the image color (including alpha)
                Image imgComponent = image.GetComponent<Image>(); // Get the Image component
                if (imgComponent != null)
                {
                    color.a = 1; // Ensure alpha is set to 1 (fully opaque)
                    imgComponent.color = color; // Set the image color
                }

                // Adjust position of the image (if necessary)
                image.transform.localPosition = new Vector3(0, 0, i * 1.5f); // Adjust position as needed
            }

            // Set parentObject inactive initially
            parentObject.SetActive(false);

            // Spawn the pressure plate at a random location
            SpawnPressurePlate();
        }

        private void SpawnPressurePlate()
        {
            if (pressurePlateLocations.Length > 0)
            {
                // Select a random Transform from the assigned pressure plate locations
                int randomIndex = Random.Range(0, pressurePlateLocations.Length);
                Transform selectedLocation = pressurePlateLocations[randomIndex];

                // Instantiate the pressure plate at the selected location
                GameObject pressurePlate = Instantiate(pressurePlatePrefab, selectedLocation.position, Quaternion.identity);
                pressurePlate.AddComponent<PressurePlate>().Initialize(parentObject); // Add a PressurePlate component
            }
        }
    }

    public class PressurePlate : MonoBehaviour
    {
        private GameObject parentObject;

        public void Initialize(GameObject parent)
        {
            parentObject = parent; // Store the reference to the parent object
        }

        private void OnTriggerEnter(Collider other)
        {
            // Check if the player collides with the pressure plate
            if (other.CompareTag("Player"))
            {
                StartCoroutine(FlashCode());  
            }
        }

        private IEnumerator FlashCode()
        {
            parentObject.SetActive(true);
            yield return new WaitForSeconds(2f);
            parentObject.SetActive(false);
        }
    }
}
