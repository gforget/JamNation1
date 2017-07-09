using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour {

    public bool NotUsable = false;
    public float VelocityDestroy = 10.0f;
    public bool IsTaken = true;

    Rigidbody m_RigidBody;
    TrailRenderer TrailRenderer;

    // Use this for initialization
    MeshRenderer m_Renderer;

    IEnumerator Start ()
    {
        while (GameManager.instance == null) yield return null;
        Init();
    }

    private void Init()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        TrailRenderer = GetComponent<TrailRenderer>();

        m_Renderer = GetComponent<MeshRenderer>();
        GameManager.instance.AddEgg(this);
    }

    private void OnDestroy()
    {
        GameManager.instance.RemoveEgg(this);
    }

    bool m_TrailActive
    {
        get
        {
            return _m_TrailActive;
        }

        set
        {
            bool prevValue = _m_TrailActive;
            _m_TrailActive = value;

            if (_m_TrailActive == prevValue) return;

            TrailRenderer.enabled = _m_TrailActive;

            if (_m_TrailActive)
            {
                AkSoundEngine.PostEvent("SFX_Eggs_Speed", gameObject);
            }
        }
    }

    bool _m_TrailActive;
    bool m_HaveBeenVisible = false;

    private void Update()
    {
        m_TrailActive = m_RigidBody.velocity.magnitude > VelocityDestroy;

        if (m_Renderer.isVisible) m_HaveBeenVisible = true;
        if (!m_Renderer.isVisible && m_HaveBeenVisible)
        {
            Destroy(gameObject);
        }
    }

    public void LaunchEgg(Vector3 direction)
    {
        transform.parent = transform.root.parent;
        m_RigidBody.constraints = RigidbodyConstraints.FreezePositionZ;
        m_RigidBody.useGravity = true;

        m_RigidBody.AddForce(direction);
        NotUsable = true;

        StartCoroutine(NotUsableRoutine());
        IsTaken = false;
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
            AkSoundEngine.PostEvent("SFX_Eggs_Breaking", gameObject);
            Destroy(gameObject);
        }
        else
        {
            AkSoundEngine.PostEvent("SFX_Eggs_Impact", gameObject);
        }
    }
}
