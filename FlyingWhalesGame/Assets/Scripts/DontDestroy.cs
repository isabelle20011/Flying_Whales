using UnityEngine;

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
}
