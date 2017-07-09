using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerScript : MonoBehaviour {

	Renderer playerRend;

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
}
