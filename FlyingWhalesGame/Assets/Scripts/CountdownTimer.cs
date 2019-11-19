using UnityEngine;
using TMPro;

public class CountdownTimer : MonoBehaviour
{
	private float currTime = 0f;
	private bool b_countdown = false;
	[SerializeField] private float startTime = 10f;
	[SerializeField] private TextMeshProUGUI countdown;


	private void Start()
	{
		currTime = startTime;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			countdown.gameObject.SetActive(true);
			b_countdown = true;
		}
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
			}

		}
	}
}
