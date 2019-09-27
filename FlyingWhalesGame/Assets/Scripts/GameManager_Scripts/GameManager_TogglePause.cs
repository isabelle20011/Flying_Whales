using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManager
{
    public class GameManager_TogglePause : MonoBehaviour
    {

        private bool isPaused;
        private void Start()
        {
            GameManager_Master.Instance.MenuToggleEvent += TogglePause;
        }

        private void OnDisable()
        {
            GameManager_Master.Instance.MenuToggleEvent -= TogglePause;
        }

        private void TogglePause()
        {
            print("ran2");
            if (isPaused)
            {
                Time.timeScale = 1;
                isPaused = false;
            }
            else
            {
                Time.timeScale = 0;
                isPaused = true;
            }
        }
    }
}
