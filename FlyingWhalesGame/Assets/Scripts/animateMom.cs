using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animateMom : MonoBehaviour
{
	private Animator animator;
	private bool b_started;

	private void Start()
	{
		animator = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
    {
		if (!b_started && !animator.GetCurrentAnimatorStateInfo(0).IsName("Turn Head"))
		{
			StartCoroutine(waitSeconds());
		}
	}


	IEnumerator waitSeconds()
	{
		b_started = true;
		yield return new WaitForSeconds(4f);
		animator.SetTrigger("Turn Head");
		b_started = false;
	}
}
