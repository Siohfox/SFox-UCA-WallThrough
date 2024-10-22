using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WallThrough.Gameplay
{
    public class MiniPuzzleColourCube : MonoBehaviour
    {
        [SerializeField] private GameObject cubePrefab; // Prefab for the colored cubes
        [SerializeField] private Transform cubeContainer; // Parent object to hold the cubes

        public void InstantiateCubes(int[] colourCodes)
        {
            // Clear any existing cubes
            foreach (Transform child in cubeContainer)
            {
                Destroy(child.gameObject);
            }

            // Instantiate cubes based on the provided colour codes
            foreach (int code in colourCodes)
            {
                Color color = ColourManager.Instance.GetColourData(code).colour; // Assume ColourData has a Color field
                GameObject cube = Instantiate(cubePrefab, cubeContainer);
                cube.GetComponent<Renderer>().material.color = color; // Set the cube color
            }
        }
    }

}