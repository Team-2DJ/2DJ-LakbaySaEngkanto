using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Cobweb : Hazard
{
    [SerializeField] private float speedModifier = 5f;

    public override void OnActHazard()
    {
        SingletonManager.Get<GameEvents>().SlowDownPlayer(speedModifier);
        Debug.Log("Hazard activated");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other == SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>())
            SingletonManager.Get<GameEvents>().IncreasePlayerSpeed(speedModifier);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>())
            OnActHazard();
    }
}
