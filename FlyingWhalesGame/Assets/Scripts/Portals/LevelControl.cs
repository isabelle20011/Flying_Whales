using UnityEngine;

using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
	[SerializeField] private LevelChanger2 level;
	void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			level.FadeToLevel();
		}
	}
}
