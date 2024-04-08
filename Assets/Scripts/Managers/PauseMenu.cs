using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button pauseButton;

    public void PauseGame()
    {
        pauseButton.onClick.AddListener(CanvasManager.Instance.PauseGame);
    }
}