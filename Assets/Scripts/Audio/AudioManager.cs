using UnityEngine;

namespace WallThrough.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        private AudioSource musicSource;

        [SerializeField]
        private GameObject soundEffectPrefab;


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
        /// Plays a music clip with given clip
        /// </summary>
        /// <param name="clip"></param>
        public void PlayMusic(AudioClip clip, float volume)
        {
            musicSource.clip = clip;
            musicSource.volume = volume;
            musicSource.Play();
        }

        /// <summary>
        /// Plays a sound effect using the provided AudioSource at the given position
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="volume"></param>
        /// <param name="source"></param>
        public void PlaySound(AudioClip clip, float volume, AudioSource source)
        {
            try
            {
                if (source && clip)
                {
                    //Debug.Log($"playing sound {clip}");
                    source.volume = volume;
                    source.PlayOneShot(clip);
                }
                else
                {
                    Debug.LogWarning("Missing source or clip");
                    return;
                }
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Error in PlaySound: {e.Message}");
            }
        }

        public void PlaySoundAtPosition(AudioClip clip, float volume, Vector3 position)
        {
            // Instantiate the sound effect prefab at the specified position
            GameObject soundEffectInstance = Instantiate(soundEffectPrefab, position, Quaternion.identity);

            // Access the AudioSource component from the instantiated prefab
            AudioSource audioSource = soundEffectInstance.GetComponent<AudioSource>();

            if (audioSource != null)
            {
                audioSource.clip = clip;
                audioSource.volume = volume;
                audioSource.spatialBlend = 1.0f; // Set to 3D sound
                audioSource.Play();

                // Destroy the sound effect instance after it finishes playing
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