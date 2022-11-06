using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] float DefaultHealth;                                     // Default HP
    public float CurrentHealth { get; private set; }                          // Current HP

    void OnDisable()
    {
        SingletonManager.Get<GameEvents>().PlayerDamaged -= TakeDamage;
        Debug.Log("Disable");
    }

    #region Initialization Functions
    // Start is called before the first frame update
    void Start()
    {
        SingletonManager.Get<GameEvents>().PlayerDamaged += TakeDamage;
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

        // If Current HP is 0 or Less
        if (CurrentHealth < 0f)
        {
            // Clamp HP to 0
            // Prevents Negative HP
            CurrentHealth = 0f;

            // Call Death
            OnDeath();
        }

        Debug.Log(CurrentHealth);
    }

    // Executes Death Functionality
    public void OnDeath()
    {
        // Insert Death VFX Here
    }
    #endregion
}
