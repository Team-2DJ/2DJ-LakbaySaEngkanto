using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] float DefaultHealth;                                     // Default HP
    public float CurrentHealth { get; private set; }                          // Current HP

    [SerializeField] SpriteRenderer PlayerSprite;
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

    void Initialize()
    {
        CurrentHealth = DefaultHealth;
    }

    #region HP System
    public void TakeDamage(float damage)
    {
        // Decrement HP based on Damage
        CurrentHealth -= damage;

        StartCoroutine(HurtVFX());
        StartCoroutine(Invincibility());
        //GetComponent<PlayerSetup>().Animator.Play("Player_Hurt");

        // If Current HP is 0 or Less
        if (CurrentHealth < 0f)
        {
            // Clamp HP to 0
            // Prevents Negative HP
            CurrentHealth = 0f;

            // Call Death
            OnDeath();
        }
        else
        {

        }

        Debug.Log(CurrentHealth);
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
    public void OnDeath()
    {
        // Insert Death VFX Here
    }
    #endregion
}
