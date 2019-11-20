using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTriggerCountdown : DialogueTrigger
{
	[SerializeField] private GameObject[] foods;
	private CountdownTimer countdown;
	private checkEnd end;

	protected override void Start()
	{
		base.Start();
		countdown = GetComponent<CountdownTimer>();
		end = GetComponent<checkEnd>();
	}
	public override void OnDoSomething()
	{
		base.OnDoSomething();
		if (foods.Length > 0)
		{
			foreach (GameObject food in foods)
			{
				food.SetActive(true);
			}
			EventManager.TriggerEvent<portalSpawnSoundEvent, Vector3>(transform.position);
		}
		if (countdown)
		{
			countdown.StartCountDown();
		}
	}
	public override void OnTriggerEnd()
	{
		base.OnTriggerEnd();
		dialogueManager.hideHelperText();
		this.enabled = false;
		if (end)
		{
			end.enabled = true;
		}
	}
}
