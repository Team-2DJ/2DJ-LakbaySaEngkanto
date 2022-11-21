using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Transform GroundCheck;                               // Checks if Player is On the Ground
    [SerializeField] LayerMask GroundMask;                                // Layer in Which that Player Would be Able to Jump
    [SerializeField] int MultipleJumpAmount;                              // Number of Times the Player can Jump In the Air
    [SerializeField] float MovementSpeed;                                 // Amount on How Fast can the Player Move
    [SerializeField] float CoyoteTime;                                    // Time When Player Can Still Jump When Off Ground
    [SerializeField] float JumpForce;                                     // Amount of Force the Player Can Jump
    [SerializeField] float GroundCheckRadius;                             // Radius of the Ground Checker
    [SerializeField] float CurrentSpeed { get; set; }                     // Current Movement Speed of the Player
    public float HorizontalInput { get; set; }                            // Checks Player Input
    public bool IsTesting;                                                // For Debugging Purposes


    private PlayerSetup playerSetup;                                      // Player Setup Class Reference
    private float coyoteTimer;                                            // Coyote Time Counter
    private int currentJumpAmount;                                        // Air Jump Amount Tracker
    private Vector3 scale;                                                // Player Scale Reference

    void OnDisable()
    {
        SingletonManager.Get<GameEvents>().OnSlowDownPlayer -= DividePlayerSpeed;
        SingletonManager.Get<GameEvents>().OnIncreasePlayerSpeed -= MultiplyPlayerSpeed;
    }

    #region Initialization Functions
    // Start is called before the first frame update
    void Start()
    {
        SingletonManager.Get<GameEvents>().OnSlowDownPlayer += DividePlayerSpeed;
        SingletonManager.Get<GameEvents>().OnIncreasePlayerSpeed += MultiplyPlayerSpeed;

        // Cache-In Variables
        playerSetup = GetComponent<PlayerSetup>();
        CurrentSpeed = MovementSpeed;
        currentJumpAmount = MultipleJumpAmount;
        scale = transform.localScale;
    }
    #endregion

    #region Update Functions
    void Update()
    {
        if (IsTesting)
            HorizontalInput = Input.GetAxisRaw("Horizontal");
        
        ControlAnimation();
        GroundChecking();
        Flip();
    }

    void FixedUpdate()
    {
        // Move the Player based on Input
        playerSetup.Rb.velocity = new Vector2(HorizontalInput * CurrentSpeed, playerSetup.Rb.velocity.y);
    }
    #endregion

    #region Animation Callbacks
    /// <summary>
    /// Manages Player Animations
    /// </summary>
    void ControlAnimation()
    {
        playerSetup.Animator.SetFloat("velocityX", Mathf.Abs(HorizontalInput));
        playerSetup.Animator.SetFloat("velocityY", playerSetup.Rb.velocity.y);
        playerSetup.Animator.SetBool("isJumping", !IsGrounded());
    }
    #endregion

    #region Jumping Mechanics
    public void Jump(bool value)
    {        
        JumpCut(value);
        MultipleJump(value);
    }

    void JumpCut(bool value)
    {
        // Tap Jump
        if (value && coyoteTimer > 0f)
        {
            playerSetup.Rb.velocity = new Vector2(playerSetup.Rb.velocity.x, JumpForce);
            coyoteTimer = 0f;
        }

        // Stop Jump Upon Button Release
        if (!value && playerSetup.Rb.velocity.y > 0f)
        {
            playerSetup.Rb.velocity = new Vector2(playerSetup.Rb.velocity.x, playerSetup.Rb.velocity.y / 2f);
            coyoteTimer = 0f;
        }
    }

    /// <summary>
    /// Enables Multiple Jumping in Mid-Air
    /// </summary>
    /// <param name="value"></param>
    void MultipleJump(bool value)
    {
        if (value && currentJumpAmount > 0 && coyoteTimer < 0f)
        {
            playerSetup.Rb.velocity = new Vector2(playerSetup.Rb.velocity.x, JumpForce);
            currentJumpAmount--;
        }
    }
    #endregion

    void GroundChecking()
    {
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

    /// <summary>
    /// Flip Entire GameObject based on Horizontal Player Input
    /// </summary>
    void Flip()
    {
        // Whenever The Player Presses the Directional Buttons
        if (HorizontalInput != 0f)
            // Flip Player Based on Horizontal Input
            transform.localScale = new Vector3(scale.x * HorizontalInput, scale.y, scale.z);
    }


    #region Conditional Functions
    /// <summary>
    /// Checks if Player is Currently Stepping on a Ground Layered Platform
    /// </summary>
    /// <returns></returns>
    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(GroundCheck.position, GroundCheckRadius, GroundMask);
    }
    #endregion

    #region Public Functions
    public void DividePlayerSpeed(float value)
    {
        CurrentSpeed /= value;
    }

    public void MultiplyPlayerSpeed(float value)
    {
        CurrentSpeed *= value;
    }
    #endregion
}