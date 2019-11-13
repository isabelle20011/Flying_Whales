using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerMultiple : DialogueTrigger
{
	public Dialogue[] dialogues;

	public override void TriggerDialogue()
	{
		int randomInd = Random.Range(0, dialogues.Length);
		dialogue = dialogues[randomInd];
		base.TriggerDialogue();
	}
}
