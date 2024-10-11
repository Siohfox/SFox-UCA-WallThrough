using UnityEngine;
using WallThrough.Gameplay.Interactable;
using WallThrough.Audio;

namespace WallThrough.Gameplay
{
    public class Objective : MonoBehaviour, IInteractable
    {
        public bool IsCompleted { get; private set; } = false;

        [SerializeField]
        private AudioClip completeSound;
        private AudioSource src;

        private void Start()
        {
            src = GetComponent<AudioSource>();
            if(!src)
            {
                Debug.LogWarning("No audio source found");
            }
        }

        public void InteractionStart()
        {
            Debug.Log("interaction starting");
            if (!IsCompleted)
            {
                AudioManager.Instance.PlaySound(completeSound, 1.0f, src);
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