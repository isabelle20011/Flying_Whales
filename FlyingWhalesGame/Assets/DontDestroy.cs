using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
	private void Awake()
	{
		GameObject[] objs = GameObject.FindGameObjectsWithTag("BackgroundMusic");
		if (objs.Length > 1)
		{
			foreach (GameObject obj in objs)
			{
				if (obj.gameObject != this.gameObject)
				{
					Destroy(obj.gameObject);
				}
			}

		}
		DontDestroyOnLoad(this.gameObject);
	}

    /* (by Sabin Kim) probably not the most efficient way, but it stops the Intro Cutscene Music when the
        actual gameplay begins */
    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "OpenMap")
        {
            Destroy(this.gameObject);
        }
    }
}
