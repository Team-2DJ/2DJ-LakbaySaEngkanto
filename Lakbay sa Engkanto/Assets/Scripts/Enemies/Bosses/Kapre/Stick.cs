using UnityEngine;

// Requires the RigidBody2D and CircleCollider2D to function properly
[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Stick : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    Rigidbody2D rb;
    GameObject target;
    Vector2 moveDirection;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        target = SingletonManager.Get<PlayerManager>().Player.gameObject;
        moveDirection = (target.transform.position - transform.position).normalized * moveSpeed;
        rb.velocity = new Vector2(moveDirection.x, moveDirection.y);
        Destroy(this.gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>())
        {
            SingletonManager.Get<GameEvents>().PlayerDamaged?.Invoke(1f);
        }
    }
}
