using UnityEngine;
using WallThrough.UI;
using WallThrough.Generation;
using System;

namespace WallThrough.Gameplay
{
    public class MiniPuzzleColourCube : MiniPuzzle
    {
        [Header("Pressure Plates")]
        [SerializeField] private GameObject pressurePlatePrefab; // Prefab for the pressure plate

        private GameObject parentObject; // Store reference to parentObject for FlashCode

        [SerializeField] private bool tutorialState = false;

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
                GameObject pressurePlate = Instantiate(pressurePlatePrefab, roomCenter + new Vector3(0, 0.6f, 0), Quaternion.identity);
                pressurePlate.transform.SetParent(transform);
                pressurePlate.AddComponent<PressurePlate>().Initialize(colourCodeManager, parentObject, tutorialState); // Pass parentObject
            }
            else
            {
                Debug.Log("RoomBehaviour component not found on parent object. Generating a default...");

                Transform spawnLocation = transform.GetChild(0).transform;

                // Spawn the pressure plate at the room center
                GameObject pressurePlate = Instantiate(pressurePlatePrefab, spawnLocation.position + new Vector3(0,0.6f,0), Quaternion.identity);
                pressurePlate.transform.SetParent(transform);
                pressurePlate.AddComponent<PressurePlate>().Initialize(colourCodeManager, parentObject, tutorialState); // Pass parentObject
            }
        }
    }

    public class PressurePlate : MonoBehaviour
    {
        private ColourCodeManager colourCodeManager;
        private GameObject parentObject; // Reference to the parent object
        bool tutorialMode = false;      

        public void Initialize(ColourCodeManager colourCodeManager, GameObject parentObject, bool tutorialMode)
        {
            this.colourCodeManager = colourCodeManager;
            this.parentObject = parentObject; // Set the reference to parentObject
            this.tutorialMode = tutorialMode;

            parentObject.SetActive(false);

            if (tutorialMode)
            {
                transform.gameObject.AddComponent<TutorialHelper>();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            OnPuzzleComplete();
        }

        public void OnPuzzleComplete()
        {
            if (!parentObject) Debug.LogWarning("No parent object");
            else if (!colourCodeManager) Debug.LogWarning("No ColourCodeManager");
            else StartCoroutine(colourCodeManager.FlashCode(parentObject));
        }
    }
}
