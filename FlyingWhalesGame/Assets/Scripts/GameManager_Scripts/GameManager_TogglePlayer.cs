﻿using System.Collections;
using System.Collections.Generic;
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
            playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
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