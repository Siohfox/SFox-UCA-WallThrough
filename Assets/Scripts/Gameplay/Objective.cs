using System;
using UnityEngine;

namespace WallThrough.Gameplay
{
    /// <summary>
    /// Defines the types of objectives available in the game.
    /// </summary>
    public enum ObjectiveType
    {
        WallPuzzle,
        Collectable,
    }

    /// <summary>
    /// Represents a game objective that can be completed.
    /// </summary>
    public class Objective : MonoBehaviour
    {
        /// <summary>
        /// Indicates whether the objective has been completed.
        /// </summary>
        public bool IsCompleted { get; private set; } = false;

        public static event Action<Objective> OnObjectiveCompleted;

        public ObjectiveManager objectiveManager; // Reference to the ObjectiveManager handling this objective

        protected AudioSource src; // Audio source for playing sounds related to the objective

        /// <summary>
        /// The type of the objective.
        /// </summary>
        public ObjectiveType Type { get; private set; }

        /// <summary>
        /// Sets the type of the objective.
        /// </summary>
        /// <param name="type">The type to assign to the objective.</param>
        protected void SetObjectiveType(ObjectiveType type)
        {
            Type = type;
        }

        /// <summary>
        /// Gets the current type of the objective.
        /// </summary>
        /// <returns>The type of the objective.</returns>
        public ObjectiveType GetObjectiveType() => Type;

        /// <summary>
        /// Marks the objective as completed.
        /// </summary>
        public virtual void CompleteObjective()
        {
            IsCompleted = true;
            UpdateCompletedObjectives(this);
        }

        /// <summary>
        /// Invokes the objective completed event.
        /// </summary>
        protected static void UpdateCompletedObjectives(Objective objective)
        {
            OnObjectiveCompleted?.Invoke(objective);
        }
    }
}
