/*
 John Pope
 This is a test script for the player controller. It will be used to test various settings and functions with the new input system before implementing a new permanent player controller
this script should not be used outside of testing environments and it should not remain implemented outside of the testing phase for movement related to the player controller
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestController : MonoBehaviour
{

    #region Variables
    #region General Variables
    [Header("General Variables")]
    protected Rigidbody2D rb2d; //the rigidbody of the test character
    protected PlayerPawn pawn; //the PlayerPawn class so that we can access the player characters movement data
    protected PlayerInput playerInput; //defines the input that the pawn is utlizing 
    protected PlayerInputActions playerInputActions; //variable for storing the input schemes the pawn will be using
    [SerializeField]
    protected LayerMask groundLayer; //The layer mask for what is considered "the ground" in the game
    #endregion
    #region Jump Variables
    [Header("Jump Variables")]
    protected int verticalVelocity; //the vertical acceleration of the player pawn
    protected bool grounded; //determines if the pawn is touching the ground or not
    [SerializeField]
    protected Transform groundCheck; //the specified location that decides whether the pawn is touching the ground or not
    [SerializeField]
    protected float circleRadius; //determines the size of the capsule at the feet of the pawn
    [Header("Lateral Movement Variables")]
    protected int walkVelocity; //the stand in variable for walk speed on this script
    protected int runVelocity; //the stand in variable for run speedon this script
    protected float inputX; //stores the players x axis input
    bool facingRight = true; //a bool that signifies whether the character is facing right, initializes as true

    #endregion
    #endregion

    #region Functions
    protected virtual void Awake()
    {
        pawn = GetComponent<PlayerPawn>(); //defines the pawn needed for all test character stats
        rb2d = GetComponent<Rigidbody2D>(); //defines the Gameobjects needed for the test character
        playerInput = GetComponent<PlayerInput>(); //defines the initial input system being used by the pawn

        #region Player Action Subscriptions
        PlayerInputActions playerInputActions = new PlayerInputActions(); //initializes te players inputs
        playerInputActions.PlayerHuman.Enable(); //enables the players input from the specfied input actions
        playerInputActions.PlayerHuman.Jump.performed += Jump; //subscribes to the jump function
        playerInputActions.PlayerHuman.Move.performed += Move; // subscribes to the jump function
        #endregion
    }
    protected virtual void FixedUpdate() 
    {
        #region
        grounded = Physics2D.OverlapCircle(groundCheck.position, circleRadius, groundLayer); //this update checks to see if the player is grounded
        walkVelocity = pawn.WalkSpeed; //sets the walkVelocity variable equal to that of the protected variable _walkSpeed on the playerpawn
        rb2d.velocity = new Vector2(inputX * walkVelocity, rb2d.velocity.y); //moves the pawn left and right based on player input
        FlipSprite(inputX); //flips the sprite of the character when moving left
    }
    #region Action Input Functions
    public virtual void Jump(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if (context.performed)
        {
            if (grounded) //only allows the player to jump if they're on the ground
            {
                verticalVelocity = pawn.JumpHeight; //sets the verticalVelocity variable equal to that of the protected variable jumpHeight on playerpawn
                rb2d.velocity = Vector2.up * verticalVelocity; //applies velocity to the upward vector causing the character to jump
            }
        }
    }

    public virtual void Move(InputAction.CallbackContext context)
    {
      inputX = context.ReadValue<Vector2>().x; //reads the value of the x input the player is using
    }
    #endregion
    #endregion

    #region Orientation Functions
    protected virtual void FlipSprite(float inputX) 
    {
        if (inputX < 0 && facingRight || inputX > 0 && !facingRight) 
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale; //sets our variable for scacle equal to the local scale of the pawn
            scale.x *= -1; //multiplies the x of our pawns scale by -1
            transform.localScale = scale; //sets the local scale equal to our custom scale
        }
    }
    #endregion
    #region Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, circleRadius); //draws a sphere around our ground check empty so that we can visualize it
    }
    #endregion
    #endregion
}
