using System;
using System.Collections.Generic;
using UnityEngine;

namespace WallThrough.Gameplay
{
    public class QuicktimeWall : MonoBehaviour
    {
        [SerializeField]
        private GameObject quickTimeMenu;

        // Enum representing the colors
        public enum ColourMap { Red, Orange, Yellow, Green, Blue, Purple };

        private int[] colourCode = new int[4];
        private string colourString;
        private bool isInteracting = false;

        // Start is called before the first frame update
        private void Start()
        {
            InitializeQuickTimeEvent();
        }

        // Initialize the quick time event
        private void InitializeQuickTimeEvent()
        {
            isInteracting = false;
            quickTimeMenu.SetActive(false);
            GenerateColourCode();
            DebugColourInfo();
        }

        // Generate a random colour code
        private void GenerateColourCode()
        {
            for (int i = 0; i < colourCode.Length; i++)
            {
                colourCode[i] = UnityEngine.Random.Range(0, Enum.GetValues(typeof(ColourMap)).Length);
            }
        }

        // Log colour names and their integer values
        private void DebugColourInfo()
        {
            List<string> colourNames = new List<string>();
            foreach (int code in colourCode)
            {
                colourNames.Add(Enum.GetName(typeof(ColourMap), code));
            }

            colourString = string.Join(" ", colourNames);
            string intResult = string.Join(" ", colourCode);
            Debug.Log("Colour Names: " + colourString);
            Debug.Log("Integer Values: " + intResult);
        }

        // Update is called once per frame
        private void Update()
        {
            if (!isInteracting) return;
        }

        private void OnCollisionEnter(Collision collision)
        {
            ActivateQuickTimeMenu();
        }

        private void OnCollisionExit(Collision collision)
        {
            DeactivateQuickTimeMenu();
        }

        // Activate the quick time menu
        private void ActivateQuickTimeMenu()
        {
            quickTimeMenu.SetActive(true);
            isInteracting = true;
            quickTimeMenu.GetComponent<QuickTimeMenu>().SetCurrentWall(this.gameObject);
        }

        // Deactivate the quick time menu
        private void DeactivateQuickTimeMenu()
        {
            quickTimeMenu.SetActive(false);
            isInteracting = false;
        }

        // Compare the input code with the generated colour code
        public void CompareCodes(List<int> codeInput)
        {
            for (int i = 0; i < colourCode.Length; i++)
            {
                if (codeInput[i] != colourCode[i])
                {
                    Debug.Log("Input was incorrect, correct input should've been: " + colourString);
                    return;
                }
            }

            Debug.Log("Input was correct, destroying door");
            DeactivateQuickTimeMenu();
            Destroy(transform.parent.gameObject);
        }
    }
}