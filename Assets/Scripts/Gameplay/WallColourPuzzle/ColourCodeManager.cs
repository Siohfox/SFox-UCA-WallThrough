using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WallThrough.Gameplay
{
    public class ColourCodeManager : MonoBehaviour
    {
        public static ColourCodeManager Instance { get; private set; }

        [SerializeField]
        private Dictionary<Objective, int[]> colourCodes = new();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        // Method to register an objective and its colour code
        public void RegisterObjective(Objective objective, int[] colourCode)
        {
            if (!colourCodes.ContainsKey(objective))
            {
                colourCodes[objective] = colourCode;
            }
        }

        // Method to get the colour code for an objective
        public int[] GetColourCode(Objective objective)
        {
            return colourCodes.TryGetValue(objective, out var colourCode) ? colourCode : null;
        }
    }
}

