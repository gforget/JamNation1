using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndScript : MonoBehaviour {

	bool[] playersIn;
	bool[] playerss;
	GameObject[] playersObjects;

	public Text[] endTexts;
	int playerIndex;



	// Use this for initialization
	void Start () {
		playersIn = new bool[5];
		playersObjects = new GameObject[5];
		playerss = new bool[5];
		endTexts = new Text[4];
	}
	
	// Update is called once per frame
	void Update () {
		if (playersIn[1] == true) {
			if (playersIn[2] == true) {
				if (playersIn[3] == true) {
					if (playersIn[4] == true) {
						StartCoroutine("EndCoroutine");
					}
				}
			}
		}
	}

	void OnTriggerEnter(Collider col) {
		if (col.CompareTag("Player")) {
			playersIn[col.gameObject.transform.parent.GetComponent<RunnerController>().m_PlayerIndex] = true;
			col.gameObject.transform.parent.GetComponent<RunnerController>().enabled = false;
			playersObjects[col.gameObject.transform.parent.GetComponent<RunnerController>().m_PlayerIndex] = col.gameObject.transform.parent.gameObject;
		}
	}

	IEnumerator EndCoroutine() 
	{
		for (int i = 1; i <5; i++) {
			if (playersObjects[i].GetComponent<RunnerController>().characterState == RunnerController.CharacterState.withEgg) {
				playerss[i] = true;
				endTexts[i - 1].text = "Player " + i + " is now a warrior.";
			} else {
				endTexts[i - 1].text = "Player " + i + " deserves only shame.";
			}
		}
		yield return new WaitForEndOfFrame();
	}
}
