using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerController : MonoBehaviour {

	public Transform[] SpawnPoints;
	int randomSpawnPoint, randomMonster;
	public static bool spawnAllowed;

	private GameObject enemyGameObject;

	// Use this for initialization
	void Start ()
	{
		// Load the Enemy GameObject from the AssetBundle
		StartCoroutine(LoadAsset("enemy", "Enemy"));
		
		spawnAllowed = true;
		
		// Spawn Enemies
		InvokeRepeating ("spawnAEnemy", 2f, 1f);
	}

	void spawnAEnemy()
	{
		if (spawnAllowed) {
			randomSpawnPoint = Random.Range (0, SpawnPoints.Length);
			Instantiate (enemyGameObject, SpawnPoints [randomSpawnPoint].position,
				Quaternion.identity);
		}
	}

	/// <summary>
	/// Function to load Asset from a AssetBundle using the specified Object Name
	/// </summary>
	/// <param name="assetBundleName"></param>
	/// <param name="objectNameToLoad"></param>
	/// <returns></returns>
	IEnumerator LoadAsset(string assetBundleName, string objectNameToLoad)
	{
		string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "AssetBundles");
		filePath = System.IO.Path.Combine(filePath, assetBundleName);

		// Load  AssetBundle
		var assetBundleCreateRequest = AssetBundle.LoadFromFileAsync(filePath);
		yield return assetBundleCreateRequest;
        
		AssetBundle asseBundle = assetBundleCreateRequest.assetBundle;

		AssetBundleRequest asset = asseBundle.LoadAssetAsync<GameObject>(objectNameToLoad);
        
		yield return asset;

		// Retrieve the object 
		enemyGameObject = asset.asset as GameObject;
		
		AssetBundle.UnloadAllAssetBundles(false);
	}
}
