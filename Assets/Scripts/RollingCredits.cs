using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingCredits : MonoBehaviour
{
    [SerializeField] private GameObject creditsMenuContainer;
    [SerializeField] private RectTransform movingUI;
    [Header("UI Settings")]
    [SerializeField] private float scrollSpeed;

    // Update is called once per frame
    void Update()
    {
        if (creditsMenuContainer.activeSelf)
        {
            movingUI.Translate(0f, scrollSpeed, 0f);
        }
    }

    public void ChangeScrollSpeedUp()
    {
        if (scrollSpeed < 0.25f)
        {
            scrollSpeed += 0.05f;
        }
    }

    public void ChangeScrollSpeedDown()
    {
        if (scrollSpeed > 0f)
        {
            scrollSpeed -= 0.05f;
        }
    }

    private void OnDisable()
    {
        movingUI.offsetMax = new Vector2(0, 0);
        movingUI.offsetMin = new Vector2(0, 0);
    }
}
