using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsAccessibility : MonoBehaviour
{
    [SerializeField] private MainMenuManager mainMenuManager;
    
    public void TrainSpeed()
    {
        // TODO: To be implemented.
    }

    public void FlashingTitleSpeed(int value)
    {
        switch (value)
        {
            case 0: // Off
                PlayerPrefs.SetFloat("FlashingTitleSpeed", 0f);
                mainMenuManager.fps = 0f;
                break;
            case 1: // On (Slow)
                PlayerPrefs.SetFloat("FlashingTitleSpeed", 0.5f);
                mainMenuManager.fps = 0.5f;
                break;
            case 2: // On (Normal)
                PlayerPrefs.SetFloat("FlashingTitleSpeed", 1f);
                mainMenuManager.fps = 1f;
                break;
            case 3: // On (Fast)
                PlayerPrefs.SetFloat("FlashingTitleSpeed", 2f);
                mainMenuManager.fps = 2f;
                break;
            case 4: // On (Very Fast)
                PlayerPrefs.SetFloat("FlashingTitleSpeed", 5f);
                mainMenuManager.fps = 5f;
                break;
        }
    }
}
