using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    private AudioSource audioSource;
	private void Awake()
	{
        audioSource = GetComponent<AudioSource>();
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
