using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class MiniGame : MonoBehaviour
{
    // On Start (On Collision)
    // Close the doors

    // Execute Game

    // On Win
    // Open the Doors

    public Collider2D PlayerCollider { get; set; }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    // Start is called before the first frame update
    void Start()
    {
        PlayerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();
    }

    protected void OnGameFinished()
    {

    }

    protected IEnumerator StartMiniGame(Action miniGame)
    {
        yield return new WaitForSeconds(3f);

        miniGame?.Invoke();

        // Return the Function Call
    }
}
