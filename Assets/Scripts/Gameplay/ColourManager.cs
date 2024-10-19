using System.Collections.Generic;
using UnityEngine;

namespace WallThrough.Utility
{
    public class ColourManager : MonoBehaviour
    {
        [System.Serializable]
        public struct ColourData
        {
            public Color colour;
            public string colourName;
        }

        public static ColourManager Instance { get; private set; }

        public List<ColourData> colourData;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;

            // Initialize default colours if none assigned in Unity Inspector
            if (colourData == null || colourData.Count == 0)
            {
                colourData = new List<ColourData>
                {
                    new ColourData {colour = Color.red, colourName = "Red"},
                    new ColourData {colour = Color.blue, colourName = "Blue"},
                    new ColourData {colour = Color.green, colourName = "Green"}
                };
            }
        }

        public ColourData GetColourData(int index)
        {
            if (index >= 0 && index < colourData.Count)
            {
                return colourData[index];
            }

            Debug.LogError("Colour index out of range: " + index);
            return default;
        }
    }
}