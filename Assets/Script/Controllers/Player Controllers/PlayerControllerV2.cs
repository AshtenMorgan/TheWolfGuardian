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
    private bool _interactRange = false;
    private float currentVelocity; //stores the player's current velocity

    #region Ground Checking
    [Header("Variables related to the ground check")]
    private CapsuleCollider2D capsuleCollider2d;
    public bool isGrounded;
    [SerializeField]
    private float belowLength = 0.1f;
    public float maxGroundAngle = 50.0f;
    #endregion

    public bool InteractRange
    {
        get { return _interactRange; }
        set { _interactRange = value; }
    }

    #endregion
    #endregion
    #region Functions
    // Start is called before the first frame update
    protected override void Awake()
    {
        //setup -- Automatically grab components from attached game object
        pawn = GetComponent<PlayerPawn>(); 
        capsuleCollider2d = GetComponent<CapsuleCollider2D>(); 
        rb2d = GetComponent<Rigidbody2D>(); 
        playerInput = GetComponent<PlayerInput>(); 
        ani = GetComponent<Animator>(); 
        combat = GetComponent<Combat>(); 

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
    
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        GroundCheck();//check ground
        //SlopeStick();//check slope
        FlipSprite(inputX); //Makes sure we are facing the direction we are moving

        ani.SetBool("Grounded", isGrounded);//match bools
        rb2d.velocity = new Vector2(inputX * currentVelocity, rb2d.velocity.y); //moves the pawn left and right based on player input and running speed

        #region Jumping Updates
        if (!isNotJumping && !isGrounded) //if we are jumping
        {
            if (jumpTimeCounter > 0) //and our jump counter hasnt reached zero
            {
                ani.SetBool("Jumping", true);//tell the animator to start jumping
                verticalVelocity = pawn.JumpHeight; //sets the verticalVelocity variable equal to that of the protected variable jumpHeight on playerpawn
                rb2d.velocity += new Vector2(0, verticalVelocity);
                jumpTimeCounter -= Time.fixedDeltaTime; // subtracts time from the jumpTimeCounter
            }
        }
        //else if (isGrounded)
        //{
        //    ani.SetBool("Jumping", false);
        //}
        //else
        //{
        //    //do nothing
        //}

        #endregion
        #region Ground Movement Updates
        if (!isCrouching)
        {
            if (!pawn.IsSprinting && isGrounded)
            {
                currentVelocity = pawn.WalkSpeed; //sets the walkVelocity variable equal to that of the protected variable _walkSpeed on the playerpawn
            }
            if (pawn.IsSprinting && isGrounded)
            {
                currentVelocity = pawn.RunSpeed; //sets the runVelocity variable equal to that of the protected variable _runSpeed on the playerpawn
            }
        }
        #endregion
    }

    //Use collision check for this instead
    protected override void SlopeStick()
    {
        if (inputX == 0)
        {
            if (isGrounded && isNotJumping)
            {
                rb2d.sharedMaterial = fullFriction; //changes Physics Material 2D of the rigidbody to our Full Friction material
            }
            else
            {
                rb2d.sharedMaterial = noFriction; //changes Physics Material 2D of the rigidbody to our No Friction material
            }
        }
        
    }
    //Tutor
    protected void GroundCheck()
    {
        //Draw a line from the center of player capsule collider straight down to the bottom of the collider
        //with an additional "belowLength"
        isGrounded = Physics2D.Raycast(capsuleCollider2d.bounds.center, Vector2.down, capsuleCollider2d.bounds.extents.y + belowLength, groundLayer);
    }
    #region Action Input Functions

    public virtual void JumpStart(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isGrounded && !isCrouching) //only allows the player to jump if they're on the ground
            {
                jumpTimeCounter = jumpTime; //if we are grounded, it sets the jumpTimeCounter back to the jumpTime variable
                isNotJumping = false; //sets the stoppedJumping bool to false so that we have !stoppedJumping
                verticalVelocity = pawn.JumpHeight; //sets the verticalVelocity variable equal to that of the protected variable jumpHeight on PlayerPawn
                rb2d.velocity = new Vector2(0, verticalVelocity);
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
    void OnCollisionEnter2D(Collision2D coll)
    {
        float angleTolerance = Mathf.Cos(maxGroundAngle * Mathf.Deg2Rad);

        foreach (ContactPoint2D contact in coll.contacts)
        {
            if (Vector3.Dot(contact.normal, Vector3.up) > angleTolerance)
            {
                Debug.Log("this collider is touching ground");
            }
        }
    }

    #region Gizmos
    private void OnDrawGizmos()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(capsuleCollider2d.bounds.center, Vector2.down, capsuleCollider2d.bounds.extents.y + belowLength, groundLayer);
        Color rayColor;
        if (raycastHit.collider != null)
        {
            rayColor = Color.green;
        }
        else
        {
            rayColor = Color.red;
        }
        Debug.DrawRay(capsuleCollider2d.bounds.center, Vector2.down * (capsuleCollider2d.bounds.extents.y + belowLength), rayColor);
        Debug.Log(raycastHit.collider);
    }
    #endregion
    #endregion
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
}
