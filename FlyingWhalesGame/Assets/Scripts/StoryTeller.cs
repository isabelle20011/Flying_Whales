using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StoryTeller : MonoBehaviour
{
	public TextMeshProUGUI currentText;
	public Dialogue dialogue;

	private Queue<string> sentences;

	private void Start()
	{
		sentences = new Queue<string>();
		Cursor.visible = false;
		StartDialogue(dialogue);
	}

	private void Update()
	{
		if (Input.GetButtonDown("Jump"))
		{
			DisplayNextSentence();
		}
	}

	public void StartDialogue(Dialogue dialogue)
	{
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
			EndDialogue();
			return;
		}
		string sentence = sentences.Dequeue(); //removes and returns the first object of the queue

		currentText.text = sentence;

	}

	void EndDialogue()
	{
		SceneManager.LoadScene(2);
	}
}
