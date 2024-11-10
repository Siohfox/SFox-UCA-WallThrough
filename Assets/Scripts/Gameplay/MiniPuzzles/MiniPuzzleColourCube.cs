using UnityEngine;
using WallThrough.UI;

namespace WallThrough.Gameplay
{
    public class MiniPuzzleColourCube : MonoBehaviour
    {
        [Header("Pressure Plates")]
        [SerializeField] private Transform pressurePlateLocation; // Array of Transforms for pressure plate locations
        [SerializeField] private GameObject pressurePlatePrefab; // Prefab for the pressure plate

        [SerializeField] private ColourCodeManager colourCodeManager; // Reference to the ColourCodeManager

        private GameObject parentObject; // Store reference to parentObject for FlashCode

        public void Initialize(int[] colourCodes)
        {
            if (colourCodeManager)
            {
                // Initialize the ColourCodeManager
                parentObject = colourCodeManager.Initialize(colourCodes);
            }
            else
            {
                try
                {
                    colourCodeManager = FindObjectOfType<ColourCodeManager>();

                    parentObject = colourCodeManager.Initialize(colourCodes);
                }
                catch
                {
                    Debug.LogWarning("Colour code manager not found");
                }
            }

            // Spawn the pressure plate
            SpawnPressurePlate();
        }

        private void SpawnPressurePlate()
        {

            Transform selectedLocation = transform.parent.parent;

            GameObject pressurePlate = Instantiate(pressurePlatePrefab, selectedLocation.position, Quaternion.identity);
            pressurePlate.transform.SetParent(transform);
            pressurePlate.AddComponent<PressurePlate>().Initialize(colourCodeManager, parentObject); // Pass parentObject
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
