using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerImmediately : DialogueTrigger
{
	private bool shouldStart = true;
	private Collider[] colliders;
	// Start is called before the first frame update

	protected override void Start()
	{
		base.Start();
		colliders = GetComponents<Collider>();
	}
	protected override void OnTriggerStay(Collider other)
	{
		base.OnTriggerStay(other);
		if (other.tag == "Player")
		{
			PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
			simplePlayerMovement sPlayerMovement = other.GetComponent<simplePlayerMovement>();
			{
				if (shouldStart && !dialogueManager.inConvo && !wasCalledThisFrame)
				{
					shouldStart = false;
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
			}
		}
	}

	protected override void OnTriggerExit(Collider other)
	{
		base.OnTriggerExit(other);
		if (other.tag == "Player")
		{
			this.enabled = false;
			foreach (Collider collider in colliders)
			{
				if (collider.isTrigger)
				{
					collider.enabled = false;
				}
			}
		}
	}

	IEnumerator waitDialogue()
	{
		yield return new WaitForSeconds(0.1f);
		wasCalledThisFrame = false;
	}

}
