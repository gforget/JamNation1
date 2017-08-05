using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsXBox : MonoBehaviour {

    [SerializeField]
    private RunnerController m_RunnerController;
    
    private float m_DistanceFromMiddle = 2.0f;
    private float m_YOffset = 0.9f;

	// Update is called once per frame
	void Update ()
    {
        if (m_RunnerController == null)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = (m_RunnerController.transform.position + new Vector3(0f, m_YOffset, 0f)) + (m_RunnerController.m_TargetDirection.normalized*m_DistanceFromMiddle);
    }

}
