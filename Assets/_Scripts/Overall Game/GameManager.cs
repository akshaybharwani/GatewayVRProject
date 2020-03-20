using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Public Variables
    
    [NonSerialized] public int KillCount = 0;
    
    public PlayerManager PlayerManager;
    
    #endregion

    #region Private Variables

    

    #endregion
    
    private static GameManager _instance;

    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }
            return _instance;
        }
    }
    
    void Awake()
    {
        _instance = this;
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerManager = FindObjectOfType<PlayerManager>();
    }

    public void UpdateKillCount()
    {
        // Increase the Kill Count
        KillCount++;
        
        // Update the Kill Count Value Text
        PlayerManager.UpdateKillCountValueText(KillCount.ToString());
    }

    public void EndGame()
    {
        
    }
}
