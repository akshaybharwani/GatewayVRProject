using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PowerUpManager : MonoBehaviour
{
    #region Private Variables

    [Header("PowerUp")]
    [SerializeField] private GameObject PowerUpObject;
    
    private int xPosition;
    private int zPosition;

    private Transform player;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // Spawn PowerUps
        InvokeRepeating ("spawnAPowerUp", 5f, 45f);
        
        // Get the Player Reference
        player = FindObjectOfType<FirstPersonController>().transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnAPowerUp()
    {
        xPosition = Random.Range(-30, 30);
        zPosition = Random.Range(-30, 30);

        var prefab = Instantiate(PowerUpObject, new Vector3(xPosition, 0.06f, zPosition), Quaternion.identity);
        
        prefab.transform.LookAt(player);
        
    }
}
