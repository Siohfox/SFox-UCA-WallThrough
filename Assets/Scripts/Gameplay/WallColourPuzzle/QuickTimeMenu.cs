using System.Collections.Generic;
using UnityEngine;
using WallThrough.Audio;

namespace WallThrough.Gameplay
{
    /// <summary>
    /// Manages the quick time menu for inputting color codes.
    /// </summary>
    public class QuickTimeMenu : MonoBehaviour
    {
        [SerializeField] private int requiredInputs;  // Number of inputs required to process
        [SerializeField] private List<int> codeInput = new();  // List to store color codes input
        [SerializeField] private AudioClip buttonClick;  // Sound played on button click

        private QuickTimeWall quickTimeWallScript;  // Reference to the current QuickTimeWall script
        private AudioSource audioSource;  // Reference to the audio source component

        /// <summary>
        /// Initializes the audio source component.
        /// </summary>
        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogWarning("No audio source found.");
            }
        }

        /// <summary>
        /// Checks if the required number of color codes has been input and processes them if so.
        /// </summary>
        private void Update()
        {
            if (codeInput.Count >= requiredInputs)
            {
                ProcessInput();
            }
        }

        /// <summary>
        /// Processes the input when enough color codes have been entered.
        /// </summary>
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

        /// <summary>
        /// Inputs a color code into the menu.
        /// </summary>
        /// <param name="colour">The color code to input.</param>
        public void InputColour(int colour)
        {
            codeInput.Add(colour);
            AudioManager.Instance.PlaySound(buttonClick, 1.0f, audioSource);
        }

        /// <summary>
        /// Clears the current input of color codes.
        /// </summary>
        public void ClearInput()
        {
            codeInput.Clear();
        }

        /// <summary>
        /// Sets the current wall being interacted with and the required number of inputs.
        /// </summary>
        /// <param name="wall">The QuickTimeWall script associated with the current wall.</param>
        /// <param name="requiredInputs">The number of inputs required to process.</param>
        public void SetCurrentWall(QuickTimeWall wall, int requiredInputs)
        {
            quickTimeWallScript = wall;
            this.requiredInputs = requiredInputs;
        }
    }
}
