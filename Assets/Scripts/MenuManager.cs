using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField] [Header("Menu Containers")]
    private GameObject mainMenuContainer, settingsMenuContainer, creditsMenuContainer;
    [SerializeField] private TextMeshProUGUI playButton;
    private SceneManager SceneManager;

    private void Awake()
    {
        SceneManager = new SceneManager();
    }

    public void PlayButton()
    {
        SceneManager.LoadSceneAsync(1);
        SceneManager.UnloadSceneAsync(0);
    }

    public void SettingsButton()
    {
        mainMenuContainer.SetActive(false);
        settingsMenuContainer.SetActive(true);
    }

    public void CreditsButton()
    {
        mainMenuContainer.SetActive(false);
        creditsMenuContainer.SetActive(true);
    }

    public void BackButton()
    {
        mainMenuContainer.SetActive(true);
        settingsMenuContainer.SetActive(false);
        creditsMenuContainer.SetActive(false);
    }
}
