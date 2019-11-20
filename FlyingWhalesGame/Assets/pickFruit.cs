using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickFruit : MonoBehaviour
{
	private CountdownTimer countdown;
	[SerializeField] private ParticleSystem particles;

	private void Start()
	{
		countdown = GameObject.FindObjectOfType<CountdownTimer>();
		if (!countdown)
		{
			Debug.LogWarning("no countdown found");
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			if (particles)
			{
				//particles.transform.parent = null;
				particles.Play();
				//particles.Stop();
				//Destroy(particles.gameObject, particles.main.duration);
			}
			if (countdown)
			{
				countdown.fruitPicked++;
			}
			//Destroy(this.gameObject);
			this.gameObject.SetActive(false);
		}
	}
}
