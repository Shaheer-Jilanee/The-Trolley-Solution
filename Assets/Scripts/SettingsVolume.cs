using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class SettingsVolume : MonoBehaviour
{
    [SerializeField] private AudioMixer mainMixer;
    [SerializeField] private TextMeshProUGUI masterAmount, musicAmount, sfxAmount;
    [SerializeField] private Slider mainSlider, musicSlider, sfxSlider;
    
    public void DoPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("MainVolume"))
        {
            var data = PlayerPrefs.GetFloat("MainVolume");
            SetMainVolume(data);
            mainSlider.value = data;
        }

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            var data= PlayerPrefs.GetFloat("MusicVolume");
            SetMusicVolume(data);
            musicSlider.value = data;
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            var data = PlayerPrefs.GetFloat("SFXVolume");
            SetSfxVolume(data);
            sfxSlider.value = data;
        }
    }

    public void SetMainVolume(float soundLevel)
    {
        mainMixer.SetFloat("volumeMaster", Mathf.Log10(soundLevel) * 20f);
        var volumePercentage = Mathf.RoundToInt(Mathf.Clamp(soundLevel, 0f, 1f) * 100);
        masterAmount.text = volumePercentage.ToString();
        PlayerPrefs.SetFloat("MainVolume", soundLevel);
    }
    
    public void SetMusicVolume(float soundLevel)
    {
        mainMixer.SetFloat("volumeMusic", Mathf.Log10(soundLevel) * 20f);
        var volumePercentage = Mathf.RoundToInt(Mathf.Clamp(soundLevel, 0f, 1f) * 100);
        musicAmount.text = volumePercentage.ToString();
        PlayerPrefs.SetFloat("MusicVolume", soundLevel);
    }

    public void SetSfxVolume(float soundLevel)
    {
        mainMixer.SetFloat("volumeSFX", Mathf.Log10(soundLevel) * 20f);
        var volumePercentage = Mathf.RoundToInt(Mathf.Clamp(soundLevel, 0f, 1f) * 100);
        sfxAmount.text = volumePercentage.ToString();
        PlayerPrefs.SetFloat("SFXVolume", soundLevel);
    }
}
