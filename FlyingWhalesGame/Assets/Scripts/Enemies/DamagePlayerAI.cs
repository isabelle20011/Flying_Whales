﻿using UnityEngine;
using System.Collections;


public class DamagePlayerAI : MonoBehaviour
{
	public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
	public bool KillOnTouch = false;
	public bool DestroyAfterDamage = false;

	GameObject player;                          // Reference to the player GameObject.
	PlayerHealth playerHealth;                  // Reference to the player's health.
	private PlayerMovement playerMovement;
	bool playerInRange;                         // Whether player is within the trigger collider and can be attacked.
	float timer;                                // Timer for counting up to the next attack.


	void Start()
	{
		// Setting up the references.
		player = GameObject.FindGameObjectWithTag("Player");
		playerHealth = player.GetComponent<PlayerHealth>();
		playerMovement = player.GetComponent<PlayerMovement>();
	}


	void OnTriggerEnter(Collider other)
	{
		// If the entering collider is the player...
		if (other.gameObject == player)
		{
			Debug.Log("hi");
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
		// Add the time since Update was last called to the timer.
		timer += Time.deltaTime;
		if (playerHealth && playerMovement)
		{
			// If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
			if (timer >= timeBetweenAttacks && playerInRange && !playerMovement.IsAttacking())
			{
				Attack();
			}
		}
	}


	void Attack()
	{
		// Reset the timer.
		timer = 0f;
		if (KillOnTouch && playerHealth.PlayerAlive())
		{
			playerHealth.Death();
		}
		else if (playerHealth.PlayerAlive())
		{
			playerHealth.TakeDamage();
		}
		if (DestroyAfterDamage)
		{
			Destroy(gameObject);
		}
	}
}

