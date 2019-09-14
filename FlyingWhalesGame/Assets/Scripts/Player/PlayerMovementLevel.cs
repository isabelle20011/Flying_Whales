using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementLevel : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float rotateSpeed = 200.0F;
    //private float rootTurnSpeed = 0.5f;

    private const float m_gravity = 20.0F;
    private Vector3 m_moveDirection = Vector3.zero;
    //private Animator m_Animator;
    private CharacterController m_controller;
    private Animation m_Animation;
    private Mesh m_mesh;

    // Use this for initialization
    void Start()
    {
        m_controller = GetComponent<CharacterController>();
        //m_Animator = GetComponent<Animator>();
        m_Animation = GetComponent<Animation>();
        m_mesh = GetComponent<Mesh>();
    }

    // Update is called once per frame
    void Update()
    {
        float fHorizontal = Input.GetAxis("Horizontal");
        float fVertical = Input.GetAxis("Vertical");

        if (m_controller.isGrounded)
        {
            m_moveDirection = new Vector3(fHorizontal, 0, fVertical);
            m_moveDirection = transform.TransformDirection(m_moveDirection);
            m_moveDirection *= speed;
            if (Input.GetButton("Jump"))
                m_moveDirection.y = jumpSpeed;

            if (Input.GetButton("Fire1"))
            {
                m_Animation.Play("sj001_skill2");
            }

        }
        m_moveDirection.y -= m_gravity * Time.deltaTime;
        m_controller.Move(m_moveDirection * Time.deltaTime);


        // animation code that will be useless
        bool bHasMoveInput = !Mathf.Approximately(fHorizontal, 0f) || !Mathf.Approximately(fVertical, 0f);
        if (bHasMoveInput && !m_Animation.IsPlaying("sj001_skill2"))
        {
            m_Animation.Play("sj001_run");
        }
        else if (!m_Animation.IsPlaying("sj001_skill2"))
        {
            m_Animation.Play("sj001_wait");
        }

        //transform.Rotate(0, fHorizontal * rotateSpeed * Time.deltaTime, 0);

        /*if (Input.GetKey(KeyCode.A))
        {
            m_mesh.vertices = 
            transform.rotation = Quaternion.LerpUnclamped(transform.rotation, Quaternion.Euler(0, -90, 0), rootTurnSpeed);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            transform.rotation = Quaternion.LerpUnclamped(transform.rotation, Quaternion.Euler(0, 0, 0), rootTurnSpeed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.LerpUnclamped(transform.rotation, Quaternion.Euler(0, 90, 0), rootTurnSpeed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
           transform.rotation = Quaternion.LerpUnclamped(transform.rotation, Quaternion.Euler(0, 180, 0), rootTurnSpeed);
        }*/
    }
    public bool IsPlayingAttack()
    {
        return m_Animation.IsPlaying("sj001_skill2");
    }
}
