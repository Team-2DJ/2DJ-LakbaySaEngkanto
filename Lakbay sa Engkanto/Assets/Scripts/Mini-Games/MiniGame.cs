using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGame : MonoBehaviour
{
    // On Start (On Collision)
    // Close the doors

    // Execute Game

    // On Win
    // Open the Doors

    public Collider2D PlayerCollider;

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public Coroutine MiniGameStarter()
    {
        return StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(3f);

        Debug.Log("BUGTONG START");
        InitMiniGame();
    }

    public virtual void InitMiniGame()
    {

    }
}
