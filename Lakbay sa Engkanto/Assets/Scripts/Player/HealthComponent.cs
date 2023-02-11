using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float DefaultHealth;                             // Default HP
    public float CurrentHealth { get; private set; }                          // Current HP
    public bool IsAlive { get; private set; }                                 // Life Condition Indicator

    [SerializeField] private SpriteRenderer PlayerSprite;
    private PlayerSetup playerSetup;
    

    void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnPlayerDamaged -= TakeDamage;
        Debug.Log("Disable");
    }

    #region Initialization Functions
    // Start is called before the first frame update
    void Start()
    {
        SingletonManager.Get<GameEvents>().OnPlayerDamaged += TakeDamage;
        playerSetup = GetComponent<PlayerSetup>();
        Initialize();
    }
    #endregion

    // TO BE REMOVED!!!!
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SingletonManager.Get<GameEvents>().PlayerDamaged(1f);

            //SingletonManager.Get<GameManager>().EndGame();
        }
    }

    void Initialize()
    {
        CurrentHealth = DefaultHealth;
        IsAlive = true;
    }

    #region HP System
    public void TakeDamage(float damage)
    {
        // Decrement HP based on Damage
        CurrentHealth -= damage;

        StartCoroutine(HurtVFX());

        // If Current HP is 0 or Less
        if (CurrentHealth <= 0f)
        {
            // Clamp HP to 0
            // Prevents Negative HP
            CurrentHealth = 0f;

            // Call Death
            StartCoroutine(OnDeath());
        }
        else
        {
            StartCoroutine(Invincibility());
        }

        Debug.Log("Current Player HP: " + CurrentHealth);
    }

    IEnumerator HurtVFX()
    {
        playerSetup.Animator.SetBool("isHurt", true);

        yield return new WaitForSeconds(0.2f);

        playerSetup.Animator.SetBool("isHurt", false);
    }

    IEnumerator Invincibility()
    {
        gameObject.layer = LayerMask.NameToLayer("Invincibility");
        PlayerSprite.color = new Color(1, 0, 0, 0.75f);

        yield return new WaitForSeconds(3f);

        gameObject.layer = LayerMask.NameToLayer("Player");
        PlayerSprite.color = Color.white;
    }

    // Executes Death Functionality
    IEnumerator OnDeath()
    {
        // Indicate Player Death in Bool
        IsAlive = false;
        
        // Disable Movement
        playerSetup.PlayerMovement.enabled = false;

        // Set Rigidbody2D Type to Static to Prevent Further Movement
        playerSetup.Rb.bodyType = RigidbodyType2D.Static;

        // Death VFX
        playerSetup.Animator.SetBool("isHurt", true);
        playerSetup.Animator.SetBool("isDead", true);

        // Put Player to Death Layer to Prevent Collision with
        // Other Objects
        gameObject.layer = LayerMask.NameToLayer("Death");

        yield return new WaitForSeconds(2.0f);

        SingletonManager.Get<GameManager>().RestartGame();
    }
    #endregion
}
