using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManagerWithSceneChange : DialogueManager
{
	public LevelChanger2 level;
	public override void EndDialogue()
	{
		base.EndDialogue();
		Debug.Log("Scene Switch");
		int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
		level.FadeToLevel();
	}
}
