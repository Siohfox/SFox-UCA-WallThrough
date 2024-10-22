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
        private int completedObjectives = 0; // Count of completed objectives

        public static event Action<string> OnObjectiveCompleted; // Event for when an objective is completed
        public static event Action<string> OnObjectiveUpdate; // Event for updating objectives

        public List<ColourData> colourData; // List of color data

        [SerializeField] private Dictionary<Objective, int[]> colourCodes = new(); // Dictionary to store colour codes for each wall-objective

        /// <summary>
        /// Updates the count of completed objectives and invokes the corresponding event.
        /// </summary>
        /// <param name="objective">The name of the completed objective.</param>
        public static void UpdateCompletedObjectives(string objective)
        {
            OnObjectiveCompleted?.Invoke(objective);
        }

        /// <summary>
        /// Triggers an objective update event with the provided message.
        /// </summary>
        /// <param name="message">The message to send with the objective update.</param>
        public static void TriggerObjectiveUpdate(string message)
        {
            OnObjectiveUpdate?.Invoke(message);
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
        }

        private void Start()
        {
            objectives = new List<Objective>(FindObjectsOfType<Objective>());
            foreach (var objective in objectives)
            {
                objective.objectiveManager = this;
            }
            UpdateObjectiveCompletion();
            UpdateCompletedObjectives(completedObjectives.ToString());
        }

        /// <summary>
        /// Checks if all objectives are completed.
        /// </summary>
        /// <returns>True if all objectives are completed; otherwise, false.</returns>
        public bool CheckObjectives()
        {
            completedObjectives = 0;
            UpdateObjectiveCompletion();

            return completedObjectives >= objectives.Count; // Return true if all objectives are completed
        }

        /// <summary>
        /// Updates the count of completed objectives.
        /// </summary>
        private void UpdateObjectiveCompletion()
        {
            completedObjectives = 0;

            foreach (Objective objective in objectives)
            {
                if (objective.IsCompleted)
                {
                    completedObjectives++;

                    // If it's a QuickTimeWall, retrieve its colour code
                    if (objective is QuickTimeWall quickTimeWall)
                    {
                        int[] colourCode = GetColourCode(quickTimeWall);
                        List<string> colourNames = new();
                        foreach (int code in colourCode)
                        {
                            var colourData = GetColourData(code);
                            colourNames.Add(colourData.colourName);
                        }

                        // Log the colour code for the completed objective
                        Debug.Log($"Objective {objective.transform.parent.gameObject.name} colour code: {string.Join(", ", colourNames)}");
                    }
                }
            }
        }

        /// <summary>
        /// Returns the number of completed objectives.
        /// </summary>
        /// <returns>The count of completed objectives.</returns>
        public int GetCompeletedObjectives()
        {
            UpdateObjectiveCompletion();
            return completedObjectives;
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