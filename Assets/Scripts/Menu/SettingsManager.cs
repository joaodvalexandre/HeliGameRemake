﻿using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Audio;

public class SettingsManager : MonoBehaviour
{
    public Toggle fullscreenToggle;
    public Dropdown resolutionDropdown;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Button applyButton;
    public Button applyButton2;

    public AudioMixer masterMixer;
    public Resolution[] resolutions;
    public GameSettings gameSettings;
    public GameObject menu;
    
    void Start()
    {
        gameSettings = new GameSettings();

        fullscreenToggle.onValueChanged.AddListener(delegate { OnFullscreenToggle(); });
        resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionChange(); });
        musicSlider.onValueChanged.AddListener(delegate { SetMusicLvl(); });
        sfxSlider.onValueChanged.AddListener(delegate { SetSfxLvl(); });
        applyButton.onClick.AddListener(delegate { OnApplyButtonClick(); });
        applyButton2.onClick.AddListener(delegate { OnApplyButtonClick(); });

        resolutions = Screen.resolutions;

        foreach (Resolution resolution in resolutions)
        {
            resolutionDropdown.options.Add(new Dropdown.OptionData(resolution.ToString()));
        }

        LoadSettings();
    }

    public void OnFullscreenToggle()
    {
        gameSettings.isFullscreen = Screen.fullScreen = fullscreenToggle.isOn;
    }

    public void OnResolutionChange()
    {
        Screen.SetResolution(resolutions[resolutionDropdown.value].width, resolutions[resolutionDropdown.value].height, Screen.fullScreen);
        gameSettings.resolutionIndex = resolutionDropdown.value;
    }

    public void SetMusicLvl()
    {
        masterMixer.SetFloat("musVol", musicSlider.value);
        gameSettings.musicVolume = musicSlider.value;
    }

    public void SetSfxLvl()
    {
        masterMixer.SetFloat("sfxVol", sfxSlider.value);
        gameSettings.soundVolume = sfxSlider.value;
    }

    public void OnApplyButtonClick()
    {
        SaveSettings();
    }

    public void SaveSettings()
    {
        string jsonData = JsonUtility.ToJson(gameSettings, true);
        File.WriteAllText(Application.persistentDataPath + "/gamesettings.json", jsonData);
    }

    public void LoadSettings()
    {
        gameSettings = JsonUtility.FromJson<GameSettings>(File.ReadAllText(Application.persistentDataPath + "/gamesettings.json"));

        if (File.Exists(Application.persistentDataPath + "/gamesettings.json"))
        {
            fullscreenToggle.isOn = gameSettings.isFullscreen;
            resolutionDropdown.value = gameSettings.resolutionIndex;
            masterMixer.SetFloat("sfxVol", gameSettings.soundVolume);
            masterMixer.SetFloat("musVol", gameSettings.musicVolume);
            sfxSlider.value = gameSettings.soundVolume;
            musicSlider.value = gameSettings.musicVolume;
            resolutionDropdown.RefreshShownValue();
        }
    }
}