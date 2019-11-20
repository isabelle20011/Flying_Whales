using UnityEngine;

using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
	public string levelName;
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			SceneManager.LoadScene(levelName);
		}
	}
}
