using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GreenCloud : MonoBehaviour
{
    UnityEvent<float> ActorHit = new();
    [SerializeField] float damage;

    private void Start()
    {
        ActorHit.AddListener(SingletonManager.Get<PlayerManager>().Player.GetComponent<HealthComponent>().TakeDamage);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DamagePlayer();
    }

    protected void DamagePlayer()
    {
        ActorHit.Invoke(damage);
    }

    private void OnDestroy()
    {
        ActorHit.RemoveListener(SingletonManager.Get<PlayerManager>().Player.GetComponent<HealthComponent>().TakeDamage);
    }
}
