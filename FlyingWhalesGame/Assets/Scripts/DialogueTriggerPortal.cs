using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerPortal : DialogueTrigger
{
	[SerializeField] private GameObject portal;
	private Collider[] colliders;

	protected override void Start()
	{
		base.Start();
		colliders = GetComponents<Collider>();
	}
	public override void OnTriggerEnd()
	{
		base.OnTriggerEnd();
		portal.SetActive(true);
		this.enabled = false;
		foreach (Collider collider in colliders)
		{
			if (collider.isTrigger)
			{
				collider.enabled = false;
			}
		}
		dialogueManager.hideHelperText();
	}
}
