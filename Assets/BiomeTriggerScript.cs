using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeTriggerScript : MonoBehaviour {

//set thisBiome to 1 through 3 depending on biome to trigger;
public int thisBiome;

	void OnTriggerEnter(Collider col) {
				if (col.CompareTag("Player")) {
					GameManager.instance.biome = thisBiome;
					GameManager.instance.SwitchBiome();
				}

	}
}
