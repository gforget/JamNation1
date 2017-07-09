using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class RunnerController : MonoBehaviour
{
    public enum CharacterState
    {
        withEgg = 0,
        withoutEgg = 1,
        Death = 2
    }

    public Transform m_EggParent;
    public EggController m_EggController;

    public float m_BaseThrowEgg = 1000.0f;

    public int m_PlayerIndex;
    public float m_Gravity = 2.0f;

    public float m_SpeedWithoutEgg;
    public float m_SpeedWithEgg;

    public float m_JumpWithoutEgg;
    public float m_JumpWithEgg;

    public float m_RadiusEggPicker;
    public float m_YOffSetRadiusEggPicker;

    public float m_TimeRespawn = 2.0f;

    public SkinnedMeshRenderer m_Renderer;

    public CharacterState characterState
    {
        get
        {
            return m_CharacterState;
        }

        set
        {
            CharacterState _prevState = m_CharacterState;
            m_CharacterState = value;

            if (m_CharacterState == CharacterState.withEgg)
            {
                m_SpeedX = m_SpeedWithEgg;
                m_JumpInitialVelocity = m_JumpWithEgg;
            }

            if (m_CharacterState == CharacterState.withoutEgg)
            {
                if (_prevState == CharacterState.withEgg)
                {
                    m_EggController.LaunchEgg((transform.forward * Random.Range(0.0f, 1.0f) + transform.up*Random.Range(0.0f, 1.0f)) * m_BaseThrowEgg);
                }

                m_SpeedX = m_SpeedWithoutEgg;
                m_JumpInitialVelocity = m_JumpWithoutEgg;
            }

            if (m_CharacterState == CharacterState.Death)
            {
                StartCoroutine(RespawnCoroutine());
            }
        }
    }

    private CharacterState m_CharacterState;

    CharacterController m_Controller;
    private Animator m_Animator;

    private Vector3 m_Direction;
    private x360_Gamepad m_Gamepad;

    private float m_XVelocity;
    private float m_YVelocity;
    private float m_CapsuleHeight;
    private Vector3 m_CapsuleCenter;

    private float m_SpeedX;
    private float m_JumpInitialVelocity;

    private float m_BeginZ;
    private Vector3 m_EggBeginPosition;
    private Quaternion m_BeginRotation;

    // Use this for initialization
    void Awake ()
    {
        m_Gamepad = GamepadManager.Instance.GetGamepad(m_PlayerIndex);
        m_Controller = GetComponent<CharacterController>();
        m_Animator = GetComponent<Animator>();

        characterState = CharacterState.withEgg;
        m_BeginZ = transform.position.z;

        m_EggBeginPosition = m_EggController.transform.localPosition;
        m_BeginRotation = m_EggController.transform.localRotation;

        StartCoroutine(EggHitDetection());
        StartCoroutine(DetectConnected());
    }
    
    IEnumerator DetectConnected()
    {
        yield return new WaitForEndOfFrame();
        if (m_Gamepad.IsConnected == false)
        {
            Destroy(gameObject);
        }
    }

    bool m_HavePressA;
    bool m_HaveBeenVisible = false;
    bool m_JumpAirDelayed
    {
        get
        {
            return _m_JumpAirDelayed;
        }

        set
        {
            bool prevValue = _m_JumpAirDelayed;
            _m_JumpAirDelayed = value;

            if (prevValue != _m_JumpAirDelayed && _m_JumpAirDelayed)
            {
                StartCoroutine(JumpAirDelayedRoutine());
            }
        }
    }

    bool _m_JumpAirDelayed = false;
    bool m_HaveBeenGrounded = false;

    private void Update()
    {
        
        if (characterState == CharacterState.Death) return;

        if (m_Gamepad.GetButtonDown("A") && (m_Controller.isGrounded || m_JumpAirDelayed))
        {
            m_YVelocity = m_JumpInitialVelocity;
            m_HavePressA = true;
            m_HaveBeenGrounded = false;
        }

        if (m_Gamepad.GetButtonDown("X"))
        {
            if (characterState == CharacterState.withEgg)
            {
                characterState = CharacterState.withoutEgg;
                m_EggController.LaunchEgg((transform.forward * m_Gamepad.GetStick_L().X * lookAtDirection + transform.up * m_Gamepad.GetStick_L().Y) * m_BaseThrowEgg);
            }
        }

        if (m_Controller.isGrounded && m_YVelocity < 0)
        {
            m_YVelocity = 0;
            m_HaveBeenGrounded = true;
        }

        if (!m_Controller.isGrounded && m_HaveBeenGrounded)
        {
            m_JumpAirDelayed = true;
            m_HaveBeenGrounded = false;
        }

        //quick fix to resolve the first frame where the camera dont render the players
        if (m_Renderer.isVisible) m_HaveBeenVisible = true;
        if (!m_Renderer.isVisible && m_HaveBeenVisible)
        {
            characterState = CharacterState.Death;
        } 
    }

    IEnumerator JumpAirDelayedRoutine()
    {
        yield return new WaitForSeconds(0.1f);
        m_JumpAirDelayed = false;
    }

    float lookAtDirection = 1;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!m_Gamepad.IsConnected || characterState == CharacterState.Death) return;

        m_XVelocity = m_Gamepad.GetStick_L().X * m_SpeedX;
        m_YVelocity -= m_Gravity;

        if (m_XVelocity != 0)
        {
            if (m_XVelocity > 0)
                lookAtDirection = 1;
            else
                lookAtDirection = -1;
        }

        transform.rotation = Quaternion.LookRotation(Camera.main.transform.right * lookAtDirection);
        m_Direction = (transform.forward * Mathf.Abs(m_XVelocity)) + (transform.up*m_YVelocity);

        m_Controller.Move(m_Direction*Time.deltaTime);

        m_Animator.SetBool("grounded", m_Controller.isGrounded);
        m_Animator.SetBool("haveEgg", characterState == CharacterState.withEgg);
        m_Animator.SetFloat("XVelocity", Mathf.Abs(m_XVelocity));
        m_Animator.SetFloat("YVelocity", m_YVelocity);
    }

    IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(m_TimeRespawn);

        Vector3 spawnPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y+25.0f, m_BeginZ);
        transform.position = spawnPosition;
        characterState = CharacterState.withoutEgg;
    }

    private int m_LayerMask = 1 << 10;

    IEnumerator EggHitDetection()
    {
        while (true)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, m_RadiusEggPicker, m_LayerMask);

            if (hitColliders.Length > 0)
            {
                if (!hitColliders[0].gameObject.GetComponent<EggController>().NotUsable
                && characterState == CharacterState.withoutEgg)
                {
                    m_EggController = hitColliders[0].gameObject.GetComponent<EggController>();
                    m_EggController.transform.SetParent(m_EggParent);
                    m_EggController.transform.localPosition = m_EggBeginPosition;
                    m_EggController.transform.localRotation = m_BeginRotation;

                    characterState = CharacterState.withEgg;
                    m_EggController.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    m_EggController.GetComponent<Rigidbody>().useGravity = false;
                }
            }

           yield return null;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + new Vector3(0f, m_YOffSetRadiusEggPicker, 0f), m_RadiusEggPicker);
    }
}
