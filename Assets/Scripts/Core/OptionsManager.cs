using System;
using UnityEngine;
using UnityEngine.Audio;

namespace WallThrough.Core
{
    public class OptionsManager : MonoBehaviour
    {
        [SerializeField] private SaveManager saveManager;
        private OptionsData optionsData;
        public static event Action OnOptionsDataLoaded;

        [SerializeField] private AudioMixer audioMixer;

        private void Start()
        {
            optionsData = saveManager.LoadOptionsData();
            if (optionsData == null)
            {
                Debug.Log("No saved options data found, initializing default settings for options");
                InitializeDefaults();
            }
            if (!audioMixer)
            {
                Debug.LogWarning("No audio mixer found");
            }
            else
            {
                audioMixer.SetFloat("volume", Mathf.Log10(optionsData.volume) * 20);
            }

            // Init settings from current options data
            QualitySettings.SetQualityLevel(optionsData.quality);
            Screen.fullScreen = optionsData.fullscreen;


            OnOptionsDataLoaded?.Invoke();
        }

        private void InitializeDefaults()
        {
            optionsData = new OptionsData
            {
                volume = 0.35f,
                sfxVolume = 0.35f,
                musicVolume = 0.35f,
                quality = 5,
                fullscreen = false,
                resolutionIndex = 0
            };
        }

        private void UpdateOption<T>(ref T field, T value, string key)
        {
            field = value;
            PlayerPrefs.SetString(key, JsonUtility.ToJson(field));
            saveManager.SaveOptionsData(optionsData);
        }

        public void SetVolume(float volume)
        {
            UpdateOption(ref optionsData.volume, volume, "volume");

            // Update the AudioMixer volume (using decibels, where 1.0f corresponds to 0 dB)
            if(audioMixer)
                audioMixer.SetFloat("volume", Mathf.Log10(volume) * 20);  // Convert to decibels
        }

        public void SetQuality(int qualityIndex)
        {
            UpdateOption(ref optionsData.quality, qualityIndex, "quality");

            QualitySettings.SetQualityLevel(qualityIndex);
        }

        public void SetResolution(int resolutionIndex) 
        {
            UpdateOption(ref optionsData.resolutionIndex, resolutionIndex, "resolution");
            Resolution resolution = Screen.resolutions[resolutionIndex];

            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }

        public void SetFullScreen(bool isFullscreen)
        {
            optionsData.fullscreen = isFullscreen;
            Screen.fullScreen = isFullscreen;
            saveManager.SaveOptionsData(optionsData);
        }

        public float GetVolume() => optionsData.volume;
        public float GetSFXVolume() => optionsData.sfxVolume;
        public float GetMusicVolume() => optionsData.musicVolume;
        public int GetQuality() => optionsData.quality;
        public bool IsFullScreen() => optionsData.fullscreen;
        public int GetResolutionIndex() => optionsData.resolutionIndex;
    }
}
