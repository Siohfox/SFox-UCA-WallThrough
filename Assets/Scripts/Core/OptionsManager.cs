using System;
using UnityEngine;

namespace WallThrough.Core
{
    public class OptionsManager : MonoBehaviour
    {


        [SerializeField] private SaveManager saveManager;
        private OptionsData optionsData;
        public static event Action OnOptionsDataLoaded;

        private void Start()
        {
            optionsData = saveManager.LoadOptionsData();
            if (optionsData == null)
            {
                Debug.Log("Initializing defaults");
                InitializeDefaults();
            }
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

        public void SetVolume(float volume) => UpdateOption(ref optionsData.volume, volume, "volume");
        public void SetSFXVolume(float volume) => UpdateOption(ref optionsData.sfxVolume, volume, "sfxVolume");
        public void SetMusicVolume(float volume) => UpdateOption(ref optionsData.musicVolume, volume, "musicVolume");
        public void SetQuality(int qualityIndex) => UpdateOption(ref optionsData.quality, qualityIndex, "quality");
        public void SetFullScreen(bool isFullscreen)
        {
            optionsData.fullscreen = isFullscreen;  // Directly set the value
            PlayerPrefs.SetInt("fullscreen", isFullscreen ? 1 : 0); //TODO : Remove as everything is in JSON
            saveManager.SaveOptionsData(optionsData);
        }

        public void SetResolution(int resolutionIndex) => UpdateOption(ref optionsData.resolutionIndex, resolutionIndex, "resolution");

        private void UpdateOption<T>(ref T field, T value, string key)
        {
            field = value;
            PlayerPrefs.SetString(key, JsonUtility.ToJson(field));
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
