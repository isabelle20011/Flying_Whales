using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// Created by Sean Joplin
/// This will be the navigation manager for each agent outside of the 
/// actual movement calculation which are controlled by NavMeshAgent
/// </summary>
public class Navigation : MonoBehaviour
{
	/// <summary>
	/// Should we be doing patrolling behavior
	/// </summary>
	public bool patrol = true;

	/// <summary>
	/// If we should be patrolling, which points should we patrol between
	/// </summary>
	public List<GameObject> patrolSpots = new List<GameObject>();
	public float multiplyBy;
	public int maxAI = 1;
	private int healthAI;

	/// <summary>
	/// The player we want to find and destroy
	/// </summary>
	private GameObject player;

	/// <summary>
	/// Used to move us to places
	/// </summary>
	private NavMeshAgent agent;

	/// <summary>
	/// Which patrol spot is next
	/// </summary>
	private int patrolIndex = 0;

	/// <summary>
	/// Where'd you go cotton eye joe
	/// </summary>
	private Transform curDest = null;

	/// <summary>
	/// Are we currently seeking a patrol point
	/// </summary>
	private bool seekingPatrol = false;

	/// <summary>
	/// Are we currently seeking a player
	/// </summary>
	private bool seekingPlayer = false;

	//animator variables
	private Animator animator;
	private bool attack;
	private bool died;

	//(Sabin Kim) reference to the set of patrol routes
	private GameObject patrolNodes;

	// Start is called before the first frame update
	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
		player = GameObject.FindGameObjectWithTag("Player");
		patrolNodes = GameObject.FindGameObjectWithTag("PatrolNodes");
		healthAI = maxAI;

		if (!animator)
		{
			Debug.LogWarning("no animator in AI");
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (CanMove() && (player && agent.enabled && transform != null))
		{
			if (Vector3.Distance(transform.position, player.transform.position) < 10)
			{
				seekingPlayer = true;
				seekingPatrol = false;
				Seek(player.transform);
			}
			//if the player moved too far away, return to nearest patrol point
			else if (seekingPlayer && Vector3.Distance(transform.position, player.transform.position) >= 10)
			{
				seekingPlayer = false;
				attack = false;
				float closestDist = 999999999;
				for (int i = 0; i < patrolSpots.Count; i++)
				{
					float curDist = Vector3.Distance(transform.position, patrolSpots[i].transform.position);
					if (curDist < closestDist)
					{
						closestDist = curDist;
						patrolIndex = i;
					}
				}
			}
			if (patrol)
			{
				if (!seekingPatrol && !seekingPlayer)
				{
					curDest = patrolSpots[patrolIndex % patrolSpots.Count].GetComponent<Transform>();
					patrolIndex++;
					Seek(curDest);
					seekingPatrol = true;
				}
				else if (Vector3.Distance(transform.position, curDest.position) < 4)
				{
					seekingPatrol = false;
				}
			}
		}
		animator.SetBool("Seeking", seekingPlayer);
		animator.SetBool("Patrol", seekingPatrol);
		animator.SetBool("Attack", attack);
	}

	/// <summary>
	/// Seek the specified destination.
	/// </summary>
	/// <param name="destination">Where should we go next</param>
	private void Seek(Transform destination)
	{
		agent.destination = destination.position;
		if (Vector3.Distance(transform.position, player.transform.position) < 3.5f)
		{
			attack = true;
		}
		else
		{
			attack = false;
		}
	}

	private bool CanMove()
	{
		if (animator.GetCurrentAnimatorStateInfo(0).IsName("Damage") || this.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")
			|| this.animator.GetCurrentAnimatorStateInfo(0).IsName("Attack2") || animator.GetCurrentAnimatorStateInfo(0).IsName("Death"))
		{
			agent.destination = this.transform.position;
			attack = false;
			return false;
		}
		return true;
	}

	private void OnTriggerEnter(Collider other)
	{
		// If the entering collider is the player...
		if (other.CompareTag("Player"))
		{
			PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
			if (playerMovement)
			{
				if (playerMovement.IsAttacking())
				{
					if (healthAI <= 1)
					{
						died = true;
						animator.SetBool("Died", died);
						agent.enabled = false;
						healthAI = 0;
						//Destroy(gameObject, 4f);
						// (Sabin Kim) GameObject patrolNodes won't be needed anymore
						//Destroy(patrolNodes);
						this.enabled = false;
					}
					else
					{
						animator.SetTrigger("Damaged");
						seekingPlayer = true;
						healthAI--;
					}
				}
			}
		}
	}

	public void ExecuteAttackSound()
	{
		EventManager.TriggerEvent<attackSoundEvent, Vector3>(transform.position);
	}

	public void ExecuteDamageSound()
	{
		EventManager.TriggerEvent<damageSoundEvent, Vector3>(transform.position);
	}
}
