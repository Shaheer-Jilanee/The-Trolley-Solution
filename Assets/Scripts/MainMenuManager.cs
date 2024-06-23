using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Sprite[] framesTitle, framesBackground;
    [SerializeField] private Image titleImage, backgroundImage;
    [SerializeField] private SettingsVolume script_settingsVolume;
    public float fps { get; set; }

    private void Awake()
    {
        fps = PlayerPrefs.HasKey("FlashingTitleSpeed") ? PlayerPrefs.GetFloat("FlashingTitleSpeed") : 1f;
    }

    private void Start()
    {
        script_settingsVolume.DoPlayerPrefs(); // Saved volume settings load when game starts
    }

    void Update() {
        AnimateTitle();
        AnimateBackground();
    }

    private void AnimateTitle()
    {
        var index = (int)(Time.time * fps);
        index %= framesTitle.Length;
        titleImage.sprite = framesTitle[index];
    }

    private void AnimateBackground()
    {
        var index = (int)(Time.time * fps * 5);
        index %= framesBackground.Length;
        backgroundImage.sprite = framesBackground[index];
    }
}
