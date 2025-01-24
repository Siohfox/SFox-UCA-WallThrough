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
            musicSource = gameObject.GetComponent<AudioSource>();
            musicSource.volume = PlayerPrefs.GetFloat("musicVolume", 0.35f);
        }

        // Toggles playing of music
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

        // Plays music track
        public void PlayMusic(AudioClip clip, float volume)
        {
            musicSource.clip = clip;
            musicSource.volume = volume;
            musicSource.loop = true;
            musicSource.Play();
        }

        // Plays a sound at camera
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

        // Plays a sound at a 3D position
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
