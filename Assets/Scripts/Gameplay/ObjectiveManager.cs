using System.Collections.Generic;
using UnityEngine;
using System;

namespace WallThrough.Gameplay
{
    public struct ColourData
    {
        public Color colour;
        public string colourName;
    }

    public class ColourManager
    {
        private List<ColourData> GetColourDataList(List<Color> colours, List<string> colourStrings)
        {

        }

        public ColourData GetColourData(Objective objective)
        {
            return objective.ColourData;
        }
    }

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