using System.Collections;
using TMPro;
using UnityEngine;

namespace GameManager
{
	public class GameManager_PlayerDied : MonoBehaviour
	{
		[HideInInspector] public GameObject canvas;
		[HideInInspector] public TextMeshProUGUI LivesUI;

		private void OnEnable()
		{
			GameManager_Master.Instance.PlayerDiedEvent += PlayerDied;
			GameManager_Master.Instance.LivesUIEvent += UpdateUI;

			if (LivesUI == null)
			{
				Debug.LogWarning("missing UI reference");
			}
			if (canvas)
			{
				canvas.SetActive(true);
			}
		}

		private void OnDisable()
		{
			GameManager_Master.Instance.PlayerDiedEvent -= PlayerDied;
			GameManager_Master.Instance.LivesUIEvent -= UpdateUI;

		}

		private void PlayerDied()
		{
			GameManager_Master.Instance.CallLivesUI();
			StartCoroutine(Dead());
		}

		IEnumerator Dead()
		{
			Debug.Log("dead");
			yield return new WaitForSeconds(5);
			GameManager_Master.Instance.CallEventRestartLevel();
		}

		private void UpdateUI()
		{
			if (LivesUI)
			{
				LivesUI.text = GameManager_Master.Instance.playerLives.ToString();
			}
		}
	}
}
