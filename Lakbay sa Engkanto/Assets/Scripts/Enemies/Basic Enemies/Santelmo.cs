using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Santelmo : Enemy
{
    [Header("AI Setup")]
    Transform rotationCenter;
    [SerializeField] float rotationRadius = 5f;
    [SerializeField] float angularSpeed = 5f;

    float posX, posY, angle = 0f;
    Vector3 pos, velocity, scale;
    [SerializeField] bool isClockwise;

    void Awake()
    {
        // Sets the center of rotation of the Santelmo to the parent transform 
        rotationCenter = transform.parent;

        // Set Default Position and Scale Values
        pos = transform.position;
        scale = transform.localScale;

        if (isClockwise)
            angularSpeed *= -1;
        else
            angularSpeed *= 1;
    }

    // Update is called once per frame
    void Update()
    {
        MovementPattern();
        SpriteFlip();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == SingletonManager.Get<PlayerManager>().Player.GetComponent<Collider2D>())
            DamagePlayer(1f);
    }

    #region Enemy Movement
    // Santelmo movement pattern,  moves in a circular pattern based from a center of rotation (rotationCenter)
    protected override void MovementPattern()
    {
        posX = rotationCenter.position.x + Mathf.Cos(angle) * rotationRadius;
        posY = rotationCenter.position.y + Mathf.Sin(angle) * rotationRadius;

        transform.position = new Vector2(posX, posY);
        angle = angle + Time.deltaTime * angularSpeed;

        if (angle >= 360f) 
            angle = 0f;
    }
    #endregion

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
    
    void OnDrawGizmos()
    {
        if (rotationCenter != null)
        {
            // Draws a blue line from this transform to the target
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, rotationCenter.position);
        }
    }
}
