using UnityEngine;

namespace WallThrough.Gameplay
{
    public class Objective : MonoBehaviour
    {
        public bool IsCompleted { get; private set; } = false;

        protected AudioSource src;

        public virtual void CompleteObjective()
        {
            IsCompleted = true;
            Debug.Log($"{gameObject.name} completed!");
        }
    }
}