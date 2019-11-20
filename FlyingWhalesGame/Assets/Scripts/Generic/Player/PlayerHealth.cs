using System.Collections;
using GameManager;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	public delegate void OnHealthChangedDelegate();
	public OnHealthChangedDelegate onHealthChangedCallback;

	private int startingHealth;                              // The amount of health the player starts the game with.
	[SerializeField] private int currentHealth;                                   // The current health the player has.
	private int maxHealth = 4;
	public AudioClip deathClip;
	public AudioClip damageClip;
	public AudioClip healingClip;
	private GameObject BGM;                                              // (by Sabin Kim) the BGM GameObject

	private Animator anim;                                              // Reference to the Animator component.
	private AudioSource playerAudio;                                    // Reference to the AudioSource component.
	private PlayerMovement playerMovement;                              // Reference to the player's movement.
	private bool isDead;
	private int playerLives;
	private AudioSource BGMSource;                                      // (by Sabin Kim) AudioSource of BGM GameObject
	private ParticleSystem particlesDeath;
	public float Health { get { return currentHealth; } }
	public float MaxHealth { get { return maxHealth; } }

	#region Sigleton
	private static PlayerHealth instance;
	public static PlayerHealth Instance
	{
		get
		{
			if (instance == null)
				instance = FindObjectOfType<PlayerHealth>();
			return instance;
		}
	}
	#endregion

	private void Awake()
	{
		// Setting up the references.
		anim = GetComponentInChildren<Animator>();
		playerAudio = GetComponent<AudioSource>();
		playerMovement = GetComponent<PlayerMovement>();

		// (by Sabin Kim) Find BGM GameObject (which wears "BackgroundMusic" Tag) and get its AudioSource component
		startingHealth = GameManager_Master.Instance.playerHealth;
		// Set the initial health of the player.
		currentHealth = startingHealth;
	}

	private void Start()
	{
		playerLives = GameManager_Master.Instance.playerLives;
		if (onHealthChangedCallback != null)
			onHealthChangedCallback.Invoke();

		BGM = GameObject.FindGameObjectWithTag("BackgroundMusic");
		if (BGM)
		{
			BGMSource = BGM.GetComponent<AudioSource>();
		}
		else
		{
			Debug.LogWarning("No death music found");
		}

		particlesDeath = GetComponentInChildren<ParticleSystem>();
	}

	public void AddHealth()
	{
		if (currentHealth < maxHealth)
		{
			currentHealth += 1;

			if (healingClip != null)
			{
				playerAudio.clip = healingClip;
				playerAudio.Play();
			}

			if (onHealthChangedCallback != null)
				onHealthChangedCallback.Invoke();
		}
	}

	public void GiveLife()
	{
		playerLives++;
		GameManager_Master.Instance.playerLives = playerLives;
		GameManager_Master.Instance.CallLivesUI();
		if (healingClip != null)
		{
			playerAudio.clip = healingClip;
			playerAudio.Play();
		}
	}


	private void Update()
	{
	}

	public void TakeDamage()
	{
		if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Damage"))
		{
			currentHealth--;

			ClampHealth();
			if (currentHealth <= 0 && !isDead)
			{
				// ... it should die.
				Death();
			}

			if (damageClip != null && !isDead)
			{
				playerAudio.clip = damageClip;
				playerAudio.Play();
				anim.SetTrigger("Damage");
			}
		}
	}


	public void Death()
	{
		isDead = true;

		anim.SetTrigger("Die");
		currentHealth = 0;
		ClampHealth();

		if (deathClip != null)
		{
			BGM = GameObject.FindGameObjectWithTag("BackgroundMusic");
			if (BGM)
			{
				BGMSource = BGM.GetComponent<AudioSource>();
			}
			BGMSource.Stop();               // (by Sabin Kim) when Whale dies, stop level BGM
			playerAudio.clip = deathClip;
			playerAudio.Play();
		}
		StartCoroutine(DestroyPlayer());
		playerMovement.enabled = false;
		playerLives--;
		if (playerLives >= 0)
		{
			GameManager_Master.Instance.CallPlayerDied();
		}
		else
		{
			GameManager_Master.Instance.CallEventGameOver();
		}
	}


	IEnumerator DestroyPlayer()
	{
		yield return new WaitForSeconds(2);
		if (particlesDeath)
		{
			Debug.Log("player");
			particlesDeath.transform.parent = null;
			particlesDeath.Play();
			Destroy(particlesDeath.gameObject, particlesDeath.main.duration);
		}
		Destroy(gameObject);
	}


	private void ClampHealth()
	{
		currentHealth = Mathf.Clamp(currentHealth, 0, startingHealth);

		if (onHealthChangedCallback != null)
			onHealthChangedCallback.Invoke();
	}

	public bool PlayerAlive()
	{
		return !isDead;
	}
}

