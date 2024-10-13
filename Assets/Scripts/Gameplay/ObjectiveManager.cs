using System.Collections.Generic;
using UnityEngine;
using WallThrough.Events;
using System;

namespace WallThrough.Gameplay
{
    // Tracks player progress
    public class ObjectiveManager : MonoBehaviour
    {
        public static ObjectiveManager Instance { get; private set; }

        public List<Objective> objectives;
        private int completedObjectives = 0;

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
            UpdateObjectiveCompletion();
            GameEvents.TriggerObjectiveCompleted(completedObjectives.ToString());
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