using System.Collections.Generic;
using UnityEngine;
using System;

namespace WallThrough.Gameplay
{
    [System.Serializable]
    public struct ColourData
    {
        public Color colour; // The color value
        public string colourName; // The name of the color
    }

    /// <summary>
    /// Tracks player progress through objectives in the game.
    /// </summary>
    public class ObjectiveManager : MonoBehaviour
    {
        public static ObjectiveManager Instance { get; private set; }

        public List<Objective> objectives; // List of objectives to track
        public int objectiveTotal = 0;
        public int completedObjectives = 0;
        public int completedWallObjectives = 0;
        public int wallObjectiveCount = 0;

        public List<ColourData> colourData; // List of color data

        [SerializeField] private Dictionary<Objective, int[]> colourCodes = new(); // Dictionary to store colour codes for each wall-objective

        private void OnEnable()
        {
            Objective.OnObjectiveCompleted += HandleObjectiveCompleted;
        }

        private void HandleObjectiveCompleted(Objective objective)
        {
            completedObjectives++;
            if(objective.GetObjectiveType() == ObjectiveType.WallPuzzle)
            {
                completedWallObjectives++;
                Debug.Log($"Completed wall objectives count = {completedWallObjectives}");
            }
        }

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

            CountObjectives();
        }

        private void CountObjectives()
        {
            // Correctly assigns this objective manager to all objectives without being a singleton
            objectives = new List<Objective>(FindObjectsOfType<Objective>());
            foreach (var objective in objectives)
            {
                objective.objectiveManager = this;
                objectiveTotal++;
                if (objective.GetObjectiveType() == ObjectiveType.WallPuzzle)
                {
                    wallObjectiveCount++;
                }
            }
        }

        /// <summary>
        /// Checks if all objectives are completed.
        /// </summary>
        /// <returns>True if all objectives are completed; otherwise, false.</returns>
        public bool CheckObjectives()
        {
            return completedObjectives >= objectives.Count; // Return true if all objectives are completed
        }

        /// <summary>
        /// Returns the number of completed objectives.
        /// </summary>
        /// <returns>The count of completed objectives.</returns>
        public int GetCompeletedObjectives()
        {
            return completedObjectives;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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