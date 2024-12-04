using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using WallThrough.Core;
using System;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    [SerializeField] private Slider audioSlider;
    [SerializeField] private Slider audioSFXSlider;
    [SerializeField] private Slider audioMusicSlider;
    [SerializeField] private TMP_Dropdown dropdownQuality;
    [SerializeField] private Toggle fullscreenCheckbox;
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    private OptionsManager optionsManager;
    Resolution[] resolutions;

    private void OnEnable()
    {
        OptionsManager.OnOptionsDataLoaded += Initialize;
    }

    private void Initialize()
    {
        optionsManager = FindObjectOfType<OptionsManager>();
        //subscribe to onIntitialized here on optionsManager
        resolutions = Screen.resolutions;

        // Populate resolution dropdown
        List<string> options = new();
        int currentResolutionIndex = optionsManager.GetResolutionIndex();

        foreach (var res in resolutions)
        {
            string option = $"{res.width}x{res.height} {res.refreshRateRatio}Hz";
            options.Add(option);
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // Initialize sliders and dropdowns
        LoadSettings();
    }

    private void Update()
    {
        //LoadSettings();
    }

    private void LoadSettings()
    {
        audioSlider.value = optionsManager.GetVolume();
        audioSFXSlider.value = optionsManager.GetSFXVolume();
        audioMusicSlider.value = optionsManager.GetMusicVolume();
        dropdownQuality.value = optionsManager.GetQuality();
        fullscreenCheckbox.isOn = optionsManager.IsFullScreen();
    }

    public void SetVolume(float volume) => optionsManager.SetVolume(volume);
    public void SetSFXVolume(float volume) => optionsManager.SetSFXVolume(volume);
    public void SetMusicVolume(float volume) => optionsManager.SetMusicVolume(volume);
    public void SetQuality(int qualityIndex) => optionsManager.SetQuality(qualityIndex);
    public void SetFullScreen(bool isFullscreen) => optionsManager.SetFullScreen(isFullscreen);
    public void SetResolution(int resolutionIndex) => optionsManager.SetResolution(resolutionIndex);
}
