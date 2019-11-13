using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalAppear : MonoBehaviour
{
	public ParticleSystem particle;
	private void OnEnable()
	{
		Debug.Log("called");
		if (particle)
		{
			particle.transform.parent = null;
			particle.Play();
			Destroy(particle.gameObject, particle.main.duration);
		}
		StartCoroutine(waitForParticle());
	}

	IEnumerator waitForParticle()
	{
		yield return new WaitForSeconds(0.2f);
		GetComponent<MeshRenderer>().enabled = true;
		
	}
}
