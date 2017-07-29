using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryTrigger : MonoBehaviour {

    BoxCollider thisCollider;

    void Start()
    {
        thisCollider = GetComponent<BoxCollider>();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("TriggerActivator"))
        {
            GameManager.instance.VictoryScreen();
        }
    }
}
