using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkEnd : MonoBehaviour
{
	private CountdownTimer countdown;
	private DialogueTriggerCountdown dialogue;
	private DialogueTriggerImmediately dialogueTrigger;
	[SerializeField] private GameObject collectible;
    // Start is called before the first frame update
    void Start()
    {
		countdown = GameObject.FindObjectOfType<CountdownTimer>();
		dialogue = GetComponent<DialogueTriggerCountdown>();
		dialogueTrigger = GetComponent<DialogueTriggerImmediately>();

	}

    // Update is called once per frame
    void Update()
    {
        if (!countdown.b_countdown && countdown.fruitPicked < 5)
		{
			restartThing();
		}
		else if (countdown.currTime > 0 && countdown.fruitPicked == 5)
		{
			doWon();
		}
    }

	private void restartThing()
	{
		Debug.Log("loss");
		this.enabled = false;
		if (dialogue)
		{
			dialogue.enabled = true;
		}
	}

	private void doWon()
	{
		collectible.SetActive(true);
		EventManager.TriggerEvent<portalSpawnSoundEvent, Vector3>(transform.position);
		dialogueTrigger.enabled = true;
		countdown.enabled = false;
		this.enabled = false;
	}
}
