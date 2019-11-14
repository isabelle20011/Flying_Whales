using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using GameManager;

public class PlayerHealth : MonoBehaviour
{
    public delegate void OnHealthChangedDelegate();
    public OnHealthChangedDelegate onHealthChangedCallback;

    private int startingHealth;                              // The amount of health the player starts the game with.
    [SerializeField]private int currentHealth;                                   // The current health the player has.
    private int maxHealth = 4;
    public AudioClip deathClip;
    public AudioClip damageClip;
    public AudioClip healingClip;
    public GameObject BGM;                                              // (by Sabin Kim) the BGM GameObject

    private Animator anim;                                              // Reference to the Animator component.
    private AudioSource playerAudio;                                    // Reference to the AudioSource component.
    private PlayerMovement playerMovement;                              // Reference to the player's movement.
    private bool isDead;
    private int playerLives;
    private AudioSource BGMSource;                                      // (by Sabin Kim) AudioSource of BGM GameObject

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
        BGM = GameObject.FindGameObjectWithTag("BGM");
		if (BGM)
		{
			BGMSource = BGM.GetComponent<AudioSource>();
		}
		else
		{
			Debug.LogWarning("No death music found");
		}
		startingHealth = GameManager_Master.Instance.playerHealth;
        // Set the initial health of the player.
        currentHealth = startingHealth;
	}

    private void Start()
    {
		playerLives = GameManager_Master.Instance.playerLives;
		if (onHealthChangedCallback != null)
			onHealthChangedCallback.Invoke();
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
            BGMSource.Stop();               // (by Sabin Kim) when Whale dies, stop level BGM
            playerAudio.clip = deathClip;
            playerAudio.Play();
        }

        Destroy(gameObject, 4f);
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

