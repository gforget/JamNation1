using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggController : MonoBehaviour {

    public bool NotUsable = false;
    public float VelocityDestroy = 10.0f;
    public bool IsTaken = true;
    public Light m_EggPointLight;

    Rigidbody m_RigidBody;
    TrailRenderer TrailRenderer;

    // Use this for initialization
    MeshRenderer m_Renderer;

    private void Awake()
    {
        m_RigidBody = GetComponent<Rigidbody>();
        TrailRenderer = GetComponent<TrailRenderer>();
        m_Renderer = GetComponent<MeshRenderer>();
        m_EggPointLight.gameObject.SetActive(GameManager.instance.NightMode);
    }

    private void OnDisable()
    {
       if (GameManager.instance != null) GameManager.instance.RemoveEgg(this);
    }

    bool m_HaveBeenVisible = false;

    private void Update()
    {
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
       AkSoundEngine.PostEvent("SFX_Eggs_Impact", gameObject);
    }
}
