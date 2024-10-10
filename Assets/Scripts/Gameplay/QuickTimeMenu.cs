using System;
using System.Collections.Generic;
using UnityEngine;
using static WallThrough.Gameplay.QuickTimeWall;

namespace WallThrough.Gameplay
{
    public class QuickTimeMenu : MonoBehaviour
    {
        private int requiredInputs;
        public List<int> codeInput = new List<int>();
        private QuickTimeWall quickTimeWallScript;

        private void Update()
        {
            // Check if the required number of colour codes has been input
            if (codeInput.Count >= requiredInputs)
            {
                ProcessInput();
            }
        }

        // Process the input when enough colour codes have been entered
        private void ProcessInput()
        {
            List<string> colourNames = new List<string>();
            foreach (int code in codeInput)
            {
                colourNames.Add(Enum.GetName(typeof(ColourMap), code));
            }

            Debug.Log("Your input: " + string.Join(" ", colourNames));
            quickTimeWallScript.CompareCodes(codeInput);
            ClearInput();
        }

        // Public method to input colour codes
        public void InputColour(int colour)
        {
            codeInput.Add(colour);
        }

        public void ClearInput()
        {
            codeInput.Clear();
            Debug.Log("clearing input");
        }

        // Set the current wall being interacted with and get its script
        public void SetCurrentWall(GameObject wallObject, int requiredInputs)
        {
            quickTimeWallScript = wallObject.GetComponent<QuickTimeWall>();
            Debug.Log("Required inputs: " + requiredInputs);
            this.requiredInputs = requiredInputs; 
        }
    }
}
