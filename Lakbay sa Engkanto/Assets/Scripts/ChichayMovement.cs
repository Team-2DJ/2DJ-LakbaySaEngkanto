using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChichayMovement : MonoBehaviour
{
    enum States
    {
        IDLE,
        FLYING
    };
    
    
    public Transform FollowPoint;
    public float MovementSpeed;
    public GameObject Graphic;
    public Animator Animator;


    private States currentState;
    private Vector2 direction;
    private Vector3 scale;
    private bool facingRight;

    // Start is called before the first frame update
    void Start()
    {
        scale.x = gameObject.transform.localScale.x;
        scale.y = gameObject.transform.localScale.y;
        scale.z = gameObject.transform.localScale.z;
    }

    // Update is called once per frame
    void Update()
    {
        Flip();
        FollowPlayer();
        Animation();
    }

    // Flip GameObject Based on Movement Direction
    void Flip()
    {
        // Calculate Facing Direction
        direction = (FollowPoint.position - transform.position).normalized;

        Debug.Log(direction);

        if (direction.x != 0f)
        {
            // If Moving to the Right
            if (direction.x > 0f)
            {
                gameObject.transform.localScale = new Vector3(scale.x * (direction.x / direction.x), scale.y, scale.z);
            }
            // If Moving to the Left
            else if (direction.x < 0f)
            {
                gameObject.transform.localScale = new Vector3(scale.x * -(direction.x / direction.x), scale.y, scale.z);
            }
        }
    }

    // Follow a Certain Point Near the Player
    void FollowPlayer()
    {
        if (Vector2.Distance(transform.position, FollowPoint.position) <= 0f)
        {
            // Set State to Idle
            currentState = States.IDLE;
        }
        else
        {
            // Set State to Flying
            currentState = States.FLYING;

            // Move Towards Target Position
            transform.position = Vector2.MoveTowards(transform.position, FollowPoint.position, MovementSpeed * Time.deltaTime);
        }
    }

    void Animation()
    {
        switch (currentState)
        {
            case States.IDLE:
                Animator.SetBool("isFlying", false);
                break;

            case States.FLYING:
                Animator.SetBool("isFlying", true);
                break;
        }
    }

    // Triggers Hurt Animation
    void OnHurt()
    {
        Animator.SetTrigger("isHurt");
    }

    // Trigers Death Animation
    void OnDeath()
    {
        Animator.SetTrigger("isDead");
    }
}
