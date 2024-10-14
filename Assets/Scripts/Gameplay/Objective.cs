using UnityEngine;
using WallThrough.Events;

namespace WallThrough.Gameplay
{
    public enum ObjectiveType
    {
        WallPuzzle,
        Collectable,
        MiniPuzzle
    }

    public class Objective : MonoBehaviour
    {
        public bool IsCompleted { get; private set; } = false;

        protected AudioSource src;
        public ObjectiveType Type { get; private set; }

        protected void SetObjectiveType(ObjectiveType type)
        {
            Type = type;
        }

        public ObjectiveType GetObjectiveType()
        {
            return Type;
        }

        public virtual void CompleteObjective()
        {
            IsCompleted = true;
            //Debug.Log($"{gameObject.name} completed!");
        }
    }
}