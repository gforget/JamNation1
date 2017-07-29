using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAnchor : MonoBehaviour {

    [SerializeField]
    Transform m_TargetToFollow;
	
	// Update is called once per frame
	void Update ()
    {
        if (m_TargetToFollow == null)
        {
            Destroy(gameObject);
            return;
        } 
        transform.position = m_TargetToFollow.position;
    }
}
