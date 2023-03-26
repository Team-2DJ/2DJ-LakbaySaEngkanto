using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class MovingSantelmo : Enemy
{
    [SerializeField] private GameObject waypointParent;
    [SerializeField] private float moveSpeed = 5f;

    private List<Transform> waypoints = new();
    private Collider2D playerCollider;
    private int waypointIndex;
    private Vector3 pos, velocity, scale;

    void Awake()
    {
        Transform[] temp = waypointParent?.GetComponentsInChildren<Transform>();

        foreach (var transform in temp)
        {
            waypoints.Add(transform);
        }

        waypoints.RemoveAt(0);

        // Set Default Position and Scale Values
        pos = transform.position;
        scale = transform.localScale;
    }

    void Start()
    {
        playerCollider = SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == playerCollider)
            DamagePlayer(1f);
    }

    private void Update()
    {
        MovementPattern();
        SpriteFlip();
    }

    protected override void MovementPattern()
    {
        transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].transform.position,
                                                     moveSpeed * Time.deltaTime);

        if (transform.position == waypoints[waypointIndex].transform.position)
            waypointIndex += 1;

        if (waypointIndex == waypoints.Count)
            waypointIndex = 0;
    }

    /// <summary>
    /// Santelmo Sprite Flipping System
    /// </summary>
    void SpriteFlip()
    {
        // Calculate Velocity
        velocity = (transform.position - pos) / Time.deltaTime;
        pos = transform.position;

        // Flip Sprite Based on Horizontal Velocity
        if (velocity.x > 0f)
            transform.localScale = new Vector3(scale.x * -1f, scale.y, scale.z);
        else
            transform.localScale = new Vector3(scale.x * 1f, scale.y, scale.z);
    }
}
