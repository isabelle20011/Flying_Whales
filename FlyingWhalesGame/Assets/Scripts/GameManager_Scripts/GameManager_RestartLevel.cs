using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
{
    public class GameManager_RestartLevel : MonoBehaviour
    {
        private void Start()
        {
            GameManager_Master.Instance.RestartLevelEvent += RestartLevel;
        }

        private void OnDisable()
        {
            GameManager_Master.Instance.RestartLevelEvent -= RestartLevel;
        }

        private void RestartLevel()
        {
            print(GameManager_Master.Instance.isMenuOn);
            if (GameManager_Master.Instance.isMenuOn)
            {
                GameManager_Master.Instance.CallEventMenuToggle();
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }
}
