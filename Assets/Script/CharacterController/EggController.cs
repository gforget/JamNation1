using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour {

    public bool NotUsable = false;
    public float VelocityDestroy = 10.0f;
    
    Rigidbody m_RigidBody;
    TrailRenderer TrailRenderer;

	// Use this for initialization
	void Awake ()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        TrailRenderer = GetComponent<TrailRenderer>();
    }

    private void Update()
    {
      TrailRenderer.enabled = m_RigidBody.velocity.magnitude > VelocityDestroy;
    }

    public void LaunchEgg(Vector3 direction)
    {
        transform.parent = transform.root.parent;
        m_RigidBody.constraints = RigidbodyConstraints.FreezePositionZ;
        m_RigidBody.useGravity = true;
        m_RigidBody.AddForce(direction);
        NotUsable = true;

        StartCoroutine(NotUsableRoutine());
    }

    IEnumerator NotUsableRoutine()
    {
        yield return new WaitForSeconds(0.4f);
        NotUsable = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (m_RigidBody.velocity.magnitude > VelocityDestroy)
        {
           Destroy(gameObject);
        }
    }
}
