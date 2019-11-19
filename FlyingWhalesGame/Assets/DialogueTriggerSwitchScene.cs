using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueTriggerSwitchScene : DialogueTrigger
{
	public LevelChanger2 level;
	public override void OnTriggerEnd()
	{
		base.OnTriggerEnd();
		int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
		level.FadeToLevel();
	}
}
