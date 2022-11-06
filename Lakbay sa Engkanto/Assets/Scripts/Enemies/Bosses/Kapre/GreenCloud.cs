using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GreenCloud : Hazard
{
    UnityEvent<float> ActorHit = new();
    [SerializeField] float damage;

    private void Start()
    {
        ActorHit.AddListener(SingletonManager.Get<PlayerManager>().Player.HealthComponent.TakeDamage);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>())
            OnActHazard();
    }

    public override void OnActHazard()
    {
        ActorHit.Invoke(damage);
    }

    private void OnDestroy()
    {
        ActorHit.RemoveListener(SingletonManager.Get<PlayerManager>().Player.HealthComponent.TakeDamage);
    }
}
