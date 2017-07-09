using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {

	public PlayerScript playerScript;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//Collision with player. Trigger loss of egg and player invulnerability.
	void OnTriggerEnter(Collider col)
	{
		if (col.CompareTag("Player")) {

			playerScript = col.gameObject.GetComponent<PlayerScript>();
			if (!playerScript.isInvulnerable) {
				StartCoroutine(playerScript.Hurt());
			}
		}
	}
}
