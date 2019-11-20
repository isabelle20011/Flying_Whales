using TMPro;
using UnityEngine;

public class CountdownTimer : MonoBehaviour
{
	[HideInInspector] public float currTime = 0f;
	[HideInInspector] public bool b_countdown = false;
	[SerializeField] private float startTime = 40f;
	[SerializeField] public TextMeshProUGUI countdown;
	[HideInInspector] public int fruitPicked = 0;


	private void Start()
	{
		currTime = startTime;
	}

	public void StartCountDown()
	{
		countdown.gameObject.SetActive(true);
		b_countdown = true;
	}

	private void Update()
	{
		if (b_countdown)
		{
			currTime -= 1 * Time.deltaTime;
			countdown.text = currTime.ToString("0");

			if (currTime <= 0)
			{
				currTime = 0;
				countdown.gameObject.SetActive(false);
				currTime = startTime;
				b_countdown = false;
			}
		}
	}
}
