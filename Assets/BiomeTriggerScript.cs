using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeTriggerScript : MonoBehaviour {

	BoxCollider thisCollider;

	void Start() {
		thisCollider = GetComponent<BoxCollider>();
	}
 
	void OnTriggerEnter(Collider col) {
				if (col.CompareTag("Player")) {
					GameManager.instance.SwitchBiome();
					thisCollider.enabled = false;
				}

	}
}
