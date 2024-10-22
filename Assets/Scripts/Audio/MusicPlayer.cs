using UnityEngine;

namespace WallThrough.Audio
{
    /// <summary>
    /// Plays a specified music clip using the AudioManager.
    /// </summary>
    public class MusicPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip musicClip; // The music clip to play
        [SerializeField] private float volume = 0.35f; // Volume level for the music
        private bool play;

        private void Start()
        {
            play = true; // Set play to true to trigger music playback
        }

        private void Update()
        {
            if (play)
            {
                PlayMusicClip();
                play = false; // Prevent further playback until reset
            }
        }

        /// <summary>
        /// Plays the music clip using the AudioManager.
        /// </summary>
        private void PlayMusicClip()
        {
            if (musicClip != null)
            {
                if (AudioManager.Instance != null)
                {
                    AudioManager.Instance.PlayMusic(musicClip, volume);
                }
                else
                {
                    Debug.LogWarning("No audio manager found.");
                }
            }
            else
            {
                Debug.LogWarning("Music clip is not assigned in the MusicPlayer.");
            }
        }
    }
}
