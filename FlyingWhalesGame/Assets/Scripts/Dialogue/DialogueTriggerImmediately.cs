using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;

public class DialogueTriggerImmediately : DialogueTrigger
{
	public bool shouldStart;
	private bool called;
	private Collider[] colliders;
	// Start is called before the first frame update

	protected override void Start()
	{
		base.Start();
		colliders = GetComponents<Collider>();
		shouldStart = !GameManager_Master.Instance.momWasCalled;
	}
	protected override void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player" && shouldStart)
		{
			PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
			simplePlayerMovement sPlayerMovement = other.GetComponent<simplePlayerMovement>();
			{
				if (!dialogueManager.inConvo && !wasCalledThisFrame)
				{
					Debug.Log("trigger");
					TriggerDialogue();
					wasCalledThisFrame = true;
					StartCoroutine(waitDialogue());
					if (playerMovement)
					{

						playerMovement.enabled = false;
					}
					else
					{
						sPlayerMovement.enabled = false;
					}
				}
				else if (Input.GetButtonUp("Interact") && dialogueManager.inConvo && !wasCalledThisFrame)
				{
					Debug.Log("displayNext");
					dialogueManager.DisplayNextSentence();
					wasCalledThisFrame = true;
					StartCoroutine(waitDialogue());

				}

				if (dialogueManager.inConvo)
				{
					Vector3 targetDir = this.transform.position - other.transform.position;
					Vector3 targetDir2 = other.transform.position - this.transform.position;

					// The step size is equal to speed times frame time.
					float step = 2 * Time.deltaTime;

					Vector3 newDir = Vector3.RotateTowards(other.transform.forward, targetDir, step, 0.0f);
					Vector3 newDir2 = Vector3.RotateTowards(this.transform.forward, targetDir2, step, 0.0f);

					newDir = new Vector3(newDir.x, 0, newDir.z);
					newDir2 = new Vector3(newDir2.x, 0, newDir2.z);

					// Move our position a step closer to the target.
					other.transform.rotation = Quaternion.LookRotation(newDir);
					this.transform.rotation = Quaternion.LookRotation(newDir2);
				}
			}
		}
		else if (!shouldStart && !called)
		{
			Debug.Log("called");
			OnTriggerEnd();
			called = true;
		}
	}

	public override void OnTriggerEnd()
	{
		base.OnTriggerEnd();
		shouldStart = false;
		this.enabled = false;
		foreach (Collider collider in colliders)
		{
			if (collider.isTrigger)
			{
				collider.enabled = false;
			}
		}
		dialogueManager.hideHelperText();
		GameManager_Master.Instance.momWasCalled = true;
	}

	IEnumerator waitDialogue()
	{
		yield return new WaitForSeconds(0.01f);
		wasCalledThisFrame = false;
	}

}
