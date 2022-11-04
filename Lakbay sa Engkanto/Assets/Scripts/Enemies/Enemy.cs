using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    // Refers to the movement pattern each Enemy type would follow 
    // (Implementations will be found per derived class)
    protected abstract void MovementPattern();

    // To be further implemented, used in damaging a Player when it hits an enemy
    protected void DamagePlayer()
    {
        Debug.Log("Player has been hit");
    }
}
