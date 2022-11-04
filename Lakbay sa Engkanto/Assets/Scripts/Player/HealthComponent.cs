using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    public float DefaultHealth;                                     // Default HP


    private float currentHealth;                                    // Current HP

    #region Initialization Functions
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
    }
    #endregion

    void Initialize()
    {
        currentHealth = DefaultHealth;
    }

    #region HP System
    public void TakeDamage(float damage)
    {
        // Decrement HP based on Damage
        currentHealth -= damage;

        // If Current HP is 0 or Less
        if (currentHealth < 0f)
        {
            // Clamp HP to 0
            // Prevents Negative HP
            currentHealth = 0f;
            
            // Call Death
            OnDeath();
        }
    }

    // Executes Death Functionality
    public void OnDeath()
    {
        // Insert Death VFX Here
    }
    #endregion
}
