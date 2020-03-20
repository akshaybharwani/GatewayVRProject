using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    #region Private Variables

    [Header("Game Camera")] 
    [SerializeField] private Camera GameCamera;
    
    [Header("In Game Panel")] 
    [SerializeField] private CanvasGroup InGamePanelCanvasGroup;
    
    [Header("Game Over Panel")]
    [SerializeField] private CanvasGroup GameOverPanelCanvasGroup;
    
    [SerializeField] private TextMeshProUGUI HUDKillCountValueText;
    [SerializeField] private TextMeshProUGUI GameOverPanelKillCountValueText;

    #endregion

    private void Update()
    {
        if (Input.GetKey ("escape")) {
            Application.Quit();
        }
    }

    public void UpdateKillCountValueText(string killCount)
    {
        HUDKillCountValueText.text = killCount;
        GameOverPanelKillCountValueText.text = killCount;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ShowGameOverPanel()
    {
        // Hide the In Game Panel
        InGamePanelCanvasGroup.alpha = 0;
        
        // Show Game Over Panel
        GameOverPanelCanvasGroup.alpha = 1;
        
        // Show Cursor
        Cursor.visible = true;
        
        // Enable Game Camera
        GameCamera.enabled = true;
    }
}
