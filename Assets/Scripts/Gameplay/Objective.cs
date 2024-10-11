using UnityEngine;

using WallThrough.Audio;

namespace WallThrough.Gameplay
{
    public class Objective : MonoBehaviour
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

        public virtual void CompleteObjective()
        {
            IsCompleted = true;
            AudioManager.Instance.PlaySound(completeSound, 1.0f, src);
            Debug.Log($"{gameObject.name} completed!");
        }
    }
}