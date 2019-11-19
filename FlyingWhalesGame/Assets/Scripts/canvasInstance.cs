using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameManager;
using TMPro;

public class canvasInstance: MonoBehaviour
{
	public TextMeshProUGUI text;
	private void Start()
	{
		GameManager_PlayerDied sendCanvas = GameManager_Master.Instance.GetComponent<GameManager_PlayerDied>();
		sendCanvas.canvas = this.gameObject;
		sendCanvas.LivesUI = text;
		text.text = GameManager_Master.Instance.playerLives.ToString();

	}
}