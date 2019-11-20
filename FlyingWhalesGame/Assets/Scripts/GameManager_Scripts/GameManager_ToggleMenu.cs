using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
{
	public class GameManager_ToggleMenu : MonoBehaviour
	{

		public GameObject menu;

		private void OnEnable()
		{
			DontDestroyOnLoad(menu);
			Time.timeScale = 1;
			GameManager_Master.Instance.MenuToggleEvent += ToggleMenu;

		}

		private void Update()
		{
			CheckForMenuToggleRequest();
		}

		private void OnDisable()
		{
			GameManager_Master.Instance.MenuToggleEvent -= ToggleMenu;
		}

		private void CheckForMenuToggleRequest()
		{
			if (Input.GetKeyUp(KeyCode.Escape) && !GameManager_Master.Instance.isGameOver && SceneManager.GetActiveScene().buildIndex != 0)
			{
				GameManager_Master.Instance.CallEventMenuToggle();
			}
		}

		private void ToggleMenu()
		{
			if (menu != null)
			{
				menu.SetActive(!menu.activeSelf);
				GameManager_Master.Instance.isMenuOn = !GameManager_Master.Instance.isMenuOn;
			}
			else
			{
				Debug.LogWarning("Need to assign UI GameObject to Toggle Menu script in the inspector");
			}
		}
	}
}