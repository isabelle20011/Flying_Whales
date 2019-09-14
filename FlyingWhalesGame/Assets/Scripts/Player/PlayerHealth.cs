using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public delegate void OnHealthChangedDelegate();
    public OnHealthChangedDelegate onHealthChangedCallback;

    [SerializeField] private int startingHealth = 1;                              // The amount of health the player starts the game with.
    [SerializeField] private int currentHealth;                                   // The current health the player has.
    [SerializeField] private int maxHealth = 4;
    public Slider healthSlider;                                 // Reference to the UI's health bar.
    public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    public AudioClip deathClip;                                 // The audio clip to play when the player dies.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.


    Animator anim;                                              // Reference to the Animator component.
   // AudioSource playerAudio;                                    // Reference to the AudioSource component.
    PlayerMovement playerMovement;                              // Reference to the player's movement.
    bool isDead;                                                // Whether the player is dead.
    bool damaged;                                               // True when the player gets damaged.

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

    void Awake()
    {
        // Setting up the references.
        anim = GetComponent<Animator>();
        //playerAudio = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();

        // Set the initial health of the player.
        currentHealth = startingHealth;
    }

    public void AddHealth()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += 1;

            if (onHealthChangedCallback != null)
                onHealthChangedCallback.Invoke();
        }
    }


    void Update()
    {
        /*// If the player has just been damaged...
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }*/

        // Reset the damaged flag.
        damaged = false;
    }


    public void TakeDamage()
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth--;

        // Set the health bar's value to the current health.
        // healthSlider.value = currentHealth;

        // Play the hurt sound effect.
        // playerAudio.Play();

        // If the player has lost all it's health and the death flag hasn't been set yet...
        ClampHealth();
        if (currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death();
        }
    }


    public void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;

        // Tell the animator that the player is dead.
        anim.SetTrigger("Die");
        currentHealth = 0;
        ClampHealth();
        //ClampHealth();

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        //playerAudio.clip = deathClip;
        //playerAudio.Play();

        // Turn off the movement and shooting scripts.
        playerMovement.enabled = false;
        Destroy(gameObject, 2f);
    }


    void ClampHealth()
    {
        currentHealth = Mathf.Clamp(currentHealth, 0, startingHealth);

        if (onHealthChangedCallback != null)
            onHealthChangedCallback.Invoke();
    }
}

