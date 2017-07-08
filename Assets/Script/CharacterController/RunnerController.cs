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

    public int m_PlayerIndex;
    public float gravity = 2.0f;

    public float m_SpeedWithoutEgg;
    public float m_SpeedWithEgg;

    public float m_JumpWithoutEgg;
    public float m_JumpWithEgg;

    public CharacterState characterState
    {
        get
        {
            return m_CharacterState;
        }

        set
        {
            m_CharacterState = value;
            if (m_CharacterState == CharacterState.withEgg)
            {
                m_SpeedX = m_SpeedWithEgg;
                m_JumpInitialVelocity = m_JumpWithEgg;
            }

            if (m_CharacterState == CharacterState.withoutEgg)
            {
                m_SpeedX = m_SpeedWithoutEgg;
                m_JumpInitialVelocity = m_JumpWithoutEgg;
            }
        }
    }

    private CharacterState m_CharacterState;

    Animator m_Animator;
    CharacterController m_Controller;

    private Vector3 m_Direction;
    private x360_Gamepad m_Gamepad;

    private float m_XVelocity;
    private float m_YVelocity;
    private float m_CapsuleHeight;
    private Vector3 m_CapsuleCenter;
    private int m_LayerMask = 1 << 8;

    private float m_SpeedX;
    private float m_JumpInitialVelocity;

    // Use this for initialization
    void Awake ()
    {
        m_Gamepad = GamepadManager.Instance.GetGamepad(m_PlayerIndex);
        m_Controller = GetComponent<CharacterController>();
        characterState = CharacterState.withEgg;
    }

    bool m_HavePressA;
    private void Update()
    {
        if (m_Gamepad.GetButtonDown("A") && m_Controller.isGrounded)
        {
            m_YVelocity = m_JumpInitialVelocity;
            m_HavePressA = true;
        }

        if (m_Gamepad.GetButtonDown("X"))
        {
            characterState = characterState == CharacterState.withEgg ? CharacterState.withoutEgg : CharacterState.withEgg;

        }

        if (m_Controller.isGrounded && m_YVelocity < 0)
        {
            m_YVelocity = 0;
        }

        Debug.Log(m_CharacterState);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!m_Gamepad.IsConnected) return;

        m_XVelocity = m_Gamepad.GetStick_L().X * m_SpeedX;
        m_YVelocity -= gravity;

        m_Direction = (transform.right * m_XVelocity) + (transform.up*m_YVelocity);
        m_Controller.Move(m_Direction*Time.deltaTime);
    }

}
