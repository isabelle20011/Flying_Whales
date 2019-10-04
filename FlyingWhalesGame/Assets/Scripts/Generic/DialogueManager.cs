using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//made by Daniel Otaigbe
public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Canvas canvas;

    private Queue<string> sentences; //works like a list, but more restricted. It's FIFO (First in, First Out) so new sentences are loaded from the end of the queu
    public bool inConvo = false;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        canvas = FindObjectOfType<Canvas>();
        canvas.enabled = false;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        nameText.text = dialogue.name;

        sentences.Clear();

        //going through the queue and putting in all the sentences we want to show
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence); //adds all the sentences to the queue
        }
        canvas.enabled = true;
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
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        canvas.enabled = false;
        Debug.Log("End of conversation.");
    }
}
