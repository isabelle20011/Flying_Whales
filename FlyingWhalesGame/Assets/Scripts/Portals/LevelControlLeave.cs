using UnityEngine;

using UnityEngine.SceneManagement;

public class LevelControlLeave : MonoBehaviour
{
	public string levelName;
	void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			SceneManager.LoadScene(levelName);
		}
	}
}
