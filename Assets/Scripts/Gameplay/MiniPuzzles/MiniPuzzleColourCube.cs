using UnityEngine;
using WallThrough.UI;
using WallThrough.Generation;

namespace WallThrough.Gameplay
{
    public class MiniPuzzleColourCube : MiniPuzzle
    {
        [Header("Pressure Plates")]
        [SerializeField] private GameObject pressurePlatePrefab; // Prefab for the pressure plate
        private ColourCodeManager colourCodeManager; // Reference to the ColourCodeManager

        private GameObject parentObject; // Store reference to parentObject for FlashCode

        private void Awake()
        {
            if (!colourCodeManager) colourCodeManager = FindObjectOfType<ColourCodeManager>();
        }

        public override void Initialize(int[] colourCodes)
        {
            if (!colourCodeManager)
            {
                Debug.LogWarning("No colourcodemanager found");
                return;
            }
            parentObject = colourCodeManager.Initialize(colourCodes);
            SpawnPressurePlate();
        }

        private void SpawnPressurePlate()
        {     
            // Attempt to get the RoomBehaviour component from the parent object
            if (transform.parent.TryGetComponent(out RoomBehaviour roomBehaviour))
            {
                // If roomBehaviour is found, spawn the pressure plate at the room center
                Vector3 roomCenter = roomBehaviour.GetRoomCentre();

                roomCenter.y -= 5f;

                // Spawn the pressure plate at the room center
                GameObject pressurePlate = Instantiate(pressurePlatePrefab, roomCenter, Quaternion.identity);
                pressurePlate.transform.SetParent(transform);
                pressurePlate.AddComponent<PressurePlate>().Initialize(colourCodeManager, parentObject); // Pass parentObject
            }
            else
            {
                // If no RoomBehaviour found, spawn the pressure plate at (0,0,0)
                Debug.LogWarning("RoomBehaviour component not found on parent object.");
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
