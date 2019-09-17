using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    private Vector3 m_Move;
    private bool m_Jump;
    private PlayerMovement m_Character;
    private float speed = 10f;
    private bool m_Attack;

    // Use this for initialization
    void Start()
    {
        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<PlayerMovement>();
    }
    private void Update()
    {
        if (!m_Jump)
        {
            m_Jump = Input.GetButtonDown("Jump");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool crouch = Input.GetKey(KeyCode.C);

        // calculate move direction to pass to character
        m_Move = new Vector3(h, 0, v);
        m_Move = m_Move.normalized;

        if (Input.GetButton("Fire3") && !crouch && !m_Jump)
        {
            speed = 20f;
        }
        else
        {
            speed = 10f;
        }

        if (Input.GetButton("Fire1") && !crouch && !m_Jump)
        {
            m_Attack = true;
        }

            // pass all parameters to the character control script
        //m_Character.Move(m_Move, crouch, m_Jump, speed, m_Attack);
        m_Jump = false;
        m_Attack = false;
    }
}
