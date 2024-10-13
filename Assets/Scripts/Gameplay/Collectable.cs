using WallThrough.Gameplay.Interactable;
using UnityEngine;
using WallThrough.Audio;

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
            if (!IsCompleted)
            {
                // Use AudioManager to play the sound at the collectable's position
                AudioManager.Instance.PlaySoundAtPosition(collectClip, 1.0f, transform.position);

                base.CompleteObjective();
                gameObject.SetActive(false);
            }
        }

        public void InteractionEnd()
        {
            // Logic for ending the interaction
        }
    }
}

