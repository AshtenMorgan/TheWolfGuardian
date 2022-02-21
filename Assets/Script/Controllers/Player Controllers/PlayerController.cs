using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Controller
{

    #region Variables
    #region General Player Variables
    [Header("General Player Variables")]
    protected PlayerInput playerInput; //defines the input that the pawn is utlizing 
    protected PlayerInputActions playerInputActions; //variable for storing the input schemes the pawn will be using
    protected PlayerPawn pawn;//variable for storing the pawn
    #endregion
    #endregion
    #region Functions
    // Start is called before the first frame update
    protected override void Awake()
    {
        pawn = GetComponent<PlayerPawn>(); //defines the pawn needed for all stats
        rb2d = GetComponent<Rigidbody2D>(); //defines the Rigidbody needed for pawn physics
        playerInput = GetComponent<PlayerInput>(); //defines the initial input system being used by the pawn
        ani = GetComponent<Animator>(); //defines the animator for the pawn
        combat = GetComponent<Combat>(); //gets the combat script on the pawn
        jumpTimeCounter = jumpTime; //sets the jumpTimeCounter

        #region Player Action Subscriptions
        PlayerInputActions playerInputActions = new PlayerInputActions(); //initializes te players inputs
        playerInputActions.PlayerHuman.Enable(); //enables the players input from the specfied input actions
        playerInputActions.PlayerHuman.JumpStart.performed += JumpStart; //subscribes to the JumpStart function
        playerInputActions.PlayerHuman.JumpEnd.performed += JumpEnd; //subscribes to the JumpEnd function
        playerInputActions.PlayerHuman.Move.performed += Move; // subscribes to the Move function
        playerInputActions.PlayerHuman.SprintStart.performed += SprintStart; //Subscribes to the SprintStart function
        playerInputActions.PlayerHuman.SprintEnd.performed += SprintEnd; //Subscribes to the SprintEnd function
        playerInputActions.PlayerHuman.CrouchStart.performed += CrouchStart; //Subscribes to the CrouchStart function
        playerInputActions.PlayerHuman.CrouchEnd.performed += CrouchEnd; //Subscribes to the CrouchStart function
        playerInputActions.PlayerHuman.LightPunch.performed += LightPunch; //subscribes to the LightPunch function
        playerInputActions.PlayerHuman.HeavyPunch.performed += HeavyPunch; //subscribes to the HeavyPunch function
        playerInputActions.PlayerHuman.Kick.performed += Kick; //subscribes to the HeavyPunch function
        #endregion
    }

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        #region Jumping Updates
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, circleRadius, groundLayer); //this update checks to see if the player is grounded
        ani.SetBool("Grounded", isGrounded);//match bools

        if (!isNotJumping && !isGrounded) //if we are jumping
        {

            if (jumpTimeCounter > 0) //and our jump counter hasnt reached zero
            {
                ani.SetBool("Jumping", true);//tell the animator to start jumping
                verticalVelocity = pawn.JumpHeight; //sets the verticalVelocity variable equal to that of the protected variable jumpHeight on playerpawn
                rb2d.velocity = new Vector2(rb2d.velocity.x, verticalVelocity);
                jumpTimeCounter -= Time.deltaTime; // subtracts time from the jumpTimeCounter
            }
        }
        else if (isGrounded)
        {
            jumpTimeCounter = jumpTime; //if we are grounded, it sets the jumpTimeCounter back to the jumpTime variable
            ani.SetBool("Jumping", false);
        }
        else
        {
            //do nothing
        }

        #endregion
        #region Ground Movement Updates
        SlopeStick();
        if (!pawn.IsSprinting)
        {
            walkVelocity = pawn.WalkSpeed; //sets the walkVelocity variable equal to that of the protected variable _walkSpeed on the playerpawn
            rb2d.velocity = new Vector2(inputX * walkVelocity, rb2d.velocity.y); //moves the pawn left and right based on player input and walking speed
        }
        if (pawn.IsSprinting)
        {
            runVelocity = pawn.RunSpeed; //sets the runVelocity variable equal to that of the protected variable _runSpeed on the playerpawn
            rb2d.velocity = new Vector2(inputX * runVelocity, rb2d.velocity.y); ////moves the pawn left and right based on player input and running speed
        }
        FlipSprite(inputX); //flips the sprite of the character when moving left
        #endregion
    }
    #region Action Input Functions

    public virtual void JumpStart(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isGrounded) //only allows the player to jump if they're on the ground
            {
                isNotJumping = false; //sets the stoppedJumping bool to false so that we have !stoppedJumping
                verticalVelocity = pawn.JumpHeight; //sets the verticalVelocity variable equal to that of the protected variable jumpHeight on PlayerPawn
                rb2d.velocity = new Vector2(rb2d.velocity.x, verticalVelocity);
            }
        }
    }
    public virtual void JumpEnd(InputAction.CallbackContext context)
    {
        jumpTimeCounter = 0; //resets the jumpTimeCounter to zero
        isNotJumping = true; //sets the stoppedJumping bool to true, cause we have stopped jumping
    }
    public virtual void Move(InputAction.CallbackContext context)
    {
        inputX = context.ReadValue<Vector2>().x; //reads the value of the x input the player is using

        //animator
        ani.SetFloat("Speed", Mathf.Abs(inputX));//tell the animator we are moving
    }
    public virtual void SprintStart(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            pawn.IsSprinting = true; //sets the isSprinting variable on the pawn to true
            ani.SetBool("Sprinting", true);  //tell the animator we are sprinting
        }
    }
    public virtual void SprintEnd(InputAction.CallbackContext context)
    {
        //removed grounded check here to fix a bug
        pawn.IsSprinting = false; //sets the isSprinting variable on the pawn to false
        ani.SetBool("Sprinting", false);//tell the animator to stop sprinting
    }

    public virtual void CrouchStart(InputAction.CallbackContext context) 
    {
        if (isGrounded)
        {
            isCrouching = true; //sets the crouching bool to true
            ani.SetBool("Crouched",true);
        }
    }

    public virtual void CrouchEnd(InputAction.CallbackContext context)
    {
        if (isGrounded)
        {
            isCrouching = false; //sets the crouching bool to false
            ani.SetBool("Crouched", false);
        }
    }

    #region Combat Functions
    public virtual void LightPunch(InputAction.CallbackContext context)
    {
        combat.HitA();
    }
    public virtual void HeavyPunch(InputAction.CallbackContext context)
    {
        combat.HitB();
    }
    public virtual void Kick(InputAction.CallbackContext context)
    {
        combat.HitC();
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
        //Gizmos.DrawSphere(groundCheck.position, circleRadius); //draws a sphere around our ground check empty so that we can visualize it
    }
    #endregion
    #endregion
}
