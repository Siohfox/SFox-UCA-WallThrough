using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WallThrough.Gameplay
{
    public class MiniPuzzleColourCube : MonoBehaviour
    {
        [SerializeField] private GameObject cubePrefab; // Prefab for the colored cubes
        [SerializeField] private GameObject slotPrefab; // Prefab for the slot GameObjects
        private List<Transform> slots = new List<Transform>(); // List to hold the slots

        public void InstantiateCubes(int[] colourCodes)
        {
            // Clear existing slots
            foreach (Transform slot in slots)
            {
                Destroy(slot.gameObject);
            }
            slots.Clear();

            // Create new slots based on the colourCodes length
            for (int i = 0; i < colourCodes.Length; i++)
            {
                GameObject slot = Instantiate(slotPrefab, transform); // Create a new slot
                slot.name = "Slot" + (i + 1); // Naming the slot for clarity
                slot.transform.localPosition = new Vector3(0, 0, i * 1.5f); // Adjust position as needed
                slots.Add(slot.transform); // Add to the slots list

                // Instantiate the cube in the slot
                Color color = ColourManager.Instance.GetColourData(colourCodes[i]).colour; // Get the color from the manager
                GameObject cube = Instantiate(cubePrefab, slot.transform); // Instantiate cube at the slot
                cube.GetComponent<Renderer>().material.color = color; // Set the cube color
            }
        }
    }
}