using UnityEngine;

public class ChestAnim : MonoBehaviour
{
	private Animator anim;
	public static bool inside = false;

	GameObject player;                          // Reference to the player GameObject.
	PlayerHealth playerHealth;                  // Reference to the player's health.


	void Awake()
	{
		// Setting up the references.
		player = GameObject.FindGameObjectWithTag("Player");
		playerHealth = player.GetComponent<PlayerHealth>();
	}

	// Start is called before the first frame upCdate
	void Start()
	{
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void Update()
	{
		if (inside == true)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				anim.SetTrigger("OpenChest");
				int action = Random.Range(0, 2);
				if (action == 1)
				{
					if (playerHealth.PlayerAlive())
					{
						playerHealth.TakeDamage();
					}

				}
				else
				{
					playerHealth.AddHealth();
				}
			}
		}
		print(playerHealth.Health);

	}
}


