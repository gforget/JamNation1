using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	//0 = mountain, 1 = jungle, 2 = cavern, 3 = beach;
	public int biome;

	string[] ambianceNames;

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
}
