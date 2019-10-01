using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Made by Daniel Otaigbe
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    private DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void TriggerDialogue()
    {
        //finds the dialogue manager and feeds it our dialogue object with the name and sentences so we can put it on the screen!
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    private void OnTriggerStay(Collider other)
    {

        //we can add another if statement here for a layer or tag of game objects you can talk to so we don't do component calls all the time
        if (other.tag == "Player")
        {
            if (Input.GetButtonDown("Interact") && !dialogueManager.inConvo)
            {
                TriggerDialogue();
            }
            else if (Input.GetButtonDown("Interact") && dialogueManager.inConvo)
            {
                dialogueManager.DisplayNextSentence();
            }
        }
    }

}
