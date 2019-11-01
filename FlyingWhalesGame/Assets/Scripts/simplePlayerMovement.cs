using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CharacterController))]
public class simplePlayerMovement : MonoBehaviour
{
	[SerializeField] private float walkingSpeed = 4f;
	[SerializeField] private float turnSpeed = 5.0F;
	[SerializeField] private float gravityScale = 1f;

	private CharacterController m_Controller;
	private Animator m_Animator;

	private Vector3 m_Move;
	private float m_moveSpeed;

	// Animator variables

	private float m_InputForward;
	private float m_InputTurn;

	// Use this for initialization
	private void Start()
	{
		m_Animator = GetComponentInChildren<Animator>();
		m_Controller = GetComponent<CharacterController>();

		m_Move = new Vector3(0f, 0f, 0f);
		m_moveSpeed = walkingSpeed;
	}

	// Update is called once per frame
	private void Update()
	{
		Move();
		Gravity();
		Rotate();

		// Apply move vector
		m_Controller.Move(this.transform.forward * m_InputForward * Time.deltaTime * m_moveSpeed);

		m_Controller.Move(m_Move * Time.deltaTime);

		UpdateAnimator();
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


	private void Gravity()
	{
		m_Move.y += (Physics.gravity.y * gravityScale * Time.deltaTime);
	}

	private void Rotate()
	{
		transform.Rotate(0f, m_InputTurn * turnSpeed, 0f);
	}

	void UpdateAnimator()
	{
		// update the animator parameters
		m_Animator.SetFloat("Forward", m_InputForward, 0.1f, Time.deltaTime);
		m_Animator.SetFloat("Turn", m_InputTurn, 0.1f, Time.deltaTime);
	}
}

