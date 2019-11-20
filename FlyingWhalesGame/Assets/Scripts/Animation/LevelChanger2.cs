using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LevelChanger2 : MonoBehaviour
{
	private Animator animator;
	[SerializeField] private bool NeedForSeconds;
	[SerializeField] private string levelName;

	private void OnEnable()
	{
		animator = GetComponent<Animator>();
	}

	public void FadeToLevel()
	{
		animator.SetTrigger("FadeOut");
	}

	private void OnFadeComplete()
	{
		if (NeedForSeconds)
		{
			animator.SetTrigger("Word");
			StartCoroutine(waitSeconds());
		}
		else
		{
			if (levelName != "")
			{
				SceneManager.LoadScene(levelName);
			}
			else
			{
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
			}
		}
	}

	IEnumerator waitSeconds()
	{
		yield return new WaitForSeconds(1.5f);
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}
}
