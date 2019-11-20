using UnityEngine;


public class HealPlayer : MonoBehaviour
{
	public bool givelife = false;
	GameObject player;                          // Reference to the player GameObject.
	PlayerHealth playerHealth;                  // Reference to the player's health.
	bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
	float timer;                                // Timer for counting up to the next attack.


	void Start()
	{
		// Setting up the references.
		player = GameObject.FindGameObjectWithTag("Player");
		playerHealth = player.GetComponent<PlayerHealth>();
	}


	void OnTriggerEnter(Collider other)
	{
		// If the entering collider is the player...
		if (other.gameObject == player)
		{
			// ... the player is in range.
			playerInRange = true;
		}
	}


	void OnTriggerExit(Collider other)
	{
		// If the exiting collider is the player...
		if (other.gameObject == player)
		{
			// ... the player is no longer in range.
			playerInRange = false;
		}
	}


	void Update()
	{
		if (playerInRange)
		{
			if (givelife)
			{
				GiveLife();
			}
			else
			{
				Heal();
			}
		}
	}


	void Heal()
	{
		if (playerHealth.PlayerAlive())
		{
			playerHealth.AddHealth();
		}
		Destroy(gameObject);
	}

	void GiveLife()
	{
		if (playerHealth.PlayerAlive())
		{
			playerHealth.GiveLife();
		}
		Destroy(gameObject);
	}
}
