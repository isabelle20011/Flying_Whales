﻿using UnityEngine;

public class DialogueTriggerMultiple : DialogueTrigger
{
	public Dialogue[] dialogues;
	private int previous = -1;
	private int previous2 = -1;
	private bool wentOnce = false;
	private int index = -1;

	public override void TriggerDialogue()
	{
		if (!wentOnce)
		{
			if (index == dialogues.Length - 2)
			{
				wentOnce = true;
			}
			index++;
		}
		else
		{
			index = Random.Range(0, dialogues.Length);
			while (index == previous || index == previous2)
			{
				index = Random.Range(0, dialogues.Length);
			}
			previous2 = previous;
			previous = index;
		}
		dialogue = dialogues[index];
		base.TriggerDialogue();
	}

	protected override void deleteExclamation()
	{
		if (index == dialogues.Length - 1)
		{
			if (exclamation.Length > 0)
			{
				foreach (GameObject excla in exclamation)
				{
					excla.SetActive(false);
				}
			}
		}
	}
}
