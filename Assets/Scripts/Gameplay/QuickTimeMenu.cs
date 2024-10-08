using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WallThrough.Gameplay.QuicktimeWall;

namespace WallThrough.Gameplay
{
    public class QuickTimeMenu : MonoBehaviour
    {
        public List<int> codeInput = new List<int>();
        public QuicktimeWall quickTimeWallScript;

        private void Update()
        {

            // Checks if you've input 4 colour codes already, if true: compare to door code
            if (codeInput.Count >= 4)
            {
                List<string> colourNames = new List<string>();
                foreach (int code in codeInput) colourNames.Add(Enum.GetName(typeof(ColourMap), code));

                Debug.Log("Your input: " + string.Join(" ", colourNames.ToArray()));
                quickTimeWallScript.CompareCodes(codeInput);
                codeInput.Clear();
            }
        }

        // Public Input for Buttons
        public void InputColour(int colour) => codeInput.Add(colour);

        // Sets the current wall as the one being interacted with, then gets its script
        public void SetCurrentWall(GameObject gameObject)
        {
            quickTimeWallScript = gameObject.GetComponent<QuicktimeWall>();
        }
    }
}

