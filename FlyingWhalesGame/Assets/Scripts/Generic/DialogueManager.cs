using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//made by Daniel Otaigbe
public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
	public TextMeshProUGUI helperText;
    public Canvas canvas;
	public GameObject dialogueBox;
	private TransformFollower cameraScript;

    private Queue<string> sentences; //works like a list, but more restricted. It's FIFO (First in, First Out) so new sentences are loaded from the end of the queu
    public bool inConvo = false;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        canvas.enabled = false;
		
		if (Camera.main)
		{
			cameraScript = Camera.main.GetComponent<TransformFollower>();
			if (cameraScript == null)
			{
				Debug.LogWarning("No main camera follower script");
			}
		}
		else
		{
			Debug.LogWarning("No main camera");
		}
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
		cameraScript.b_offsetPositionDialog = true;
		canvas.enabled = true;
		dialogueBox.gameObject.SetActive(true);
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
		dialogueText.text = sentence;
    }

    public virtual void EndDialogue()
    {
		cameraScript.b_offsetPositionDialog = false;
		dialogueBox.gameObject.SetActive(false);
		Debug.Log("End of conversation.");
    }

	public void showHelperText()
	{
		canvas.enabled = true;
	}

	public void hideHelperText()
	{
		canvas.enabled = false;
	}
}
