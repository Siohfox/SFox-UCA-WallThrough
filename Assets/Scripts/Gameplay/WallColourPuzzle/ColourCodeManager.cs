using System.Collections.Generic;
using UnityEngine;

namespace WallThrough.Gameplay
{
    /// <summary>
    /// Manages the registration and retrieval of color codes associated with objectives.
    /// </summary>
    public class ColourCodeManager : MonoBehaviour
    {
        /// <summary>
        /// Singleton instance of the ColourCodeManager.
        /// </summary>
        public static ColourCodeManager Instance { get; private set; }

        [SerializeField]
        private Dictionary<Objective, int[]> colourCodes = new(); // Dictionary to store colour codes for each objective

        /// <summary>
        /// Ensures that there is only one instance of ColourCodeManager in the scene.
        /// </summary>
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

        /// <summary>
        /// Registers an objective with its associated color code.
        /// </summary>
        /// <param name="objective">The objective to register.</param>
        /// <param name="colourCode">The color code associated with the objective.</param>
        public void RegisterObjective(Objective objective, int[] colourCode)
        {
            if (!colourCodes.ContainsKey(objective))
            {
                colourCodes[objective] = colourCode;
            }
        }

        /// <summary>
        /// Retrieves the color code for a given objective.
        /// </summary>
        /// <param name="objective">The objective for which to get the color code.</param>
        /// <returns>The color code associated with the objective, or null if not found.</returns>
        public int[] GetColourCode(Objective objective)
        {
            return colourCodes.TryGetValue(objective, out var colourCode) ? colourCode : null;
        }
    }
}
