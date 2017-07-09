using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	//0 = mountain, 1 = jungle, 2 = cavern, 3 = beach;
	public int biome;

    public GameObject EggPrefab;
    public List<EggController> eggControllers;

    public void AddEgg(EggController egg)
    {
        eggControllers.Add(egg);
    }
    public void RemoveEgg(EggController egg)
    {
        float zPosition = egg.transform.position.z;
        eggControllers.Remove(egg);

        if (eggControllers.Count == 0)
        {
            GameObject eggGo = Instantiate(EggPrefab);
            Vector3 spawnPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + 25.0f, zPosition);
            eggGo.transform.position = spawnPosition;
        }
    }

	string[] ambianceNames;

	public bool cavernLight;
	public Light dirLight;

	// Use this for initialization
	void Awake () 
	{
		instance = this;
	}

	void Start() {
		ambianceNames = new string[4];
		ambianceNames[0] = "Ambiance_start";
		ambianceNames[1] = "Mountain_Jungle";
		ambianceNames[2] = "Jungle_Cave";
		ambianceNames[3] = "Cave_Beach";
		SwitchBiome();
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void SwitchBiome() {
		AkSoundEngine.PostEvent(ambianceNames[biome], gameObject);
		biome++;
	}

	public IEnumerator ChangeLight () {
		yield return new WaitForEndOfFrame();
		if (cavernLight == true) {
			dirLight.DOIntensity(0, 0.50f);
		} else {
			dirLight.DOIntensity(1, 0.50f);
		}
	}
}
