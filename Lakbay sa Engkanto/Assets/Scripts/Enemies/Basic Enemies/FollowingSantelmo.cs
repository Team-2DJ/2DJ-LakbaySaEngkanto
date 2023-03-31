using UnityEngine;

enum States
{
    IDLE,
    CHASE
}

public class FollowingSantelmo : Enemy
{
    [Header("OverlapBox Parameters")]
    [SerializeField] private float acquisitionRadius = 5f;
    [SerializeField] private Vector2 acquisitionRadiusOffset = Vector2.zero;
    [SerializeField] private LayerMask layerToCheck;

    [Header("Gizmos Settings")]

    [SerializeField] private bool showGizmo;
    [SerializeField] private Color gizmoIdleColor = Color.green;
    [SerializeField] private Color gizmoDetectedColor = Color.red;

    [Header("Gameplay Settings")]
    [SerializeField] private float movementSpeed = 5f;

    private Transform target;
    private States currentState;

    // Update is called once per frame
    void Update()
    {
        DetectTarget();
        MovementPattern();
    }

    protected override void MovementPattern()
    {
        switch (currentState)
        {
            case States.IDLE:
                break;
            case States.CHASE:
                transform.position = Vector2.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);
                SpriteFlip();
                break;
        }
    }

    private void DetectTarget()
    {
        Collider2D collider = Physics2D.OverlapCircle((Vector2)transform.position + acquisitionRadiusOffset, acquisitionRadius, layerToCheck);


        if (collider != null)
        {
            currentState = States.CHASE;
            target = collider.transform;
        }
        else
        {
            currentState = States.IDLE;
            target = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == target.GetComponent<Collider2D>())
        {
            Debug.Log("PLAYER HAS ENTERED");
            DamagePlayer(1f);
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmo)
        {
            switch (currentState)
            {
                case States.IDLE:
                    Gizmos.color = gizmoIdleColor;
                    break;
                case States.CHASE:
                    Gizmos.color = gizmoDetectedColor;
                    break;
            }

            Gizmos.DrawSphere((Vector2)transform.position + acquisitionRadiusOffset, acquisitionRadius);
        }
    }

    private void SpriteFlip()
    {
        Vector3 scale = transform.localScale;

        scale.x = target.position.x > transform.position.x ? Mathf.Abs(scale.x) * -1 : Mathf.Abs(scale.x);

        transform.localScale = scale;
    }


}
