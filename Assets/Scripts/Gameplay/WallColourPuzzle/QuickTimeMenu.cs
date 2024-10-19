using System;
using System.Collections.Generic;
using UnityEngine;
using static WallThrough.Gameplay.QuickTimeWall;
using WallThrough.Audio;
using WallThrough.Utility;

namespace WallThrough.Gameplay
{
    public class QuickTimeMenu : MonoBehaviour
    {
        private int requiredInputs;
        public List<int> codeInput = new();
        private QuickTimeWall quickTimeWallScript;

        [SerializeField]
        private AudioClip buttonClick;
        private AudioSource src;

        private void Start()
        {
            src = GetComponent<AudioSource>();
            if (!src)
            {
                Debug.LogWarning("No audio source found");
            }
        }

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
                var colourData = ColourManager.Instance.GetColourData(code);
                colourNames.Add(colourData.colourName);
            }

            Debug.Log("Your input: " + string.Join(" ", colourNames));
            quickTimeWallScript.CompareCodes(codeInput);
            ClearInput();
        }

        // Public method to input colour codes
        public void InputColour(int colour)
        {
            codeInput.Add(colour);
            AudioManager.Instance.PlaySound(buttonClick, 1.0f, src);
        }

        public void ClearInput()
        {
            codeInput.Clear();
            //Debug.Log("Clearing input");
        }

        // Set the current wall being interacted with and get its script
        public void SetCurrentWall(QuickTimeWall wall, int requiredInputs)
        {
            quickTimeWallScript = wall;
            this.requiredInputs = requiredInputs;
        }
    }
}
