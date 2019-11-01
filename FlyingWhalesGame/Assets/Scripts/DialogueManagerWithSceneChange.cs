using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueManagerWithSceneChange : DialogueManager
{
	public override void EndDialogue()
	{
		base.EndDialogue();
		Debug.Log("Scene Switch");
		int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
		if (SceneManager.sceneCountInBuildSettings > nextSceneIndex)
		{
			SceneManager.LoadScene(nextSceneIndex);
		}
	}
}
