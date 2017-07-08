using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class RunnerController : MonoBehaviour {

    public int playerIndex;
    public float groundDistance = 0.2f;
    public float JumpInitialVelocity = 10.0f;
    public float gravity = 2.0f;

    public float translationSpeed;
    public float Speed;

    Rigidbody m_Rigidbody;
    Animator m_Animator;
    CapsuleCollider m_Capsule;
    CharacterController controller;

    private GamePadState state;
    private GamePadState prevState;
    private Vector3 direction;
    private x360_Gamepad gamepad;

    private bool isGrounded
    {
        get
        {
            return _isGrounded;
        }

        set
        {
            _isGrounded = value;
            if (_isGrounded) yVelocity = 0;
        }
    }

    private bool _isGrounded;

    private float xVelocity;
    private float yVelocity;
    private float m_CapsuleHeight;
    private Vector3 m_CapsuleCenter;
    private int layermask = 1 << 8;

    // Use this for initialization
    void Awake ()
    {
        //m_Rigidbody = GetComponent<Rigidbody>();
        m_Capsule = GetComponent<CapsuleCollider>();

        m_CapsuleHeight = m_Capsule.height;
        m_CapsuleCenter = m_Capsule.center;

        gamepad = GamepadManager.Instance.GetGamepad(playerIndex);

        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (gamepad.GetButtonDown("A") && controller.isGrounded)
        {
            yVelocity = JumpInitialVelocity;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!gamepad.IsConnected) return;

        Debug.Log(controller.isGrounded);

        xVelocity = gamepad.GetStick_L().X * Speed;
        yVelocity -= gravity;

        direction = (transform.right * xVelocity) + (transform.up*yVelocity);
        controller.Move(direction*Time.deltaTime);

        //m_Rigidbody.velocity = direction * Time.deltaTime;

        //if (Physics.Raycast(RaycastStartPosition, transform.up * -1f, out hit, groundDistance) && yVelocity <= 0)
        //{
        //    isGrounded = true;
        //    yVelocity = 0;
        //}
        //else
        //{
        //    isGrounded = false;
        //}

        //Debug.Log(yVelocity);
        //direction = (transform.right * xVelocity) + (transform.up*yVelocity);
        //m_Rigidbody.velocity = direction * Time.deltaTime;
    }

}
