using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasInstance: MonoBehaviour
{
	public static canvasInstance instance { get; private set; }
	private void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad(this.gameObject);
	}
}