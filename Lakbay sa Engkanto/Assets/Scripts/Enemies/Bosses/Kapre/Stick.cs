using UnityEngine;
using UnityEngine.Events;

// Requires the RigidBody2D and CircleCollider2D to function properly
[RequireComponent(typeof(Rigidbody2D), typeof(CircleCollider2D))]
public class Stick : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    UnityEvent<float> ActorHit = new();

    Rigidbody2D rb;
    GameObject target;
    Vector2 moveDirection;

    void Start()
    {
        ActorHit.AddListener(SingletonManager.Get<PlayerManager>().Player.GetComponent<HealthComponent>().TakeDamage);

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
            ActorHit.Invoke(1);
        }
    }

    void OnDestroy()
    {
        ActorHit.RemoveListener(SingletonManager.Get<PlayerManager>().Player.GetComponent<HealthComponent>().TakeDamage);
    }

}
