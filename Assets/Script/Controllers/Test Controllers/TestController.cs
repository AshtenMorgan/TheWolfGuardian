///*
// John Pope
// This is a test script for the player controller. It will be used to test various settings and functions with the new input system before implementing a new permanent player controller
//this script should not be used outside of testing environments and it should not remain implemented outside of the testing phase for movement related to the player controller
// */

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;

//public class TestController : MonoBehaviour
//{
//    #region Variables
//    #region General Variables
//    [Header("General Variables")]
//    protected Rigidbody2D rb2d; //the rigidbody of the test character
//    protected PlayerPawn pawn; //the PlayerPawn class so that we can access the player characters movement data
//    protected PlayerInput playerInput; //defines the input that the pawn is utlizing 
//    protected PlayerInputActions playerInputActions; //variable for storing the input schemes the pawn will be using
//    [SerializeField]
//    protected LayerMask groundLayer; //The layer mask for what is considered "the ground" in the game
//    #endregion
//    #region Jump Variables
//    [Header("Jump Variables")]
//    protected float verticalVelocity; //the vertical acceleration of the player pawn
//    protected bool grounded; //determines if the pawn is touching the ground or not
//    [SerializeField]
//    protected float jumpTime; //the time we set in the editor for the maximum amount of time we can jump into the air before we start falling
//    protected float jumpTimeCounter; //the counter that keeps track of jumpTime
//    protected bool stoppedJumping = true; //decides whether the player has stopped jumping
//    [SerializeField]
//    protected Transform groundCheck; //the specified location that decides whether the pawn is touching the ground or not
//    [SerializeField]
//    protected float circleRadius; //determines the size of the capsule at the feet of the pawn
//    [Header("Lateral Movement Variables")]
//    protected int walkVelocity; //the stand in variable for walk speed on this script
//    protected int runVelocity; //the stand in variable for run speedon this script
//    protected float inputX; //stores the players x axis input
//    bool facingRight = true; //a bool that signifies whether the character is facing right, initializes as true
//    #region Animator Variables
//    [Header("Animator Variables")]       
//    public Animator ani;//animator code
//    #endregion
//    #endregion
//    #endregion

//    #region Functions
//    protected virtual void Awake()
//    {
//        pawn = GetComponent<PlayerPawn>(); //defines the pawn needed for all test character stats
//        rb2d = GetComponent<Rigidbody2D>(); //defines the Gameobjects needed for the test character
//        playerInput = GetComponent<PlayerInput>(); //defines the initial input system being used by the pawn

//        jumpTimeCounter = jumpTime; //sets the jumpTimeCounter

//        #region Player Action Subscriptions
//        PlayerInputActions playerInputActions = new PlayerInputActions(); //initializes te players inputs
//        playerInputActions.PlayerHuman.Enable(); //enables the players input from the specfied input actions
//        playerInputActions.PlayerHuman.JumpStart.performed += JumpStart; //subscribes to the JumpStart function
//        playerInputActions.PlayerHuman.JumpEnd.performed += JumpEnd; //subscribes to the JumpEnd function
//        playerInputActions.PlayerHuman.Move.performed += Move; // subscribes to the Move function
//        playerInputActions.PlayerHuman.SprintStart.performed += SprintStart; //Subscribes to the SprintStart function
//        playerInputActions.PlayerHuman.SprintEnd.performed += SprintEnd; //Subscribes to the SprintEnd function
//        #endregion
//    }
//    protected virtual void FixedUpdate() 
//    {
//        #region Jumping Updates
//        grounded = Physics2D.OverlapCircle(groundCheck.position, circleRadius, groundLayer); //this update checks to see if the player is grounded

//        if (!stoppedJumping) //if we are jumping
//        {
//            if (jumpTimeCounter > 0) //and our jump counter hasnt reached zero
//            {
//                verticalVelocity = pawn.JumpHeight; //sets the verticalVelocity variable equal to that of the protected variable jumpHeight on playerpawn
//                rb2d.velocity = new Vector2(rb2d.velocity.x, verticalVelocity);
//                //rb2d.AddForce(Vector2.up * verticalVelocity, ForceMode2D.Impulse); //makes the rigidbody of the pawn jump
//                jumpTimeCounter -= Time.deltaTime; // subtracts time from the jumpTimeCounter
//            }
//        }
//        if (grounded) 
//        {
//            jumpTimeCounter = jumpTime; //if we are grounded, it sets the jumpTimeCounter back to the jumpTime variable
//        }

//        #endregion
//        #region Ground Movement Updates
        
//        if (!pawn.IsSprinting)
//        {
//            walkVelocity = pawn.WalkSpeed; //sets the walkVelocity variable equal to that of the protected variable _walkSpeed on the playerpawn
//            rb2d.velocity = new Vector2(inputX * walkVelocity, rb2d.velocity.y); //moves the pawn left and right based on player input and walking speed
//        }
//        if (pawn.IsSprinting)
//        {
//            runVelocity = pawn.RunMult; //sets the runVelocity variable equal to that of the protected variable _runSpeed on the playerpawn
//            rb2d.velocity = new Vector2(inputX * runVelocity, rb2d.velocity.y); ////moves the pawn left and right based on player input and running speed
//        }
//        FlipSprite(inputX); //flips the sprite of the character when moving left
//        #endregion
//    }

//    #region Action Input Functions
//    public virtual void JumpStart(InputAction.CallbackContext context)
//    {
//        if (context.performed)
//        {
//            if (grounded) //only allows the player to jump if they're on the ground
//            {
//                verticalVelocity = pawn.JumpHeight; //sets the verticalVelocity variable equal to that of the protected variable jumpHeight on PlayerPawn
//                rb2d.velocity = new Vector2(rb2d.velocity.x, verticalVelocity);
//                //rb2d.AddForce(Vector2.up * verticalVelocity, ForceMode2D.Impulse); //makes the rigidbody of the pawn jump
//                stoppedJumping = false; //sets the stoppedJumping bool to false so that we have !stoppedJumping

//                //animator
//                ani.SetBool("Jumping", true);//tell the animator a jump is occuring
//            }
//        }
//    }
//    public virtual void JumpEnd(InputAction.CallbackContext context) 
//    {
//        jumpTimeCounter = 0; //resets the jumpTimeCounter to zero
//        stoppedJumping = true; //sets the stoppedJumping bool to true, cause we have stopped jumping
                              
//        //animator
//        ani.SetBool("Jumping", false);//tell the animator to stop jumping
//    }

//    public virtual void Move(InputAction.CallbackContext context)
//    {
//      inputX = context.ReadValue<Vector2>().x; //reads the value of the x input the player is using

//        //animator
//        ani.SetFloat("Speed", Mathf.Abs(inputX));//tell the animator we are moving
//    }

//    public virtual void SprintStart(InputAction.CallbackContext context) 
//    {
//        if (grounded)
//        {
//            pawn.IsSprinting = true; //sets the isSprinting variable on the pawn to true
//        }
//    }
//    public virtual void SprintEnd(InputAction.CallbackContext context)
//    {
//        if (grounded)
//        {
//            pawn.IsSprinting = false; //sets the isSprinting variable on the pawn to false
//        }
//    }
//    #endregion
//    #endregion

//    #region Orientation Functions
//    protected virtual void FlipSprite(float inputX) 
//    {
//        if (inputX < 0 && facingRight || inputX > 0 && !facingRight) 
//        {
//            facingRight = !facingRight;
//            Vector3 scale = transform.localScale; //sets our variable for scacle equal to the local scale of the pawn
//            scale.x *= -1; //multiplies the x of our pawns scale by -1
//            transform.localScale = scale; //sets the local scale equal to our custom scale
//        }
//    }
//    #endregion
//    #region Gizmos
//    private void OnDrawGizmos()
//    {
//        Gizmos.DrawSphere(groundCheck.position, circleRadius); //draws a sphere around our ground check empty so that we can visualize it
//    }
//    #endregion
//}
