using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//made by Daniel Otaigbe
public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
	public TextMeshProUGUI helperText;
    public Canvas canvas;
	public GameObject dialogueBox;
	private TransformFollower cameraScript;
	private GameObject player;
	private DialogueTrigger trigger;

    private Queue<string> sentences; //works like a list, but more restricted. It's FIFO (First in, First Out) so new sentences are loaded from the end of the queue
	private int sentencesNum;
    public bool inConvo = false;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        canvas.enabled = false;
		player = GameObject.FindGameObjectWithTag("Player");
		if (player == null)
		{
			Debug.LogWarning("No player found");
		}


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
		sentencesNum = sentences.Count;
		cameraScript.b_offsetPositionDialog = true;
		cameraScript.f_transitionTime = 5f;
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
		if (sentences.Count == sentencesNum - trigger.doSomething)
		{
			trigger.OnDoSomething();
		}

		inConvo = true;
		string sentence = sentences.Dequeue(); //removes and returns the first object of the queue
		dialogueText.text = sentence;
    }

	public virtual void EndDialogue()
	{
		cameraScript.b_offsetPositionDialog = false;
		cameraScript.f_transitionTime = cameraScript.f_transitionTimeFinal;
		dialogueBox.gameObject.SetActive(false);
		trigger.OnTriggerEnd();
		Debug.Log("End of conversation.");

		PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
		simplePlayerMovement sPlayerMovement = player.GetComponent<simplePlayerMovement>();
		if (playerMovement || sPlayerMovement)
		{
			if (playerMovement)
			{
				playerMovement.enabled = true;
			}
			else
			{
				sPlayerMovement.enabled = true;
			}
		}
		SetTrigger(null);
	}

	public void showHelperText()
	{
		canvas.enabled = true;
	}

	public void hideHelperText()
	{
		canvas.enabled = false;
	}

	public void SetTrigger(DialogueTrigger triggerD)
	{
		trigger = triggerD;
	}
}
