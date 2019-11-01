using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Made by Daniel Otaigbe
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private DialogueManager dialogueManager;
	private bool wasCalledThisFrame = false;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void TriggerDialogue()
    {
        //finds the dialogue manager and feeds it our dialogue object with the name and sentences so we can put it on the screen!
        dialogueManager.StartDialogue(dialogue);
    }

    private void OnTriggerStay(Collider other)
    {
        //we can add another if statement here for a layer or tag of game objects you can talk to so we don't do component calls all the time
        if (other.tag == "Player")
        {
			if (Input.GetButtonUp("Interact") && !dialogueManager.inConvo && !wasCalledThisFrame)
			{
				Debug.Log("trigger");
				TriggerDialogue();
				wasCalledThisFrame = true;
				StartCoroutine(waitDialogue());
			}
			else if (Input.GetButtonUp("Interact") && dialogueManager.inConvo && !wasCalledThisFrame)
            {
				Debug.Log("displayNext");
				dialogueManager.DisplayNextSentence();
				wasCalledThisFrame = true;
				StartCoroutine(waitDialogue());

			}
        }
    }

	IEnumerator waitDialogue()
	{
		yield return new WaitForSeconds(0.1f);
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

	private void OnTriggerExit(Collider other)
	{
		//we can add another if statement here for a layer or tag of game objects you can talk to so we don't do component calls all the time
		if (other.tag == "Player")
		{
			dialogueManager.hideHelperText();
		}
	}

}
