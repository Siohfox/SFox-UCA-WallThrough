using System.Collections.Generic;
using UnityEngine;

namespace WallThrough.Gameplay
{
    // Tracks player progress
    public class ObjectiveManager : MonoBehaviour
    {
        public List<Objective> objectives;
        private int completedObjectives = 0;

        private void Start()
        {
            objectives = new List<Objective>(FindObjectsOfType<Objective>());
        }
        private void Update()
        {
            CheckObjectives();
        }

        private void CheckObjectives()
        {
            completedObjectives = 0;

            foreach(Objective objective in objectives)
            {
                if(objective.IsCompleted)
                {
                    completedObjectives++;
                }
            }

            if(completedObjectives >= objectives.Count)
            {
                OpenEndPortal();
            }
        }

        private void OpenEndPortal()
        {
            Debug.Log("All objectives completed, opening portal!");
        }
    }
}