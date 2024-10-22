using System.Collections.Generic;
using UnityEngine;

namespace WallThrough.Gameplay
{
    /// <summary>
    /// Manages color data for the game, providing access to predefined colors.
    /// </summary>
    public class ColourManager : MonoBehaviour
    {
        [System.Serializable]
        public struct ColourData
        {
            public Color colour; // The color value
            public string colourName; // The name of the color
        }

        public static ColourManager Instance { get; private set; }

        public List<ColourData> colourData; // List of color data

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this); // Ensure only one instance exists
                return;
            }
            Instance = this;

            // Initialize default colors if none are assigned in the Inspector
            if (colourData == null || colourData.Count == 0)
            {
                colourData = new List<ColourData>
                {
                    new ColourData { colour = Color.red, colourName = "Red" },
                    new ColourData { colour = Color.blue, colourName = "Blue" },
                    new ColourData { colour = Color.green, colourName = "Green" }
                };
            }
        }

        /// <summary>
        /// Retrieves the color data at the specified index.
        /// </summary>
        /// <param name="index">The index of the color data to retrieve.</param>
        /// <returns>The corresponding ColourData, or default if out of range.</returns>
        public ColourData GetColourData(int index)
        {
            if (index >= 0 && index < colourData.Count)
            {
                return colourData[index];
            }

            Debug.LogError("Colour index out of range: " + index);
            return default; // Return default ColourData if index is invalid
        }
    }
}
