using WallThrough.Gameplay.Interactable;
using UnityEngine;
using WallThrough.Audio;
using WallThrough.Events;

namespace WallThrough.Gameplay
{
    public class Collectable : Objective, IInteractable
    {
        [SerializeField]
        private AudioClip collectClip;

        private void Awake()
        {
            src = GetComponent<AudioSource>();
            if (!src) Debug.LogWarning("No audio source found");
        }

        public void InteractionStart()
        {
            if (!IsCompleted) // Ensure the objective isn't already completed
            {
                // Play sound effect
                AudioManager.Instance.PlaySoundAtPosition(collectClip, 1.0f, transform.position);

                // Complete the objective
                base.CompleteObjective();

                // Trigger the objective completed event
                if (ObjectiveManager.Instance)
                {
                    GameEvents.TriggerObjectiveCompleted(ObjectiveManager.Instance.GetCompeletedObjectives().ToString());
                }

                // Disable the collectable
                gameObject.SetActive(false);
            }
        }

        public void InteractionEnd()
        {
            // Logic for ending the interaction
        }
    }
}

