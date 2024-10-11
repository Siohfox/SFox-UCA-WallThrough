
using WallThrough.Gameplay.Interactable;
using UnityEngine;

namespace WallThrough.Gameplay
{
    public class Collectable : Objective, IInteractable
    {
        public void InteractionStart()
        {
            if (!IsCompleted)
            {
                Destroy(gameObject);
                base.CompleteObjective();
            }
        }

        public void InteractionEnd()
        {
            // Logic for ending the interaction
        }
    }
}

