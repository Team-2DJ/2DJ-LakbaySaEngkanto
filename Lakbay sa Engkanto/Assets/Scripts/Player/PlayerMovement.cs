using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public Transform GroundCheck;                               // Checks if Player is On the Ground
    public LayerMask GroundMask;                                // Layer in Which that Player Would be Able to Jump
    public int MultipleJumpAmount;                              // Number of Times the Player can Jump In the Air
    public float MovementSpeed;                                 // Amount on How Fast can the Player Move
    public float CoyoteTime;                                    // Time When Player CAn Still Jump When Off Ground
    public float JumpForce;                                     // Amount of Force the Player Can Jump
    public float GroundCheckRadius;                             // Radius of the Ground Checker
    public float CurrentSpeed { get; set; }                     // Current Movement Speed of the Player


    private PlayerSetup playerSetup;                            // Player Setup Class Reference
    private float horizontalInput;                              // Checks Player Input
    private float coyoteTimer;                                  // Coyote Time Counter
    private int currentJumpAmount;                              // Air Jump Amount Tracker

    #region Initialization Functions
    // Start is called before the first frame update
    void Start()
    {
        // Cache-In Variables
        playerSetup = GetComponent<PlayerSetup>();
        CurrentSpeed = MovementSpeed;
        currentJumpAmount = MultipleJumpAmount;
    }
    #endregion

    void FixedUpdate()
    {
        // Move the Player based on Input
        playerSetup.Rb.velocity = new Vector2(horizontalInput * CurrentSpeed, playerSetup.Rb.velocity.y);

        // If Player is on the Ground
        if (IsGrounded())
        {
            // Reset Coyote Timer
            coyoteTimer = CoyoteTime;

            // Reset Current Jump Amount
            currentJumpAmount = MultipleJumpAmount;
        }
        // If Player is No Longer on the Ground
        else
        {
            // Decrement Coyote Timer
            coyoteTimer -= Time.deltaTime;
        }
    }

    #region Player Input Unity Events
    // Player Movement Event
    public void Move(InputAction.CallbackContext context)
    {
        // Modify Horizontal Input based on Player's Input
        horizontalInput = context.ReadValue<Vector2>().x;

        // Flip Player Upon Movement
        Flip();
    }

    // Player Jump Event
    public void Jump(InputAction.CallbackContext context)
    {
        JumpCut(context);
        MultipleJump(context);
    }
    #endregion

    #region Private Functions
    // Executes a Mario-Style Jumping Mechanic
    // The Height of the Jump is Dependent Upon
    // The Tension of the Player Input
    void JumpCut(InputAction.CallbackContext context)
    {
        // Tap Jump
        if (context.performed && coyoteTimer > 0f)
        {
            playerSetup.Rb.velocity = new Vector2(playerSetup.Rb.velocity.x, JumpForce);
            coyoteTimer = 0f;
        }

        // Stop Jump Upon Button Release
        if (context.canceled && playerSetup.Rb.velocity.y > 0f)
        {
            playerSetup.Rb.velocity = new Vector2(playerSetup.Rb.velocity.x, playerSetup.Rb.velocity.y / 2f);
            coyoteTimer = 0f;
        }
    }

    void MultipleJump(InputAction.CallbackContext context)
    {
        if (context.performed && currentJumpAmount > 0 && coyoteTimer < 0f)
        {
            playerSetup.Rb.velocity = new Vector2(playerSetup.Rb.velocity.x, JumpForce);
            currentJumpAmount--;
        }
    }

    // Flip Entire GameObject based on
    // Horizontal Player Input
    void Flip()
    {
        // If Player is Moving Right
        if (horizontalInput > 0)
        {
            // Face Right
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        // If Player is Moving Left
        else if (horizontalInput < 0)
        {
            // Face Left
            gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
    #endregion

    #region Conditional Functions
    // Checks if Player is Currently Stepping on a Ground Layered Platform
    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.4f, GroundMask);
    }
    #endregion
    public void DividePlayerSpeed(float value)
    {
        CurrentSpeed /= value; 
    }

    public void MultiplyPlayerSpeed(float value)
    {
        CurrentSpeed *= value;
    }
}