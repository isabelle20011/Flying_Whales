﻿using UnityEngine;

public class changeAnim : MonoBehaviour
{
	public void ExecuteJumpSound()
	{
		EventManager.TriggerEvent<jumpSoundEvent, Vector3>(transform.position);
	}

	public void ExecuteAttackSound()
	{
		EventManager.TriggerEvent<playerAttackSoundEvent, Vector3>(transform.position);
	}
}
