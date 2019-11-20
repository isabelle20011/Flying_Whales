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
	public override void OnDoSomething()
	{
		base.OnDoSomething();
		if (portal)
		{
			portal.SetActive(true);
			EventManager.TriggerEvent<portalSpawnSoundEvent, Vector3>(transform.position);
		}
	}

	public override void OnTriggerEnd()
	{
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