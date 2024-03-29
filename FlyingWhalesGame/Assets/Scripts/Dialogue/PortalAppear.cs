﻿using System.Collections;
using UnityEngine;

public class PortalAppear : MonoBehaviour
{
	[SerializeField] private ParticleSystem particle;
	[SerializeField] private MeshRenderer[] meshes;
	private void OnEnable()
	{
		Debug.Log("called");
		if (particle)
		{
			particle.transform.parent = null;
			particle.Play();
			Destroy(particle.gameObject, particle.main.duration + 0.1f);
		}
		StartCoroutine(waitForParticle());
	}

	IEnumerator waitForParticle()
	{
		yield return new WaitForSeconds(0.3f);
		if (meshes.Length > 0)
		{
			foreach (MeshRenderer mesh in meshes)
			{
				mesh.enabled = true;
			}
		}
		else
		{
			Debug.LogWarning("no mesh");
		}

	}
}
