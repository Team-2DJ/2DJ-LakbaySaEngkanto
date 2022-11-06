using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform GroundCheck;                               // Checks if Player is On the Ground
    [SerializeField] LayerMask GroundMask;                                // Layer in Which that Player Would be Able to Jump
    [SerializeField] int MultipleJumpAmount;                              // Number of Times the Player can Jump In the Air
    [SerializeField] float MovementSpeed;                                 // Amount on How Fast can the Player Move
    [SerializeField] float CoyoteTime;                                    // Time When Player CAn Still Jump When Off Ground
    [SerializeField] float JumpForce;                                     // Amount of Force the Player Can Jump
    [SerializeField] float GroundCheckRadius;                             // Radius of the Ground Checker
    [SerializeField] float CurrentSpeed { get; set; }                     // Current Movement Speed of the Player
    public float HorizontalInput { get; private set; }                    // Checks Player Input

    private PlayerSetup playerSetup;                                      // Player Setup Class Reference
    private float coyoteTimer;                                            // Coyote Time Counter
    private int currentJumpAmount;                                        // Air Jump Amount Tracker

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

    #region Update Functions
    void FixedUpdate()
    {
        // Move the Player based on Input
        playerSetup.Rb.velocity = new Vector2(HorizontalInput * CurrentSpeed, playerSetup.Rb.velocity.y);

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
    #endregion

    #region Player Input Unity Events
    /// <summary>
    /// Player Movement Event
    /// </summary>
    /// <param name="context"></param>
    public void Move(InputAction.CallbackContext context)
    {
        // Modify Horizontal Input based on Player's Input
        HorizontalInput = context.ReadValue<Vector2>().x;

        // Flip Player Upon Movement
        Flip();
    }

    /// <summary>
    /// Player Jump Event
    /// </summary>
    /// <param name="context"></param>
    public void Jump(InputAction.CallbackContext context)
    {
        JumpCut(context);
        MultipleJump(context);
    }
    #endregion

    #region Private Functions
    /// <summary>
    /// Executes a Mario-Style Jumping Mechanic Where The Height of the Jump is Dependent Upon The Tension of the Player Input
    /// </summary>
    /// <param name="context"></param>
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

    /// <summary>
    /// Enables Air Jumping A Certain Amount of Times
    /// </summary>
    /// <param name="context"></param>
    void MultipleJump(InputAction.CallbackContext context)
    {
        if (context.performed && currentJumpAmount > 0 && coyoteTimer < 0f)
        {
            playerSetup.Rb.velocity = new Vector2(playerSetup.Rb.velocity.x, JumpForce);
            currentJumpAmount--;
        }
    }

    /// <summary>
    /// Flip Entire GameObject based on Horizontal Player Input
    /// </summary>
    void Flip()
    {
        // If Player is Moving Right
        if (HorizontalInput > 0)
        {
            // Face Right
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        // If Player is Moving Left
        else if (HorizontalInput < 0)
        {
            // Face Left
            gameObject.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }
    #endregion

    #region Conditional Functions
    /// <summary>
    /// Checks if Player is Currently Stepping on a Ground Layered Platform
    /// </summary>
    /// <returns></returns>
    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, 0.4f, GroundMask);
    }
    #endregion
}