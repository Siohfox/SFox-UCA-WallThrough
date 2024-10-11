using System;
using System.Collections.Generic;
using UnityEngine;
using static WallThrough.Gameplay.QuickTimeWall;

namespace WallThrough.Gameplay
{
    public class QuickTimeMenu : MonoBehaviour
    {
        private int requiredInputs;
        public List<int> codeInput = new();
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
            List<string> colourNames = new();
            foreach (int code in codeInput)
            {
                colourNames.Add(Enum.GetName(typeof(ColourMap), code));
            }

            //Debug.Log("Your input: " + string.Join(" ", colourNames));
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
            Debug.Log("Clearing input");
        }

        // Set the current wall being interacted with and get its script
        public void SetCurrentWall(GameObject wallObject, int requiredInputs)
        {
            quickTimeWallScript = wallObject.GetComponent<QuickTimeWall>();
            this.requiredInputs = requiredInputs; 
        }
    }
}
