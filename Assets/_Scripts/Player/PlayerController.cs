using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Private Variables

    [Header("Player Values")] [SerializeField]
    private float health = 100f;

    [SerializeField] private float damageAmount = 30f;

    [Header("Player Info")] [SerializeField]
    private TextMeshProUGUI playerHPValueText;

    private GameManager gameManagerInstance;

    private PlayerManager playerManager;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Get the Game Manager reference
        gameManagerInstance = GameManager.Instance;
        
        // Get the Player Manager reference
        playerManager = FindObjectOfType<PlayerManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage()
    {
        health -= damageAmount;
        
        updatePlayerHPValueText(health);

        if (health <= 0f)
        {
            playerManager.ShowGameOverPanel();

            die();
        }
    }

    void die()
    {
        Destroy(gameObject);
    }

    void updatePlayerHPValueText(float health)
    {
        playerHPValueText.text = health.ToString();
    }
}
