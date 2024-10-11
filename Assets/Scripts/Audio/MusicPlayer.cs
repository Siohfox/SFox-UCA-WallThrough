using UnityEngine;

namespace WallThrough.Audio
{
    public class MusicPlayer : MonoBehaviour
    {
        public AudioClip musicClip;
        public float volume = 0.35f;
        public bool play;

        private void Start()
        {
            play = true;
        }

        private void Update()
        {
            if(play)
            {
                if (musicClip != null)
                {
                    if (AudioManager.Instance != null)
                    {
                        AudioManager.Instance.PlayMusic(musicClip, volume);
                    }
                    else
                    {
                        Debug.LogWarning("No audio manager found");
                    }
                }
                else
                {
                    Debug.LogWarning("Music clip is not assigned in the MusicPlayer.");
                }

                play = false;
            }
        }
    }
}