//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;

//public class PlayerController : Controller
//{
//    #region Variables
//    #region General Player Variables
//    [Header("General Player Variables")]
//    protected PlayerInput playerInput; //defines the input that the pawn is utlizing 
//    protected PlayerInputActions playerInputActions; //variable for storing the input schemes the pawn will be using
//    protected PlayerPawn pawn;//variable for storing the pawn
//    private bool _interactRange = false;
//    private float currentVelocity; //stores the player's current velocity
//    [SerializeField]
//    private float extraHeightText = 0.1f;

//    public bool InteractRange 
//    {
//        get { return _interactRange; }
//        set { _interactRange = value; }
//    }

//    #endregion
//    #endregion
//    #region Functions
//    // Start is called before the first frame update
//    protected override void Awake()
//    {
//        pawn = GetComponent<PlayerPawn>(); //defines the pawn needed for all stats
//        capsuleCollider2d = GetComponent<CapsuleCollider2D>(); //Fix for no movement temp
//        rb2d = GetComponent<Rigidbody2D>(); //defines the Rigidbody needed for pawn physics
//        playerInput = GetComponent<PlayerInput>(); //defines the initial input system being used by the pawn
//        ani = GetComponent<Animator>(); //defines the animator for the pawn
//        combat = GetComponent<Combat>(); //gets the combat script on the pawn
//        jumpTimeCounter = jumpTime; //sets the jumpTimeCounter

//        #region Player Action Subscriptions
//        playerInputActions = new PlayerInputActions(); //initializes te players inputs
//        playerInputActions.PlayerHuman.Enable(); //enables the players input from the specfied input actions
//        playerInputActions.PlayerHuman.JumpStart.performed += JumpStart; //subscribes to the JumpStart function
//        playerInputActions.PlayerHuman.JumpEnd.performed += JumpEnd; //subscribes to the JumpEnd function
//        playerInputActions.PlayerHuman.Move.performed += Move; // subscribes to the Move function
//        playerInputActions.PlayerHuman.SprintStart.performed += SprintStart; //Subscribes to the SprintStart function
//        playerInputActions.PlayerHuman.SprintEnd.performed += SprintEnd; //Subscribes to the SprintEnd function
//        playerInputActions.PlayerHuman.CrouchStart.performed += CrouchStart; //Subscribes to the CrouchStart function
//        playerInputActions.PlayerHuman.CrouchEnd.performed += CrouchEnd; //Subscribes to the CrouchStart function
//        playerInputActions.PlayerHuman.LightPunch.performed += LightPunch; //subscribes to the LightPunch function
//        playerInputActions.PlayerHuman.HeavyPunch.performed += HeavyPunch; //subscribes to the HeavyPunch function
//        playerInputActions.PlayerHuman.Kick.performed += Kick; //subscribes to the HeavyPunch function
//        #endregion
//    }

//    protected override void Start()
//    {
//        base.Start();
//    }
//    //WIP
//    protected override void SlopeCheck()
//    {
//        if (isGrounded && inputX == 0)
//        {
//            rb2d.sharedMaterial = fullFriction; //changes Physics Material 2D of the rigidbody to our Full Friction material
//        }
//        else
//        {
//            rb2d.sharedMaterial = noFriction; //changes Physics Material 2D of the rigidbody to our No Friction material
//        }
//    }

//    // Update is called once per frame
//    protected override void Update()
//    {
        
//        base.Update();
//    }
    
    
       
    
//    //This NewGrounded bool is experimental *****
//    protected void NewGrounded()
//        //does nothing yet, trying to get this to replace the isGrounded functions or to addon to them. Will be trying to use this in order to fix the can't jump up hill issue.
//        //Will use raycast to detect ground and display a green line from th3e capsule to show the player is on ground, will have to remove the debug lines oince complete.
//        //Use the codemonkey video on "3 ways to groundcheck in unity" for further help on this.
//    {
        
        
//        isGrounded = Physics2D.Raycast(capsuleCollider2d.bounds.center, Vector2.down, capsuleCollider2d.bounds.extents.y + extraHeightText, whatIsGround);
       
//    }
//    //End of experimental code ****


//    //This is the original code
//    protected override void FixedUpdate()
//    {
//        #region Jumping Updates
//        //boxSize, 0, groundLayer
//        NewGrounded();
//        //isGrounded = Physics2D.OverlapBox(groundCheck.position, boxSize, 0, groundLayer); //this update checks to see if the player is grounded
//        ani.SetBool("Grounded", isGrounded);//match bools

//        rb2d.velocity = new Vector2(inputX * currentVelocity, rb2d.velocity.y); //moves the pawn left and right based on player input and running speed

//        if (!isNotJumping && !isGrounded) //if we are jumping
//        {
//            if (jumpTimeCounter > 0) //and our jump counter hasnt reached zero
//            {
//                ani.SetBool("Jumping", true);//tell the animator to start jumping
//                verticalVelocity = pawn.JumpHeight; //sets the verticalVelocity variable equal to that of the protected variable jumpHeight on playerpawn
//                rb2d.velocity += new Vector2(0, verticalVelocity);
//                jumpTimeCounter -= Time.fixedDeltaTime; // subtracts time from the jumpTimeCounter
//            }
//        }
//        else if (isGrounded)
//        {
//            ani.SetBool("Jumping", false);
//        }
//        else
//        {
//            //do nothing
//        }

//        #endregion
//        #region Ground Movement Updates
//        SlopeStick();
//        if(!isCrouching)
        
//            {
//                if (!pawn.IsSprinting && isGrounded)
//                {
//                    currentVelocity = pawn.WalkSpeed; //sets the walkVelocity variable equal to that of the protected variable _walkSpeed on the playerpawn
//                }
//                if (pawn.IsSprinting && isGrounded)
//                {
//                    currentVelocity = pawn.RunSpeed; //sets the runVelocity variable equal to that of the protected variable _runSpeed on the playerpawn
//                }
//            }
//        FlipSprite(inputX); //flips the sprite of the character when moving left
//        #endregion
//    }
//    #region Action Input Functions

//    public virtual void JumpStart(InputAction.CallbackContext context)
//    {
//        if (context.performed)
//        {
//            if (isGrounded && !isCrouching) //only allows the player to jump if they're on the ground
//            {
//                jumpTimeCounter = jumpTime; //if we are grounded, it sets the jumpTimeCounter back to the jumpTime variable
//                isNotJumping = false; //sets the stoppedJumping bool to false so that we have !stoppedJumping
//                verticalVelocity = pawn.JumpHeight; //sets the verticalVelocity variable equal to that of the protected variable jumpHeight on PlayerPawn
//                rb2d.velocity = new Vector2(0, verticalVelocity);
//            }
//        }
//    }
//    public virtual void JumpEnd(InputAction.CallbackContext context)
//    {
//        jumpTimeCounter = 0; //resets the jumpTimeCounter to zero
//        isNotJumping = true; //sets the stoppedJumping bool to true, cause we have stopped jumping
//    }
//    public virtual void Move(InputAction.CallbackContext context)
//    {
//        if (!isCrouching)
//        {
//            inputX = context.ReadValue<Vector2>().x; //reads the value of the x input the player is using
//        }
//        //animator
//        ani.SetFloat("Speed", Mathf.Abs(inputX));//tell the animator we are moving
//    }
//    public virtual void SprintStart(InputAction.CallbackContext context)
//    {
//       //if (isGrounded)
//        //{
//            pawn.IsSprinting = true; //sets the isSprinting variable on the pawn to true
//            ani.SetBool("Sprinting", true);  //tell the animator we are sprinting
//        //}
//    }
//    public virtual void SprintEnd(InputAction.CallbackContext context)
//    {
//        //removed grounded check here to fix a bug
//        pawn.IsSprinting = false; //sets the isSprinting variable on the pawn to false
//        ani.SetBool("Sprinting", false);//tell the animator to stop sprinting
//    }

//    public virtual void CrouchStart(InputAction.CallbackContext context) 
//    {
//        if(isGrounded)
//        {
//            isCrouching = true; //sets the crouching bool to true
//            ani.SetBool("Crouched",true);
//        }
//    }

//    public virtual void CrouchEnd(InputAction.CallbackContext context)
//    {
//        if (isGrounded)
//        {
//            isCrouching = false; //sets the crouching bool to false
//            ani.SetBool("Crouched", false);
//        }
//    }

//    #region Combat Functions
//    public virtual void LightPunch(InputAction.CallbackContext context)
//    {
//        //combat.HitA();
//    }
//    public virtual void HeavyPunch(InputAction.CallbackContext context)
//    {
//        //combat.HitB();
//    }
//    public virtual void Kick(InputAction.CallbackContext context)
//    {
//        //combat.HitC();
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
//        RaycastHit2D raycastHit = Physics2D.Raycast(capsuleCollider2d.bounds.center, Vector2.down, capsuleCollider2d.bounds.extents.y + extraHeightText, whatIsGround);
//        Color rayColor;
//        if (raycastHit.collider != null)
//        {
//            rayColor = Color.green;
//        }
//        else
//        {
//            rayColor = Color.red;
//        }
//        Debug.DrawRay(capsuleCollider2d.bounds.center, Vector2.down * (capsuleCollider2d.bounds.extents.y + extraHeightText), rayColor);
//        Debug.Log(raycastHit.collider);
       
//        //Gizmos.DrawCube(groundCheck.position, boxSize); //draws a square around our ground check empty so that we can visualize it
//    }
//    #endregion
//    #endregion
//    private void OnDestroy()
//    {
        
        
//        playerInputActions.PlayerHuman.JumpStart.performed -= JumpStart; //subscribes to the JumpStart function
//        playerInputActions.PlayerHuman.JumpEnd.performed -= JumpEnd; //subscribes to the JumpEnd function
//        playerInputActions.PlayerHuman.Move.performed -= Move; // subscribes to the Move function
//        playerInputActions.PlayerHuman.SprintStart.performed -= SprintStart; //Subscribes to the SprintStart function
//        playerInputActions.PlayerHuman.SprintEnd.performed -= SprintEnd; //Subscribes to the SprintEnd function
//        playerInputActions.PlayerHuman.CrouchStart.performed -= CrouchStart; //Subscribes to the CrouchStart function
//        playerInputActions.PlayerHuman.CrouchEnd.performed -= CrouchEnd; //Subscribes to the CrouchStart function
//        playerInputActions.PlayerHuman.LightPunch.performed -= LightPunch; //subscribes to the LightPunch function
//        playerInputActions.PlayerHuman.HeavyPunch.performed -= HeavyPunch; //subscribes to the HeavyPunch function
//        playerInputActions.PlayerHuman.Kick.performed -= Kick; //subscribes to the HeavyPunch function
//        playerInputActions.PlayerHuman.Disable(); //enables the players input from the specfied input actions
//    }
//}
