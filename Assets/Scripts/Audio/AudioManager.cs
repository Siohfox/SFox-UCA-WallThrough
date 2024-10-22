using UnityEngine;

namespace WallThrough.Audio
{
    /// <summary>
    /// Manages audio playback for music and sound effects in the game.
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        private AudioSource musicSource;

        [SerializeField]
        private GameObject soundEffectPrefab; // Prefab for sound effects

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.volume = PlayerPrefs.GetFloat("musicVolume", 0.35f);
        }

        /// <summary>
        /// Toggles playback of the music. Stops if playing, plays if stopped.
        /// </summary>
        public void StopPlayMusic()
        {
            if (musicSource.isPlaying)
            {
                musicSource.Stop();
            }
            else
            {
                musicSource.Play();
            }
        }

        /// <summary>
        /// Plays the specified music clip at the given volume.
        /// </summary>
        /// <param name="clip">The music clip to play.</param>
        /// <param name="volume">The volume at which to play the clip.</param>
        public void PlayMusic(AudioClip clip, float volume)
        {
            musicSource.clip = clip;
            musicSource.volume = volume;
            musicSource.Play();
        }

        /// <summary>
        /// Plays a sound effect using the provided AudioSource at the specified volume.
        /// </summary>
        /// <param name="clip">The sound effect clip to play.</param>
        /// <param name="volume">The volume at which to play the clip.</param>
        /// <param name="source">The AudioSource to play the sound effect from.</param>
        public void PlaySound(AudioClip clip, float volume, AudioSource source)
        {
            if (source && clip)
            {
                source.volume = volume;
                source.PlayOneShot(clip);
            }
            else
            {
                Debug.LogWarning("Missing source or clip");
            }
        }

        /// <summary>
        /// Plays a sound effect at a specific position in the world.
        /// </summary>
        /// <param name="clip">The sound effect clip to play.</param>
        /// <param name="volume">The volume at which to play the clip.</param>
        /// <param name="position">The position at which to play the sound effect.</param>
        public void PlaySoundAtPosition(AudioClip clip, float volume, Vector3 position)
        {
            GameObject soundEffectInstance = Instantiate(soundEffectPrefab, position, Quaternion.identity);
            AudioSource audioSource = soundEffectInstance.GetComponent<AudioSource>();

            if (audioSource != null)
            {
                audioSource.clip = clip;
                audioSource.volume = volume;
                audioSource.spatialBlend = 1.0f; // Set to 3D sound
                audioSource.Play();

                Destroy(soundEffectInstance, clip.length);
            }
            else
            {
                Debug.LogWarning("No AudioSource found on sound effect prefab!");
                Destroy(soundEffectInstance); // Clean up if there's no AudioSource
            }
        }
    }
}
