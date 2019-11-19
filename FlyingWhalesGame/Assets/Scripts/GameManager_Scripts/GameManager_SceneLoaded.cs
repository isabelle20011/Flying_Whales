using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
{
	public class GameManager_SceneLoaded : MonoBehaviour
	{
		private void OnEnable()
		{
			SceneManager.sceneLoaded += OnLevelFinishedLoading;
		}

		private void OnDisable()
		{
			SceneManager.sceneLoaded -= OnLevelFinishedLoading;

		}
		private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
		{
			Debug.Log("Level Loaded");
			Debug.Log(scene.name);
			Debug.Log(mode);

			GameObject player = GameObject.FindGameObjectWithTag("Player");
			if (player)
			{
				PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
				if (playerMovement)
				{
					playerMovement.allowCrouch = GameManager_Master.Instance.hasCrouch;
					playerMovement.allowSprinting = GameManager_Master.Instance.hasSprint;
					playerMovement.allowAttack = GameManager_Master.Instance.hasAttack;
				}
			}
		}
	}
}
