using System.Collections;
using UnityEngine;

//Made by Daniel Otaigbe
public class DialogueTrigger : MonoBehaviour
{
	public Dialogue dialogue;
	protected DialogueManager dialogueManager;
	protected bool wasCalledThisFrame = false;
	public int doSomething = -1;
	[SerializeField] protected GameObject[] exclamation;

	protected virtual void deleteExclamation()
	{
		if (exclamation.Length > 0)
		{
			foreach (GameObject excla in exclamation)
			{
				excla.SetActive(false);
			}
		}
	}

	protected virtual void Start()
	{
		dialogueManager = FindObjectOfType<DialogueManager>();
	}

	public virtual void TriggerDialogue()
	{
		//finds the dialogue manager and feeds it our dialogue object with the name and sentences so we can put it on the screen!
		dialogueManager.SetTrigger(this);
		dialogueManager.StartDialogue(dialogue);
	}

	public virtual void OnTriggerEnd()
	{

	}

	public virtual void OnDoSomething()
	{

	}

	protected virtual void OnTriggerStay(Collider other)
	{
		//we can add another if statement here for a layer or tag of game objects you can talk to so we don't do component calls all the time
		if (other.tag == "Player")
		{
			PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();
			simplePlayerMovement sPlayerMovement = other.GetComponent<simplePlayerMovement>();
			if (playerMovement || sPlayerMovement)
			{
				if (Input.GetButtonDown("Interact") && !dialogueManager.inConvo && !wasCalledThisFrame)
				{
					Debug.Log("trigger");
					TriggerDialogue();
					deleteExclamation();
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
				else if (Input.GetButtonDown("Interact") && dialogueManager.inConvo && !wasCalledThisFrame)
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
	}

	IEnumerator waitDialogue()
	{
		yield return new WaitForSeconds(0.01f);
		wasCalledThisFrame = false;
	}

	private void OnTriggerEnter(Collider other)
	{
		//we can add another if statement here for a layer or tag of game objects you can talk to so we don't do component calls all the time
		if (other.tag == "Player")
		{
			dialogueManager.showHelperText();
		}
	}

	protected virtual void OnTriggerExit(Collider other)
	{
		//we can add another if statement here for a layer or tag of game objects you can talk to so we don't do component calls all the time
		if (other.tag == "Player")
		{
			dialogueManager.hideHelperText();
		}
	}

}
