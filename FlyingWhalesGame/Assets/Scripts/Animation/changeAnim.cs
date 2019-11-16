using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeAnim : MonoBehaviour
{
	public void ExecuteJumpSound()
	{
		Debug.Log("bieng");
		EventManager.TriggerEvent<jumpSoundEvent, Vector3>(transform.position);
	}
}
