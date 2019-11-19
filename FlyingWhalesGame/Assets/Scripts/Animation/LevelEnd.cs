using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
	[SerializeField] private Transform swimHere;
	[SerializeField] private GameObject canvasEnd;
	private GameObject heartCanvas;
    private AudioSource BGM;
    private AudioSource EndSound;

	private void Start()
	{
		heartCanvas = GameObject.FindGameObjectWithTag("HeartCanvas");
		if (!heartCanvas)
		{
			Debug.LogWarning("No heartCanvas");
		}
        BGM = GameObject.FindGameObjectWithTag("BackgroundMusic").GetComponent<AudioSource>();
        EndSound = GetComponent<AudioSource>();
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			Debug.Log("Entered");
            BGM.Stop();
            EndSound.Play();
			PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
			NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
			Animator animator = other.GetComponentInChildren<Animator>();
			TransformFollower camera = Camera.main.GetComponent<TransformFollower>();
			if (playerMovement && agent && animator && camera)
			{
				heartCanvas.SetActive(false);
				canvasEnd.SetActive(true);
				playerMovement.enabled = false;
				agent.enabled = true;
				agent.SetDestination(swimHere.position);
				animator.SetFloat("Forward", 1.0f);
				camera.stopFollowing = true;
				StartCoroutine(waitToChangeScene());
			}
		}
	}

	IEnumerator waitToChangeScene()
	{
		yield return new WaitForSeconds(10f);
		SceneManager.LoadScene("EndCredits");
	}
}
