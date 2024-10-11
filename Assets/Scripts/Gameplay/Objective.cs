using UnityEngine;
using WallThrough.Gameplay.Interactable;

namespace WallThrough.Gameplay
{
    public class Objective : MonoBehaviour, IInteractable
    {
        public bool IsCompleted { get; private set; } = false;

        public void InteractionStart()
        {
            Debug.Log("interaction starting");
            if (!IsCompleted)
            {
                CompleteObjective();
            }
        }

        public void InteractionEnd()
        {
            // Logic for ending the interaction
        }

        public void CompleteObjective()
        {
            IsCompleted = true;
            Debug.Log($"{gameObject.name} completed!");
        }
    }
}