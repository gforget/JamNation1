using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAdjustor : MonoBehaviour {

    Rigidbody rb;
    EggController eggScript;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        eggScript = GetComponent<EggController>();
	}

    // Update is called once per frame
    void Update() {
        if (eggScript.IsTaken == false)
        {
                if (rb.velocity.y < -2.5f)
            {
                rb.drag = 1.3f;
            } else
            {
                rb.drag = 0.05f;
            }
        }
    }
}
