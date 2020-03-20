using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class EnemyController : MonoBehaviour
{
    #region Public Variables

    [Header("Enemy Values")] 
    public float Health = 100f;
    public float MoveSpeed;

    [Header("Enemy Info")]
    public TextMeshProUGUI EnemyHPValueText;

    #endregion

    #region Private Variables

    private Rigidbody enemyRigidbody;

    private GameManager gameManagerInstance;

    private Transform player;
    
    private Vector3 directionToPlayer;

    private float minimumDistance = 0.3f;

    private bool isWallActive;

    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        // Get the Enemy RigitBody Component reference
        enemyRigidbody = GetComponent<Rigidbody>();
        
        // Get Game Manager reference
        gameManagerInstance = GameManager.Instance;

        // Get the Player Reference
        player = FindObjectOfType<FirstPersonController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        moveEnemy();
        
        // Create a RaycastHit
        RaycastHit hit;
        
        // If the Raycast hits something
        if (Physics.Raycast(transform.position,
            transform.forward,
            out hit,
            3))
        {
            // Set isWallActive if there is a Raycast Hit
            if (hit.transform.CompareTag("Wall"))
            {
                isWallActive = true;
                return;
            }

            // Otherwise set isWallActive to false, only if in the last frame it was true
            if (isWallActive)
                isWallActive = false;

        }
    }

    /// <summary>
    /// Function which runs whenever Player shoots at the Enemy
    /// </summary>
    /// <param name="amount"></param>
    public void TakeDamage(float amount)
    {
        Health -= amount;

        updateEnemyHPValueText(Health);
        
        if (Health <= 0f)
        {
            gameManagerInstance.UpdateKillCount();
            
            Die();
        }
    }

    /// <summary>
    /// Function to run whenever Enemy's health reaches 0
    /// </summary>
    void Die()
    {
        Destroy(gameObject);
    }

    void updateEnemyHPValueText(float health)
    {
        EnemyHPValueText.text = health.ToString();
    }
    
    /// <summary>
    /// Function to move the Enemy towards the Player
    /// </summary>
    void moveEnemy()
    {
        if (player && !isWallActive) 
        {
            if (Vector3.Distance(transform.position, player.position) >= minimumDistance)
            {
                Transform transform1;
                (transform1 = transform).LookAt(player);

                transform1.position += transform1.forward * (MoveSpeed * Time.deltaTime);
            }
        }
        else
        {
            if (enemyRigidbody)
                enemyRigidbody.velocity = Vector3.zero;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<PlayerController>().TakeDamage();
        }
    }
}
