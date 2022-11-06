using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Santelmo : Enemy
{
    [Header("AI Setup")]
    Transform rotationCenter;
    [SerializeField] float rotationRadius = 5f;
    [SerializeField] float angularSpeed = 5f;

    float posX, posY, angle = 0f;

    void Awake()
    {
        // Sets the center of rotation of the Santelmo to the parent transform 
        rotationCenter = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {
        MovementPattern();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>())
            DamagePlayer(1);
    }

    #region EnemyMovement
    // Santelmo movement pattern,  moves in a circular pattern based from a center of rotation (rotationCenter)
    protected override void MovementPattern()
    {
        posX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius;
        posY = rotationCenter.position.y + Mathf.Sin(angle) * rotationRadius;
        transform.position = new Vector2(posX, posY);
        angle = angle + Time.deltaTime * angularSpeed;

        if (angle >= 360f) angle = 0f;
    }
    #endregion
}
