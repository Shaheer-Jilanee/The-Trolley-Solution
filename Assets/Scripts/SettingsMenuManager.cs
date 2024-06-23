using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Video;
using Update = UnityEngine.PlayerLoop.Update;

public class SettingsMenuManager : MonoBehaviour
{
    [Header("Settings Containers")]
    [SerializeField] 
    private GameObject audioContainer, videoContainer, controlContainer, gameplayContainer, accessibilityContainer;
    [Header("Settings UI")]
    [SerializeField] private Scrollbar scrollbar;
    
    public void AudioSettingsButton()
    {
        SetAllInactive();
        audioContainer.SetActive(true);
        scrollbar.size = 1f; // Fills out scroll bar
    }
    public void VideoSettingsButton()
    {
        SetAllInactive();
        videoContainer.SetActive(true);
        scrollbar.size = 1f;
    }
    public void ControlSettingsButton()
    {
        SetAllInactive();
        controlContainer.SetActive(true);
        scrollbar.size = 0.1f;
    }
    public void GameplaySettingsButton()
    {
        SetAllInactive();
        gameplayContainer.SetActive(true);
        scrollbar.size = 1f;
    }
    public void AccessibilitySettingsButton()
    {
        SetAllInactive();
        accessibilityContainer.SetActive(true);
        scrollbar.size = 1f;
    }
    private void SetAllInactive()
    {
        audioContainer.SetActive(false);
        videoContainer.SetActive(false);
        controlContainer.SetActive(false);
        gameplayContainer.SetActive(false);
        accessibilityContainer.SetActive(false);
    }
}
