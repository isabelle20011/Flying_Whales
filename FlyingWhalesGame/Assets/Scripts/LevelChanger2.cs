﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public class LevelChanger2 : MonoBehaviour
{
	private Animator animator;
	[SerializeField] private bool NeedForSeconds;

	private void OnEnable()
	{
		animator = GetComponent<Animator>();
	}

	public void FadeToLevel()
	{
		animator.SetTrigger("FadeOut");
	}

	private void OnFadeComplete()
	{
		if (NeedForSeconds)
		{
			animator.SetTrigger("Word");
			StartCoroutine(waitSeconds());
		}
		else
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}

	IEnumerator waitSeconds()
	{
		yield return new WaitForSeconds(2f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
