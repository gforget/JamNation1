using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npc : MonoBehaviour {

	Animator thisAnimator;

	// Use this for initialization
	void Start () {
		StartCoroutine("Jumping");
		thisAnimator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Jumping() {
		int x = 0;
		while (x != -5) {
			x = Random.Range(3,5);
			yield return new WaitForSeconds(x);
			
		}
		yield return new WaitForEndOfFrame();
	}
}
