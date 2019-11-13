using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerMultiple : DialogueTrigger
{
	public Dialogue[] dialogues;
	private int previous = -1;

	public override void TriggerDialogue()
	{
		int randomInd = Random.Range(0, dialogues.Length);
		while (randomInd == previous)
		{
			randomInd = Random.Range(0, dialogues.Length);
		}
		previous = randomInd;
		dialogue = dialogues[randomInd];
		base.TriggerDialogue();
	}
}
