using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//made by Daniel Otaigbe
public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences; //works like a list, but more restricted. It's FIFO (First in, First Out) so new sentences are loaded from the end of the queu
    public bool inConvo = false;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting conversation with " + dialogue.name);

        sentences.Clear();

        //going through the queue and putting in all the sentences we want to show
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence); //adds all the sentences to the queue
        }
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            inConvo = false;
            EndDialogue();
            return;
        }
        inConvo = true;
        string sentence = sentences.Dequeue(); //removes and returns the first object of the queue
        Debug.Log(sentence);

    }

    void EndDialogue()
    {
        Debug.Log("End of conversation.");
    }
}
