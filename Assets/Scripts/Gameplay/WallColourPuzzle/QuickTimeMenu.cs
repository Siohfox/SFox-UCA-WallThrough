using System.Collections.Generic;
using UnityEngine;
using WallThrough.Audio;
using UnityEngine.UI;

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
        [SerializeField] private List<Image> colorImages;  // UI Images to display color codes
        [SerializeField] private Color defaultColor = Color.white;  // Default color for images
        [SerializeField] private GameObject colorImagePrefab;  // Prefab for color images
        [SerializeField] private Transform colorImageContainer;  // Parent container for color images

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

            SetupColorImages();
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
                var colourData = ObjectiveManager.Instance.GetColourData(code);
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
            UpdateColorImages(colour);
            AudioManager.Instance.PlaySound(buttonClick, 1.0f, audioSource);
        }

        /// <summary>
        /// Clears the current input of color codes.
        /// </summary>
        public void ClearInput()
        {
            codeInput.Clear();
            for (int i = 0; i < colorImageContainer.childCount; i++)
            {
                // Get the Image component of each child and reset its color
                Image img = colorImageContainer.GetChild(i).GetComponent<Image>();
                img.color = defaultColor;  // Reset to default color
            }
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
            SetupColorImages();  // Ensure color images are set up for the new input requirement
        }

        /// <summary>
        /// Deactivates the quick time menu.
        /// </summary>
        public void DeactivateQuickTimeMenu()
        {
            gameObject.SetActive(false);
        }

        private void SetupColorImages()
        {
            // Clear existing images if any
            foreach (Transform child in colorImageContainer)
            {
                Destroy(child.gameObject);
            }

            // Instantiate new color images based on requiredInputs
            for (int i = 0; i < requiredInputs; i++)
            {
                GameObject newImage = Instantiate(colorImagePrefab, colorImageContainer);
                newImage.GetComponent<Image>().color = defaultColor;  // Set default color
            }
        }


        private void UpdateColorImages(int colour)
        {
            // Assuming you have a method to convert the color code to a Unity Color
            Color colorToDisplay = ObjectiveManager.Instance.GetColourData(colour).colour;
            colorToDisplay.a = 1f;

            if (codeInput.Count - 1 < colorImageContainer.childCount)
    {
                // Get the corresponding image from the container and update its color
                colorImageContainer.GetChild(codeInput.Count - 1).GetComponent<Image>().color = colorToDisplay;
            }
        }
    }
}
