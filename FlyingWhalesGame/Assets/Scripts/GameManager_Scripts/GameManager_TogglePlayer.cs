using UnityEngine;

namespace GameManager
{
	public class GameManager_TogglePlayer : MonoBehaviour
	{
		private PlayerMovement playerController;

		private void Start()
		{
			SetInitialReferences();
			GameManager_Master.Instance.MenuToggleEvent += TogglePlayerController;
		}

		private void OnDisable()
		{
			GameManager_Master.Instance.MenuToggleEvent -= TogglePlayerController;

		}

		private void SetInitialReferences()
		{
			GameObject player = GameObject.FindGameObjectWithTag("Player");
			if (player)
			{
				playerController = player.GetComponent<PlayerMovement>();
			}
		}

		private void TogglePlayerController()
		{
			if (playerController != null)
			{
				playerController.enabled = !playerController.enabled;
			}
		}

	}

}