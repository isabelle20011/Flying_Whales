using UnityEngine;

public class bunnyController : MonoBehaviour
{
	public int turnAngle = 25;
	private bool spinning = true;
	private float gameTimer;
	private int seconds;

	private Animator anim;

	private void Start()
	{
		anim = GetComponent<Animator>();
		gameTimer = Time.time;
	}

	void Update()
	{
		if (Time.time > gameTimer + 1)
		{
			gameTimer = Time.time;
			seconds++;
		}

		if (seconds == 5)
		{
			seconds = 0;
			spinning = !spinning;
		}

		if (spinning)
		{
			transform.Rotate(0, turnAngle * Time.deltaTime, 0);
		}
		anim.SetBool("Spinning", spinning);
	}
}
