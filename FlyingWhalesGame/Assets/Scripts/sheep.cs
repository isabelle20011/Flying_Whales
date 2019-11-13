using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sheep : MonoBehaviour
{
	public void ExecuteSheepSound()
	{
		EventManager.TriggerEvent<sheepSoundEvent, Vector3>(transform.position);
	}
}
