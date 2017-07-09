using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveCamera : MonoBehaviour {

	public GameObject targetCameraObject;
	public float baseSpeed;
	public float speedMultiplier;

	public float elevatorMultiplier;

	public int currentWaypoint;
	public Transform[] waypoints;

	float distance, travelTime;

	// Use this for initialization
	void Start () {
		StartCoroutine("moveToWaypoints");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public IEnumerator moveToWaypoints(){
		for (int i = 0; i < (waypoints.Length - 1); i++) {
			distance = Vector3.Distance(targetCameraObject.transform.position, waypoints[currentWaypoint + 1].position);
			travelTime = distance / (baseSpeed * speedMultiplier);
			Tween myTween = targetCameraObject.GetComponent<Transform>().DOMove(new Vector3(waypoints[currentWaypoint + 1].position.x, waypoints[currentWaypoint + 1].position.y, waypoints[currentWaypoint + 1].position.z), travelTime, false).SetEase(Ease.Linear);
			yield return myTween.WaitForCompletion();
			currentWaypoint++;
		}
	}
}
