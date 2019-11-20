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

    // (by Sabin Kim) probably a very bashy/inefficient solution to make the intro cutscene music shut up
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "OpenMap")
        {
            while (audioSource.volume > 0)
            {
                audioSource.volume -= .04f;
            }
            Destroy(this);
        }
    }
}
