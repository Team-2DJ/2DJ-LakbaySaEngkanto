using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenCloud : Hazard
{
    [SerializeField] float damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>())
            OnActHazard();
    }

    public override void OnActHazard()
    {
        SingletonManager.Get<GameEvents>().PlayerDamaged?.Invoke(damage);
    }
}
