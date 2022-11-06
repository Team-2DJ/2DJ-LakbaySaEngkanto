using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChichayMovement : MonoBehaviour
{
    // Chichay State Machine
    enum States
    {
        IDLE,
        FLYING
    };


    [SerializeField] Transform FollowPoint;                         // Point Where Chichay Needs to Go to
    [SerializeField] float MovementSpeed;                           // Default Movement Speed
    [SerializeField] Animator Animator;                             // Animator Controller Component Reference

    private float currentSpeed;                                     // Current Movement Speed
    private States currentState;                                    // Current State
    private Vector3 scale;                                          // Default Scale Reference

    // Start is called before the first frame update
    void Start()
    {
        // Initialize Scale Values
        scale = transform.localScale;

        currentSpeed = MovementSpeed;
    }

    #region Update Functions
    // Update is called once per frame
    void Update()
    {
        Flip();
        FollowPlayer();
        MovementAnimations();
    }
    #endregion

    #region Private Functions
    /// <summary>
    /// Flip GameObject Based on Movement Direction
    /// </summary>
    void Flip()
    {
        // Get Player Horizontal Input Reference from PlayerManager
        float horizontalDirection = SingletonManager.Get<PlayerManager>().Player.PlayerMovement.HorizontalInput;

        // If Player Is Moving
        if (horizontalDirection != 0f)
            // Flip Sprite Based on Current Direction the Player is Facing
            gameObject.transform.localScale = new Vector3(scale.x * horizontalDirection, scale.y, scale.z);
    }

    /// <summary>
    /// Follow a Certain Point Near the Player
    /// </summary>
    void FollowPlayer()
    {
        // If Chichay has Reached the FollowPoint
        if (Vector2.Distance(transform.position, FollowPoint.position) <= 0f)
        {
            // Set State to Idle
            currentState = States.IDLE;
        }
        // Otherwise...
        else
        {
            // Set State to Flying
            currentState = States.FLYING;

            // Move Towards Target Position
            transform.position = Vector2.MoveTowards(transform.position, FollowPoint.position, currentSpeed * Time.deltaTime);
        }
    }
    #endregion

    #region Animation Functions
    /// <summary>
    /// Handles Chichay's Idle and Flying Animations
    /// </summary>
    void MovementAnimations()
    {
        // Animate Chichay based on Current State
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

    /// <summary>
    /// Triggers Hurt Animation
    /// </summary>
    void OnHurt()
    {
        Animator.SetTrigger("isHurt");
    }

    /// <summary>
    /// Trigers Death Animation
    /// </summary>
    void OnDeath()
    {
        Animator.SetTrigger("isDead");
    }
    #endregion
}
