using UnityEngine;
using WallThrough.Utility;

namespace WallThrough.Gameplay
{
    public enum ObjectiveType
    {
        WallPuzzle,
        Collectable,
    }

    public class Objective : MonoBehaviour
    {
        public bool IsCompleted { get; private set; } = false;
        public ObjectiveManager objectiveManager;

        protected AudioSource src;
        public ObjectiveType Type { get; private set; }

        protected void SetObjectiveType(ObjectiveType type)
        {
            Type = type;
        }

        public ObjectiveType GetObjectiveType() => Type;

        public virtual void CompleteObjective()
        {
            IsCompleted = true;
            //Debug.Log($"{gameObject.name} completed!");
        }
    }
}