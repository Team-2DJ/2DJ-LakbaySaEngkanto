using UnityEngine;

// Requires the RigidBody2D and CircleCollider2D to function properly
/*[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))] */

[RequireComponent(typeof(BoxCollider2D))] // Requires the BoxCollider2D to function correctly; 
public class Stick : MonoBehaviour
{
    private Collider2D playerCollider;

    void Start()
    {
        playerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerCollider)
        {
            SingletonManager.Get<GameEvents>().PlayerDamaged(1f);
            
        }

        Destroy(this.gameObject);
    }
}
