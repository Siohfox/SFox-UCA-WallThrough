using WallThrough.Gameplay.Interactable;
using UnityEngine;
using WallThrough.Audio;
using WallThrough.Utility;

namespace WallThrough.Gameplay
{
    public class Collectable : Objective, IInteractable
    {
        [SerializeField]
        private AudioClip collectClip;

        private PassiveObjectMovement passiveMovement;

        private void Awake()
        {
            src = GetComponent<AudioSource>();
            if (!src) Debug.LogWarning("No audio source found");

            SetObjectiveType(ObjectiveType.Collectable);

            passiveMovement = GetComponent<PassiveObjectMovement>();
            if (!passiveMovement) Debug.LogWarning("PassiveObjectMovement component not found!");
        }

        private void Update()
        {
            // The passive movement logic is now handled by PassiveObjectMovement
        }

        public void InteractionStart()
        {
            if (!IsCompleted) // Ensure the objective isn't already completed
            {
                // Play sound effect
                AudioManager.Instance.PlaySoundAtPosition(collectClip, 1.0f, transform.position);

                // Complete the objective
                base.CompleteObjective();

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
