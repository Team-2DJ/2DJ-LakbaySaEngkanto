using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))] // Requires the BoxCollider2D to function correctly; 
public class Stick : Item
{
    protected override void ItemCollected()
    {
        SingletonManager.Get<PlayerEvents>().PlayerDamaged(1f);
    }
}
