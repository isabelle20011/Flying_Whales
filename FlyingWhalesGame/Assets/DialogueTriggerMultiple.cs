using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerMultiple : DialogueTrigger
{
	public Dialogue[] dialogues;
	private int previous = -1;
	private int previous2 = -1;

	public override void TriggerDialogue()
	{
		int randomInd = Random.Range(0, dialogues.Length);
		while (randomInd == previous || randomInd == previous2)
		{
			randomInd = Random.Range(0, dialogues.Length);
		}
		previous2 = previous;
		previous = randomInd;
		dialogue = dialogues[randomInd];
		base.TriggerDialogue();
	}
}
