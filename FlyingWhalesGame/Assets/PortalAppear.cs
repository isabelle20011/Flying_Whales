using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalAppear : MonoBehaviour
{
	[SerializeField]private ParticleSystem particle;
	[SerializeField]private MeshRenderer mesh;
	private void OnEnable()
	{
		Debug.Log("called");
		if (particle)
		{
			particle.transform.parent = null;
			particle.Play();
			Destroy(particle.gameObject, particle.main.duration+ 0.1f);
		}
		StartCoroutine(waitForParticle());
	}

	IEnumerator waitForParticle()
	{
		yield return new WaitForSeconds(0.3f);
		if (mesh)
		{
			mesh.enabled = true;
		}
		else
		{
			Debug.LogWarning("no mesh");
		}
		
	}
}
