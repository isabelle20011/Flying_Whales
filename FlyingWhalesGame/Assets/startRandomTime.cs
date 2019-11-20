using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startRandomTime : MonoBehaviour
{
	private Animator animator;

	private void Start()
	{
		animator = GetComponent<Animator>();
		if (animator)
		{
			animator.Play("Idle", 0, Random.Range(0.0f, 1.0f));
		}
	}
}
