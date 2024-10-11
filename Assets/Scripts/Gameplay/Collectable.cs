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
                AudioManager.Instance.PlaySound(collectClip, 1.0f, src);
                base.CompleteObjective();
                Destroy(gameObject);
            }
        }

        public void InteractionEnd()
        {
            // Logic for ending the interaction
        }
    }
}

