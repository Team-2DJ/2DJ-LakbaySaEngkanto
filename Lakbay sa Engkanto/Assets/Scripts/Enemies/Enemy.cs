using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    /// <summary>
    /// Refers to the movement pattern each Enemy type would follow 
    /// </summary>
    protected abstract void MovementPattern();

    /// <summary>
    /// Used in damaging a Player when it hits an enemy
    /// </summary>

    protected void DamagePlayer(float damage)
    {
        SingletonManager.Get<GameEvents>().PlayerDamaged(damage);
    }
}
