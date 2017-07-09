using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavernTrigger : MonoBehaviour {

	BoxCollider thisCollider;

	// Use this for initialization
	void Start () {
				thisCollider = GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

		void OnTriggerEnter(Collider col) {
				if (col.CompareTag("Player")) {
					col.gameObject.GetComponent<PlayerScript>().TriggerCavern();
					thisCollider.enabled = false;
				}

	}
}
