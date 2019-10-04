﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float   crouchSpeed = 2f;
    [SerializeField] private float   walkingSpeed = 4f;
    [SerializeField] private float   sprintingSpeed = 8f;
    [SerializeField] private float   turnSpeed = 5.0F;
    [SerializeField] private float   jumpHeight = 12.0F;
    [SerializeField] private float   gravityScale = 1f;
    [SerializeField] private float   DashDistance = 10f;
    [SerializeField] private Vector3 Drag = new Vector3(5f, 0f, 5f);

    private CharacterController m_Controller;
    private Animator            m_Animator;
    private Rigidbody           m_Rigidbody;

    private float   m_CapsuleRadius;
    private Vector3 m_CapsuleCenter;
    private Vector3 m_Move;
    private float   m_moveSpeed;

    // Animator variables

    private float m_InputForward;
    private float m_InputTurn;
    private bool  m_Crouching;
    private bool  m_Sprint;
    private bool  m_Attack;
    public bool  m_Jump;
    private bool  m_Dash;

    public bool allowCrouch;

    // Use this for initialization
    private void Start()
    {
        m_Animator = GetComponentInChildren<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Controller = GetComponent<CharacterController>();

        m_CapsuleRadius = m_Controller.radius;
        m_CapsuleCenter = m_Controller.center;

        m_Move = new Vector3(0f, 0f, 0f);
        m_moveSpeed = walkingSpeed;
    }

    // Update is called once per frame
    private void Update()
    {
        Move();
        Jump();
        Sprint();
        if (allowCrouch)
        {
            Dash();
            Crouch();
        }
        Gravity();
        Rotate();
        Attack();

        // Apply move vector
        m_Controller.Move(this.transform.forward * m_InputForward * Time.deltaTime * m_moveSpeed);

        m_Controller.Move(m_Move * Time.deltaTime);

        UpdateAnimator();

        m_Attack = false;
    }

    private void Move()
    {
        float h = Input.GetAxisRaw("Horizontal");// setup h variable as our horizontal input axis
        float v = Input.GetAxisRaw("Vertical"); // setup v variables as our vertical input axis

        h = h * Mathf.Sqrt(1f - 0.5f * v * v);
        v = v * Mathf.Sqrt(1f - 0.5f * h * h);

        m_InputForward = Mathf.Clamp(Mathf.Lerp(m_InputForward, v, Time.deltaTime * 5f), -5f, 5);

        m_InputTurn = Mathf.Lerp(m_InputTurn, h, Time.deltaTime * 5f);

        if (m_InputForward < -0.01f)
            m_InputTurn = -m_InputTurn;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && !m_Jump && !m_Crouching)
        {
            Debug.Log("Jump");
            m_Move.y = jumpHeight;
            m_Jump = true;
        }
    }

    private void Sprint()
    {
        if (!m_Crouching && Input.GetButtonDown("Sprint") && !m_Dash)
        {
            Debug.Log("Sprint");
            m_Sprint = true;
            m_moveSpeed = sprintingSpeed;

        }
        else if (Input.GetButtonUp("Sprint") && m_Sprint)
        {
            m_Sprint = false;
            m_moveSpeed = walkingSpeed;
        }
    }

    private void Dash()
    {
        if (m_Sprint && Input.GetButtonDown("Crouch") && !m_Crouching && !m_Dash)
        {
            Debug.Log("Dash");
            m_Move += Vector3.Scale(transform.forward, DashDistance * new Vector3((Mathf.Log(1f / (Time.deltaTime * Drag.x + 1)) / -Time.deltaTime), 0,
                                        (Mathf.Log(1f / (Time.deltaTime * Drag.z + 1)) / -Time.deltaTime)));
            m_Sprint = false;
            m_Dash = true;
            m_moveSpeed = walkingSpeed;
        }
        m_Move.x /= 1 + Drag.x * Time.deltaTime;
        m_Move.y /= 1 + Drag.y * Time.deltaTime;
        m_Move.z /= 1 + Drag.z * Time.deltaTime;
    }

    private void Crouch()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            Debug.Log("Crouch");
            if (m_Crouching) return;
            m_Controller.radius /= 2f;
            //m_Controller.center /= 2f;
            m_Crouching = true;
            m_moveSpeed = crouchSpeed;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Controller.radius * 0.5f, Vector3.up);
            float crouchRayLength = m_CapsuleRadius * 0.5f;
            if (Physics.SphereCast(crouchRay, m_Controller.radius * 0.5f, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore) && m_Crouching)
            {
                Debug.Log("OOPS");
                m_Crouching = true;
                return;
            }
            else if (m_Controller.radius != m_CapsuleRadius && m_Crouching)
            {
                m_Controller.radius = m_CapsuleRadius;
                //m_Controller.center = m_CapsuleCenter;
                m_Crouching = false;
                m_moveSpeed = walkingSpeed;
                m_Dash = false;
            }
        }
    }

    private void Gravity()
    {
        m_Move.y += (Physics.gravity.y * gravityScale * Time.deltaTime);
    }

    private void Rotate()
    {
        transform.Rotate(0f, m_InputTurn * turnSpeed, 0f);
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1") && !m_Crouching && !m_Jump)
        {
            Debug.Log("Attack");
            m_Attack = true;
        }
    }

    void UpdateAnimator()
    {
        // update the animator parameters
        m_Animator.SetFloat("Forward", m_InputForward, 0.1f, Time.deltaTime);
        m_Animator.SetFloat("Turn", m_InputTurn, 0.1f, Time.deltaTime);
        m_Animator.SetBool("Crouch", m_Crouching);
        m_Animator.SetBool("Jump", m_Jump);
        m_Animator.SetBool("Sprinting", m_Sprint);
        m_Animator.SetBool("Attack", m_Attack);
        m_Animator.SetBool("Dash", m_Dash);
    }

    public bool IsAttacking()
    {
        //change it to check animation instead
        return m_Attack || m_Sprint || m_Dash;
    }


    public void toggleJump()
    {
        m_Jump = false;
    }

    public void toggleAttack()
    {
        m_Attack = false;
    }
}
