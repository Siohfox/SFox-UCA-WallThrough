using System;
using System.Collections.Generic;
using UnityEngine;
using static WallThrough.Gameplay.QuicktimeWall;

namespace WallThrough.Gameplay
{
    public class QuickTimeMenu : MonoBehaviour
    {
        private const int RequiredInputs = 4; // Number of inputs needed
        public List<int> codeInput = new List<int>();
        private QuicktimeWall quickTimeWallScript;

        private void Update()
        {
            // Check if the required number of colour codes has been input
            if (codeInput.Count >= RequiredInputs)
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
            codeInput.Clear();
        }

        // Public method to input colour codes
        public void InputColour(int colour)
        {
            codeInput.Add(colour);
        }

        // Set the current wall being interacted with and get its script
        public void SetCurrentWall(GameObject wallObject)
        {
            quickTimeWallScript = wallObject.GetComponent<QuicktimeWall>();
        }
    }
}
