using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	//0 = mountain, 1 = jungle, 2 = cavern, 3 = beach;
	public int biome;

	// Use this for initialization
	void Awake () 
	{
		instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SwitchBiome() {
		//DO STUFF WITH INT biome;
	}
}
