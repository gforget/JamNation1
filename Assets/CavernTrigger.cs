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
					if(GameManager.instance.cavernLight == true) {
						GameManager.instance.cavernLight = false;
					} else {
						GameManager.instance.cavernLight = true;
					}
					StartCoroutine(GameManager.instance.ChangeLight());
					thisCollider.enabled = false;
				}
		}

}
