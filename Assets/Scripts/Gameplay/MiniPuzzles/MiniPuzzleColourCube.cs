using UnityEngine;
using WallThrough.UI;

namespace WallThrough.Gameplay
{
    public class MiniPuzzleColourCube : MonoBehaviour
    {
        [Header("Pressure Plates")]
        [SerializeField] private Transform[] pressurePlateLocations; // Array of Transforms for pressure plate locations
        [SerializeField] private GameObject pressurePlatePrefab; // Prefab for the pressure plate

        [SerializeField] private ColourCodeManager colourCodeManager; // Reference to the ColourCodeManager

        private GameObject parentObject; // Store reference to parentObject for FlashCode

        public void Initialize(int[] colourCodes)
        {
            // Initialize the ColourCodeManager
            parentObject = colourCodeManager.Initialize(colourCodes);

            // Spawn the pressure plate
            SpawnPressurePlate();
        }

        private void SpawnPressurePlate()
        {
            if (pressurePlateLocations.Length > 0)
            {
                int randomIndex = Random.Range(0, pressurePlateLocations.Length);
                Transform selectedLocation = pressurePlateLocations[randomIndex];

                GameObject pressurePlate = Instantiate(pressurePlatePrefab, selectedLocation.position, Quaternion.identity);
                pressurePlate.AddComponent<PressurePlate>().Initialize(colourCodeManager, parentObject); // Pass parentObject
            }
        }
    }

    public class PressurePlate : MonoBehaviour
    {
        private ColourCodeManager colourCodeManager;
        private GameObject parentObject; // Reference to the parent object

        public void Initialize(ColourCodeManager colourCodeManager, GameObject parentObject)
        {
            this.colourCodeManager = colourCodeManager;
            this.parentObject = parentObject; // Set the reference to parentObject

            parentObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                StartCoroutine(colourCodeManager.FlashCode(parentObject)); // Pass parentObject to FlashCode
            }
        }
    }
}
