using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerV2 : Controller
{
    #region Variables
    #region General Player Variables
    [Header("General Player Variables")]
    protected PlayerInput playerInput; //defines the input that the pawn is utlizing 
    protected PlayerInputActions playerInputActions; //variable for storing the input schemes the pawn will be using
    protected PlayerPawn pawn;//variable for storing the pawn
    [SerializeField] protected Rigidbody2D rb2d; //the rigidbody of the test character
    private bool isAttacking;

    private bool _interactRange = false;


    public bool InteractRange
    {
        get { return _interactRange; }
        set { _interactRange = value; }
    }

    #endregion

    #region Lateral Movement Variables
    [Header("Lateral Movement Variables")]
    [SerializeField]
    protected PhysicsMaterial2D noFriction; //stores the friction material for normal movement
    [SerializeField]
    protected PhysicsMaterial2D fullFriction; //stores the friction matieral for idle movement
    protected Vector2 currentVelocity; //stores the player's current velocity
    protected Vector2 newVelocity; //walk velocity
    protected Vector2 newForce; //Jump force
    protected float inputX; //stores the players x axis input
    protected bool facingRight = true; //a bool that signifies whether the character is facing right, initializes as true
    [SerializeField] protected float walkSpeed;//speed from pawn class
    [SerializeField] protected float runMult;//the speed multiplyer for sprinting (from pawn class)
    [SerializeField] protected float jumpForce;//jump height from pawn class
    #endregion

    #region Jumping Variables
    [SerializeField]
    protected float jumpTime = 0.09f; //the time we set in the editor for the maximum amount of time we can jump into the air before we start falling
    protected float jumpTimeCounter; //the counter that keeps track of jumpTime
    [SerializeField]protected bool isJumping = false; //decides whether the player has stopped jumping
    #endregion

    #region slope stuff
    [SerializeField] protected float slopeCheckDistance;
    protected float slopeDownAngle;
    protected float slopeDownAngleOld;
    protected float slopeSideAngle;
    protected Vector2 slopeNormalPerp;
    [SerializeField] protected bool isOnSlope;

    #endregion

    #region Ground Checking
    [Header("Variables related to the ground check")]
    [SerializeField] protected bool isGrounded;
    public LayerMask whatIsGround; //The layer mask for what is considered "the ground" in the game
    public CapsuleCollider2D capsuleCollider2d;
    [SerializeField] protected float belowCheck = 0.2f;
    [SerializeField] private int colCount = 0;

    [SerializeField]
    protected float maxGroundAngle = 50.0f;
    protected float angleTolerance;
    public Vector2 colSize;
    #endregion
    #endregion

    #region Functions
    #region Setup
    protected override void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        combat = GetComponent<Combat>();
        pawn = GetComponent<PlayerPawn>();
        ani = GetComponent<Animator>();
        capsuleCollider2d = GetComponent<CapsuleCollider2D>();
        rb2d = GetComponent<Rigidbody2D>(); //defines the Rigidbody needed for pawn physics
        colSize = capsuleCollider2d.size;

        walkSpeed = pawn.WalkSpeed;
        runMult = pawn.RunMult;
        jumpForce = pawn.JumpHeight;


        jumpTimeCounter = jumpTime; //Defaults the jump timer



        #region Player Action Subscriptions
        playerInputActions = new PlayerInputActions(); //initializes te players inputs
        playerInputActions.PlayerHuman.Enable(); //enables the players input from the specfied input actions

        //subscriptions
        playerInputActions.PlayerHuman.JumpStart.performed += JumpStart;
        playerInputActions.PlayerHuman.JumpEnd.performed += JumpEnd;
        playerInputActions.PlayerHuman.Move.performed += Move;
        playerInputActions.PlayerHuman.SprintStart.performed += SprintStart;
        playerInputActions.PlayerHuman.SprintEnd.performed += SprintEnd;
        playerInputActions.PlayerHuman.CrouchStart.performed += CrouchStart;
        playerInputActions.PlayerHuman.CrouchEnd.performed += CrouchEnd;
        playerInputActions.PlayerHuman.LightPunch.performed += LightPunch;
        playerInputActions.PlayerHuman.HeavyPunch.performed += HeavyPunch;
        playerInputActions.PlayerHuman.Kick.performed += Kick;
        #endregion


    }

    protected override void Start()
    {
        base.Start();
    }
    #endregion

    #region Update
    protected override void Update()
    {
        base.Update();
    }
    protected override void FixedUpdate()
    {
        ApplyMovement();

        isGrounded = GroundCheck();

        if (colCount == 0)//not colliding with anything
            GroundCheck();//look for something beneath us

        SlopeCheck();

        FlipSprite(inputX); //Makes sure we are facing the direction we are moving

        ani.SetBool("Grounded", isGrounded);//match bools

        if (isJumping && !isGrounded) //if we are jumping
        {
            if (jumpTimeCounter > 0) //and our jump counter hasnt reached zero
            {
                ani.SetBool("Jumping", true);//tell the animator to start jumping
                isJumping = true;
                rb2d.velocity += newForce;
                jumpTimeCounter -= Time.fixedDeltaTime; // subtracts time from the jumpTimeCounter
            }
        }
        else if (isGrounded)
        {
            ani.SetBool("Jumping", false);
            isJumping = false;
        }
        else
        {
            //do nothing
        }

        base.FixedUpdate();
    }
    #endregion

    #region Movement
    protected override void ApplyMovement()
    {
        if (!isAttacking)
        {
            if (!isCrouching)
            {
                if (!isOnSlope)
                {
                    rb2d.gravityScale = 5;
                    if (!pawn.IsSprinting)
                    {
                        if (isGrounded)
                        {
                            newVelocity.Set(walkSpeed * inputX, rb2d.velocity.y);
                            rb2d.velocity = newVelocity; ; //sets the walkVelocity variable equal to that of the protected variable _walkSpeed on the playerpawn
                        }
                        else
                        {
                            newVelocity.Set(walkSpeed * inputX, rb2d.velocity.y);
                            rb2d.velocity = newVelocity; ; //sets the walkVelocity variable equal to that of the protected variable _walkSpeed on the playerpawn
                        }
                    }
                    if (pawn.IsSprinting)
                    {
                        if (isGrounded)
                        {
                            newVelocity.Set(walkSpeed * runMult * inputX, rb2d.velocity.y);
                            rb2d.velocity = newVelocity; ; //sets the walkVelocity variable equal to that of the protected variable _walkSpeed on the playerpawn
                        }
                        else
                        {
                            newVelocity.Set(walkSpeed * runMult * inputX, rb2d.velocity.y);
                            rb2d.velocity = newVelocity; ; //sets the walkVelocity variable equal to that of the protected variable _walkSpeed on the playerpawn
                        }
                    }
                }
                else
                {
                    rb2d.gravityScale = 10;
                    if (isJumping)
                    {
                        rb2d.gravityScale = 5;
                        newVelocity.Set(rb2d.velocity.x, rb2d.velocity.y);
                    }
                    else if (!pawn.IsSprinting)
                    {
                        newVelocity.Set(walkSpeed * slopeNormalPerp.x * -inputX, walkSpeed * slopeNormalPerp.y * -inputX);
                        rb2d.velocity = newVelocity;
                    }
                    else
                    {
                        newVelocity.Set(walkSpeed * runMult * slopeNormalPerp.x * -inputX, walkSpeed * runMult * slopeNormalPerp.y * -inputX);
                        rb2d.velocity = newVelocity;
                    }

                }
            }
        }
    }
    #endregion

    #region SlopeCheck
    protected override void SlopeCheck()
    {
        Vector2 checkPos = transform.position - new Vector3(0.0f, colSize.y / 2);
        SlopeVertical(checkPos);
        SlopHorizontal(checkPos);

        base.SlopeCheck();
    }
    protected void SlopHorizontal(Vector2 checkPos)
    {
        RaycastHit2D slopeHitFront = Physics2D.Raycast(checkPos, transform.right, slopeCheckDistance, whatIsGround);
        RaycastHit2D slopeHitBack = Physics2D.Raycast(checkPos, -transform.right, slopeCheckDistance, whatIsGround);

        if (slopeHitFront)
        {
            slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
            if (slopeSideAngle > 89)
            {
                isOnSlope = false;
            }
            else
            {
                isOnSlope = true;
            }
            
           
        }
        else if (slopeHitBack)
        {
            slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);

            if (slopeSideAngle > 89)
            {
                isOnSlope = false;
            }
            else
            {
                isOnSlope = true;
            }
            
        }
        else
        {
            slopeSideAngle = 0.0f;
            isOnSlope = false;
        }
    }
    protected void SlopeVertical(Vector2 checkPos)
    {
        RaycastHit2D hit = Physics2D.Raycast(checkPos, Vector2.down, slopeCheckDistance, whatIsGround);
        if (hit)
        {
            slopeNormalPerp = Vector2.Perpendicular(hit.normal).normalized;
            slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if ((slopeDownAngle <= angleTolerance)&&(slopeDownAngle != 0))
            {
                Debug.Log(slopeDownAngle);
                if (slopeDownAngle != slopeDownAngleOld)
                {
                    isOnSlope = true;
                }
            }

            slopeDownAngleOld = slopeDownAngle;


            //debug
            Debug.DrawRay(hit.point, hit.normal, Color.green);
            Debug.DrawRay(hit.point, slopeNormalPerp, Color.red);
        }
        if (isOnSlope && inputX == 0)
        {
            rb2d.sharedMaterial = fullFriction;
            capsuleCollider2d.sharedMaterial = fullFriction;
        }
        else
        {
            rb2d.sharedMaterial = noFriction;
            capsuleCollider2d.sharedMaterial = noFriction;
        }
    }
    #endregion

    #region Action Input Functions
    public virtual void JumpStart(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isGrounded && !isCrouching) //only allows the player to jump if they're on the ground
            {
                jumpTimeCounter = jumpTime; //if we are grounded, it sets the jumpTimeCounter back to the jumpTime variable
                currentVelocity = newVelocity;
                isJumping = true;
                newForce.Set(0.0f, pawn.JumpHeight);
                newVelocity.Set(currentVelocity.x, jumpForce);
                rb2d.velocity = newVelocity;
            }
        }
    }
    public virtual void JumpEnd(InputAction.CallbackContext context)
    {
        jumpTimeCounter = 0; //resets the jumpTimeCounter to zero
        isJumping = false;
    }
    public virtual void Move(InputAction.CallbackContext context)
    {
        if (!isCrouching)
        {
            inputX = context.ReadValue<Vector2>().x; //reads the value of the x input the player is using
        }
        //animator
        ani.SetFloat("Speed", Mathf.Abs(inputX));//tell the animator we are moving
    }
    public virtual void SprintStart(InputAction.CallbackContext context)
    {
        pawn.IsSprinting = true; //sets the isSprinting variable on the pawn to true
        ani.SetBool("Sprinting", true);  //tell the animator we are sprinting
    }
    public virtual void SprintEnd(InputAction.CallbackContext context)
    {
        pawn.IsSprinting = false; //sets the isSprinting variable on the pawn to false
        ani.SetBool("Sprinting", false);//tell the animator to stop sprinting
    }
    public virtual void CrouchStart(InputAction.CallbackContext context)
    {
        isCrouching = isGrounded; //sets the crouching bool to true if we are grounded
        ani.SetBool("Crouched", isGrounded);
    }
    public virtual void CrouchEnd(InputAction.CallbackContext context)
    {
        isCrouching = false; //sets the crouching bool to false
        ani.SetBool("Crouched", false);
    }

    #region Combat Functions
    public virtual void LightPunch(InputAction.CallbackContext context)
    {
        //combat.HitA();
    }
    public virtual void HeavyPunch(InputAction.CallbackContext context)
    {
        //combat.HitB();
    }
    public virtual void Kick(InputAction.CallbackContext context)
    {
        //combat.HitC();
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

    #region Ground Check
    protected bool GroundCheck()
    {
        //Draw a line from the center of player capsule collider straight down to the bottom of the collider
        //with an additional "belowLength"
        //**********FIXED*********//
        if (isJumping)
        {
            isOnSlope = false;
            return false;
        }
            
        else
            return Physics2D.Raycast(capsuleCollider2d.bounds.center, Vector2.down, capsuleCollider2d.bounds.extents.y + belowCheck, whatIsGround);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        colCount++;
    }
    void OnCollisionStay2D(Collision2D coll)
    {
        angleTolerance = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);

        foreach (ContactPoint2D contact in coll.contacts)
        {
            if (Vector3.Dot(contact.normal, Vector3.up) < angleTolerance)
            {
                isGrounded = true;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        colCount--;
    }
    #endregion

    #region StopMovement
    private void StopMovement()
    {
        isAttacking = true;
        newVelocity.Set(0, 0);
        rb2d.velocity.Set(newVelocity.x, newVelocity.y);
    }

    private void ResumeMovement()
    {
        isAttacking = false;
    }
    #endregion

    #region Cleanup
    private void OnDestroy()
    {

        //Unsubscribe
        playerInputActions.PlayerHuman.JumpStart.performed -= JumpStart;
        playerInputActions.PlayerHuman.JumpEnd.performed -= JumpEnd;
        playerInputActions.PlayerHuman.Move.performed -= Move;
        playerInputActions.PlayerHuman.SprintStart.performed -= SprintStart;
        playerInputActions.PlayerHuman.SprintEnd.performed -= SprintEnd;
        playerInputActions.PlayerHuman.CrouchStart.performed -= CrouchStart;
        playerInputActions.PlayerHuman.CrouchEnd.performed -= CrouchEnd;
        playerInputActions.PlayerHuman.LightPunch.performed -= LightPunch;
        playerInputActions.PlayerHuman.HeavyPunch.performed -= HeavyPunch;
        playerInputActions.PlayerHuman.Kick.performed -= Kick;

        //Disable Input
        playerInputActions.PlayerHuman.Disable();
    }
    #endregion
    #region Gizmos
    private void OnDrawGizmos()
    {
        //RaycastHit2D raycastHit = Physics2D.Raycast(capsuleCollider2d.bounds.center, Vector2.down, capsuleCollider2d.bounds.extents.y + belowLength, whatIsGround);
        //Color rayColor;
        //if (raycastHit.collider != null)
        //{
        //    rayColor = Color.green;
        //}
        //else
        //{
        //    rayColor = Color.red;
        //}
        //Debug.DrawRay(capsuleCollider2d.bounds.center, Vector2.down * (capsuleCollider2d.bounds.extents.y + belowLength), rayColor);
        //Debug.Log(raycastHit.collider);
    }
    #endregion
    #endregion
}
