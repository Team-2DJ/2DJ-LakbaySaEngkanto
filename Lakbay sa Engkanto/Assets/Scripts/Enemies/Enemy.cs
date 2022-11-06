using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Enemy : MonoBehaviour
{
    UnityEvent<float> ActorHit = new();

    private void Start()
    {
        ActorHit.AddListener(SingletonManager.Get<PlayerManager>().Player.HealthComponent.TakeDamage);
    }

    /// <summary>
    /// Refers to the movement pattern each Enemy type would follow 
    /// </summary>
    protected abstract void MovementPattern();

    /// <summary>
    /// Used in damaging a Player when it hits an enemy
    /// </summary>

    protected void DamagePlayer(float damage)
    {
        ActorHit.Invoke(damage);
    }

    private void OnDestroy()
    {
        ActorHit.RemoveListener(SingletonManager.Get<PlayerManager>().Player.HealthComponent.TakeDamage);
    }
}
