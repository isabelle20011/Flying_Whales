﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float m_JumpPower = 12.0F;
    [SerializeField] float m_MovingTurnSpeed = 360.0F;
    [SerializeField] float m_StationaryTurnSpeed = 180;
    [Range(1f, 10f)] [SerializeField] float m_GravityMultiplier = 2f;
    [SerializeField] float m_MoveSpeedMultiplier = 1f;
    [SerializeField] float m_AnimSpeedMultiplier = 1f;
    [SerializeField] bool  main;

    Camera m_Camera;
    Rigidbody   m_Rigidbody;
    CharacterController m_Controller;
    Animator    m_Animator;
    const float k_Half = 0.5f;
    float       m_TurnAmount;
    float       m_ForwardAmount;
    float       m_CapsuleHeight;
    Vector3     m_CapsuleCenter;
    bool        m_Crouching;
    Vector3     m_GroundNormal;
    private float vSpeed = 0;
    private Vector3 m_movement;
    private bool m_sprint;
    private bool m_Attack;

    // Use this for initialization
    void Start()
    {
        m_Camera = Camera.main;
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Controller = GetComponent<CharacterController>();
        m_CapsuleHeight = m_Controller.height;
        m_CapsuleCenter = m_Controller.center;
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
    }

    // Update is called once per frame
    public void Move(Vector3 move, bool crouch, bool jump, float speed, bool Attack)
    {
        m_ForwardAmount = move.z;
        m_TurnAmount = Mathf.Atan2(move.x, move.z);
        m_sprint = (speed > 10f) ? true : false;
        m_Attack = Attack;
        if (move != Vector3.zero)
        {
            ApplyExtraTurnRotation(move);
        }

        ScaleCapsuleForCrouching(crouch);
        PreventStandingInLowHeadroom();
        

        // send input and other state parameters to the animator
        UpdateAnimator(move);

        // Move character
        m_movement = m_Camera.transform.TransformDirection(move).normalized * speed * m_MoveSpeedMultiplier * Time.fixedDeltaTime;
        HandleJumpMovement(crouch, jump);
        m_Controller.Move(m_movement);
    }


    void ScaleCapsuleForCrouching(bool crouch)
    {
        if (m_Controller.isGrounded && crouch)
        {
            if (m_Crouching) return;
            m_Controller.height /= 2f;
            m_Controller.center /= 2f;
            m_Crouching = true;
        }
        else
        {
            Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Controller.radius * k_Half, Vector3.up);
            float crouchRayLength = m_CapsuleHeight - m_Controller.radius * k_Half;
            if (Physics.SphereCast(crouchRay, m_Controller.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                m_Crouching = true;
                return;
            }
            m_Controller.height = m_CapsuleHeight;
            m_Controller.center = m_CapsuleCenter;
            m_Crouching = false;
        }
    }

    void PreventStandingInLowHeadroom()
    {
        // prevent standing up in crouch-only zones
        if (!m_Crouching)
        {
            Ray crouchRay = new Ray(m_Rigidbody.position + Vector3.up * m_Controller.radius * k_Half, Vector3.up);
            float crouchRayLength = m_CapsuleHeight - m_Controller.radius * k_Half;
            if (Physics.SphereCast(crouchRay, m_Controller.radius * k_Half, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                m_Crouching = true;
            }
        }
    }

    void UpdateAnimator(Vector3 move)
    {
        // update the animator parameters
        m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);
        m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
        m_Animator.SetBool("Crouch", m_Crouching);
        m_Animator.SetBool("OnGround", m_Controller.isGrounded);
        m_Animator.SetBool("Sprinting", m_sprint);
        m_Animator.SetBool("Attack", m_Attack);
    }

    void HandleJumpMovement(bool crouch, bool jump)
    {
        vSpeed = -1;
        if (m_Controller.isGrounded)
        {
            // check whether conditions are right to allow a jump:
            if (jump && !crouch)
            {
                //moveDirection = new Vector3(0, m_JumpPower * 10f, 0);
                vSpeed = m_JumpPower;
            }
        }
        vSpeed -= 9.8f * m_GravityMultiplier * Time.fixedDeltaTime;
        m_movement.y = vSpeed;
        //moveDirection.y -= 20f * Time.deltaTime;
    }

    void ApplyExtraTurnRotation(Vector3 move)
    {
        // character turn
        if (!main)
        {
            float targetRotation = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + m_Camera.transform.eulerAngles.y;
            Quaternion lookAt = Quaternion.Slerp(transform.rotation,
                                          Quaternion.Euler(0, targetRotation, 0),
                                          0.5f);
            transform.rotation = lookAt;
        }
        else
        {
            float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
            transform.Rotate(0, m_TurnAmount * turnSpeed * Time.fixedDeltaTime, 0);
        }
    }

    public bool IsAttacking()
    {
    return m_Attack || m_sprint;
    }
}
