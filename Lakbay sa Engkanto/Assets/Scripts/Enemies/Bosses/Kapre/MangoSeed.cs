using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]   // Requires the Box Collider to Function Correctly; 
public class MangoSeed : Item
{
    protected override void ItemCollected()
    {
        // SingletonManager.Get<GameEvents>().PlayerCollectItem(id);
        Debug.Log("+1 point");
    }
}
