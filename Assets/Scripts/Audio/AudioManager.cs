using UnityEngine;

namespace WallThrough.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        private AudioSource musicSource;


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
                    Debug.Log($"playing sound {clip}");
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
    }
}