using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    #region Public Variables

    [Header("Character")]
    public Camera CharacterCamera;
    
    [Header("Weapon Values")]
    public float Damage = 50f;
    public float Range = 100f;
    public float FireRate = 1f;
    public bool AutomaticFiring = false;
    public int MaxAmmo = 0;

    [Header("Weapon Features")] public ParticleSystem shootFlash;

    [Header("Weapons UI")] 
    public TextMeshProUGUI ReloadingText;
    public Image WeaponReticleImage;

    #endregion

    #region Private Variables

    private float nextTimeToFire = 0f;

    private int currentAmmo;
    private float reloadTime = 2f;
    private bool isReloading;

    private AudioSource audioSource;

    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = MaxAmmo;
        
        // Get the Audio Source reference
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
            return;
        
        if (currentAmmo <= 0)
        {
            StartCoroutine(reloadWeapon());
            return;
        }
        
        if (AutomaticFiring)
        {
            if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / FireRate;
                shootWeapon();
            }
        }
        else
        {
            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / FireRate;
                shootWeapon();
            }
        }
        
        // Create a RaycastHit
        RaycastHit hit;
        
        // If the Raycast hits something
        if (Physics.Raycast(CharacterCamera.transform.position,
            CharacterCamera.transform.forward,
            out hit,
            Range))
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                // Change Weapon Reticle Color
                toggleWeaponReticleColor(true);
                return;
            }
            
            toggleWeaponReticleColor(false);
        }
        
    }

    /// <summary>
    /// Function to shoot a Weapon when Fire1 Mouse button is pressed
    /// </summary>
    private void shootWeapon()
    {
        shootFlash.Play();
        audioSource.Play();
        
        currentAmmo--;
        
        // Create a RaycastHit
        RaycastHit hit;
        
        // If the Raycast hits something
        if (Physics.Raycast(CharacterCamera.transform.position,
            CharacterCamera.transform.forward,
            out hit,
            Range))
        {
            EnemyController enemyController = hit.transform.GetComponent<EnemyController>();

            if (enemyController)
            {
                var enemyTransform = enemyController.transform;
                Vector3 direction = (transform.position - enemyTransform.position).normalized;
                enemyTransform.position -= direction * 1;
                enemyController.TakeDamage(Damage);
            }
        }
    }

    /// <summary>
    /// Function to reload a Weapon
    /// </summary>
    private IEnumerator reloadWeapon()
    {
        toggleReloading(true);

        yield return new WaitForSeconds(reloadTime);
        
        currentAmmo = MaxAmmo;

        toggleReloading(false);
    }

    void toggleReloading(bool toggleValue)
    {
        isReloading = toggleValue;
        ReloadingText.enabled = toggleValue;
    }

    void toggleWeaponReticleColor(bool toggleValue)
    {
        if (toggleValue)
            WeaponReticleImage.color = Color.red;
        else 
            WeaponReticleImage.color = Color.white;
    }
}
