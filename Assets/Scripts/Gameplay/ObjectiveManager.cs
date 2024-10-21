using System.Collections.Generic;
using UnityEngine;
using System;
using WallThrough.Utility;

namespace WallThrough.Gameplay
{
    // Tracks player progress
    public class ObjectiveManager : MonoBehaviour
    {
        public static ObjectiveManager Instance { get; private set; }

        public List<Objective> objectives;
        private int completedObjectives = 0;

        public static event Action<string> OnObjectiveCompleted;
        public static event Action<string> OnObjectiveUpdate;

        public static void UpdateCompletedObjectives(string objective)
        {
            OnObjectiveCompleted?.Invoke(objective);
        }

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

        public bool CheckObjectives()
        {
            completedObjectives = 0;

            UpdateObjectiveCompletion();

            if (completedObjectives >= objectives.Count)
            {
                return true;
            }

            return false;
        }

        private void UpdateObjectiveCompletion()
        {
            completedObjectives = 0;

            foreach (Objective objective in objectives)
            {
                if (objective.IsCompleted)
                {
                    completedObjectives++;

                    // If it's a QuickTimeWall, get its colour code
                    if (objective is QuickTimeWall quickTimeWall)
                    {
                        int[] colourCode = ColourCodeManager.Instance.GetColourCode(quickTimeWall);
                        List<string> colourNames = new();
                        foreach (int code in colourCode)
                        {
                            var colourData = ColourManager.Instance.GetColourData(code);
                            colourNames.Add(colourData.colourName);
                        }
                        // Use colourCode as needed, e.g., logging or processing
                        Debug.Log($"Objective {objective.transform.parent.gameObject.name} colour code: {string.Join(", ", colourNames)}");
                    }
                }
            }
        }

        public int GetCompeletedObjectives()
        {
            UpdateObjectiveCompletion();
            return completedObjectives;
        }
    }
}