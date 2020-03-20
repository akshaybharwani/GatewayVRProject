using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class WeaponsManager : MonoBehaviour
{
    #region Private Variables

    [Header("Weapons")] 
    [SerializeField] private Transform Pistol;
    [SerializeField] private Transform Rifle;

    private string assetBundleName = "weapons";

    private AssetBundle weaponsAssetBundle;
        
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        weaponsAssetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/AssetBundles/" + assetBundleName);
        
        // Initialize Weapons from the AssetBundles
        loadAsset("Pistol", Pistol);
        loadAsset("Rifle", Rifle);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            toggleWeapons(true);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            toggleWeapons(false);
    }

    void loadAsset(string objectNameToLoad, Transform parent)
    {
        var weapon = weaponsAssetBundle.LoadAsset<GameObject>(objectNameToLoad);

        Instantiate(weapon, parent);
    }

    private void toggleWeapons(bool toggleValue)
    {
        if (toggleValue)
        {
            Pistol.gameObject.SetActive(true);
            Rifle.gameObject.SetActive(false);
        }
        else
        {
            Pistol.gameObject.SetActive(false);
            Rifle.gameObject.SetActive(true);
        }
    }
}
