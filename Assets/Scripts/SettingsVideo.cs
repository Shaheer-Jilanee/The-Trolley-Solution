using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SettingsVideo : MonoBehaviour
{
    [Header("Video Settings")]
    [SerializeField]
    private GameObject settingsResolution, settingsRatio;
    [SerializeField] 
    private TMP_Dropdown screenModeDropdown, resolutionSizeDropdown, vsyncDropdown, qualityDropdown;

    private Resolution[] resolutions;
    void Update()
    {
        // 1:1 ratio only for gameplay purposes
        // if (screenMode.value == 1) // Fullscreen (1)
        // {
        //     settingsRatio.SetActive(true);
        //     settingsResolution.SetActive(false);
        // }
        // else // Window (0) or Borderless (2)
        // {
        //     settingsRatio.SetActive(false);
        //     settingsResolution.SetActive(true);
        // }
    }
    public void ScreenMode(int value)
    {
        Screen.fullScreenMode = value switch
        {
            0 => FullScreenMode.Windowed,
            1 => FullScreenMode.ExclusiveFullScreen,
            2 => FullScreenMode.FullScreenWindow,
            _ => Screen.fullScreenMode
        };
        //Debug.Log(value + " | " + Screen.fullScreenMode);
    }
    public void ResolutionSize(int value)
    {
        switch (value)
        {
            case 0:
                
                Screen.SetResolution(600, 600, Screen.fullScreenMode);
                break;
            case 1:
                Screen.SetResolution(800, 800, Screen.fullScreenMode);
                break;
            case 2:
                Screen.SetResolution(1000, 1000, Screen.fullScreenMode);
                break;
            case 3:
                Screen.SetResolution(1200, 1200, Screen.fullScreenMode);
                break;
            case 4:
                Screen.SetResolution(1400, 1400, Screen.fullScreenMode);
                break;
        }
    }

    public void VSyncMode(int value)
    {
        QualitySettings.vSyncCount = value;
        PlayerPrefs.SetInt("VSyncMode", value);
    }

    public void QualitySetting(int value)
    {
        QualitySettings.SetQualityLevel(value);
        PlayerPrefs.SetInt("QualityLevel", value);
    }
}
