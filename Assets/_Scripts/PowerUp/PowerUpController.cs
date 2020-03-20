using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    #region Private Variables

    [Header("PowerUp Features")] 
    [SerializeField] private GameObject Coin;
    [SerializeField] private GameObject Wall;

    private bool isPowerUpActive;
    
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroyPowerUpIfNotActive());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Function to Destroy Power Up if not Collected within 5 seconds
    /// </summary>
    /// <returns></returns>
    IEnumerator destroyPowerUpIfNotActive()
    {
        yield return new WaitForSeconds(5f);

        if (!isPowerUpActive)
        {
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPowerUpActive = true;
            
            activateWall();
        }
    }

    /// <summary>
    /// Show the Wall and hide the Coin once PowerUp activated
    /// </summary>
    void activateWall()
    {
        Coin.SetActive(false);
        Wall.SetActive(true);

        StartCoroutine(activateWallTimer());
    }

    // Destroy PowerUp/Wall after 10 seconds of getting activated
    IEnumerator activateWallTimer()
    {
        yield return new WaitForSeconds(10f);
        
        Destroy(gameObject);
    }
}
