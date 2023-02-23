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

    private bool isHurt;

    private void OnEnable()
    {
        SingletonManager.Get<GameEvents>().OnPlayerDamaged += TakeDamage;
    }

    private void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnPlayerDamaged -= TakeDamage;
        Debug.Log("Disable");
    }

    #region Initialization Functions
    // Start is called before the first frame update
    void Start()
    {
        playerSetup = GetComponent<PlayerSetup>();
        Initialize();
    }
    #endregion

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
            StartCoroutine(HurtVFX());
            StartCoroutine(Invincibility());
        }

        Debug.Log("Current Player HP: " + CurrentHealth);
    }

    IEnumerator HurtVFX()
    {
        playerSetup.Animator.SetBool("isHurt", true);
        isHurt = true;

        yield return new WaitForSeconds(0.2f);

        playerSetup.Animator.SetBool("isHurt", false);

        yield return new WaitForSeconds(3f);

        isHurt = false;
    }

    IEnumerator Invincibility()
    {
        gameObject.layer = LayerMask.NameToLayer("Invincibility");
        float flickerDuration = 0.03f;

        while (isHurt)
        {
            PlayerSprite.color = new Color(1, 1, 1, 0.75f);
            yield return new WaitForSeconds(flickerDuration);

            PlayerSprite.color = new Color(1, 1, 1, 0f);
            yield return new WaitForSeconds(flickerDuration);
        }

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
        playerSetup.Animator.SetBool("isDead", true);

        // Put Player to Death Layer to Prevent Collision with
        // Other Objects
        gameObject.layer = LayerMask.NameToLayer("Death");

        yield return new WaitForSeconds(2.0f);

        // Restart Game
        SingletonManager.Get<GameManager>().RestartGame();
    }
    #endregion
}
