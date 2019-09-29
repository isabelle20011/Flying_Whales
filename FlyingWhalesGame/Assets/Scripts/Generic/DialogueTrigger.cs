using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Made by Daniel Otaigbe
public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        //finds the dialogue manager and feeds it our dialogue object with the name and sentences so we can put it on the screen!
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
