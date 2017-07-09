using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerScript : MonoBehaviour {

	Renderer playerRend;
	public SkinnedMeshRenderer meshRend;
	public SpriteRenderer eyesRend;

	public bool isInvulnerable;

	// Use this for initialization
	void Start () {
		playerRend = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator Hurt() {
		isInvulnerable = true;
		playerRend.material.DOColor(Color.red, 0.25f);
		yield return new WaitForSeconds(2.5f);
		playerRend.material.DOColor(Color.white, 0.25f);
		isInvulnerable = false;
	}

	public void TriggerCavern() {
		if (eyesRend.enabled == true) {
			//meshRend.enabled = true;
			meshRend.materials[0].color = Color.white;
			eyesRend.enabled = false;
		} else {
			//meshRend.enabled = false;
			meshRend.materials[0].color = Color.black;
			eyesRend.enabled = true;
		}
	}
}
