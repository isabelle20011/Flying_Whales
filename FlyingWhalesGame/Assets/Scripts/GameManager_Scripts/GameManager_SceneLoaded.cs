using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
{
    public class GameManager_SceneLoaded : MonoBehaviour
    {
        private GameManager_Master gameManagerMaster;
        public Transform SpawnPoint;
        public GameObject PlayerPrefab;
        private void OnEnable()
        {
            SetInitialReferences();
            SceneManager.sceneLoaded += OnLevelFinishedLoading;
            if (SpawnPoint == null)
            {
                Debug.LogWarning("spawn point not set");
            }
            if (PlayerPrefab == null)
            {
                Debug.LogWarning("player prefab not set");
            }
            DontDestroyOnLoad(SpawnPoint.gameObject);
        }

        private void OnDisable()
        {
            SceneManager.sceneLoaded -= OnLevelFinishedLoading;

        }

        private void SetInitialReferences()
        {
            gameManagerMaster = GameManager_Master.Instance;
        }

        private void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
        {
            if (scene.buildIndex != 0)
            {
                Instantiate(PlayerPrefab, SpawnPoint.position, SpawnPoint.rotation);
            }
            GameManager_Master.Instance.CallLivesUI();
        }

    }
}
