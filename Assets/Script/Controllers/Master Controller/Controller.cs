using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    #region Variables
    #region General Variables
    [Header("General Variables")]
    protected Rigidbody2D rb2d; //the rigidbody of the test character
    protected PlayerPawn pawn; //the PlayerPawn class so that we can access the player characters movement data
    [SerializeField]
    protected LayerMask groundLayer; //The layer mask for what is considered "the ground" in the game
    #endregion
    #region Jump Variables
    [Header("Jump Variables")]
    protected float verticalVelocity; //the vertical acceleration of the player pawn
    protected bool isGrounded; //determines if the pawn is touching the ground or not
    [SerializeField]
    protected float jumpTime; //the time we set in the editor for the maximum amount of time we can jump into the air before we start falling
    protected float jumpTimeCounter; //the counter that keeps track of jumpTime
    protected bool isNotJumping = true; //decides whether the player has stopped jumping
    [SerializeField]
    protected Transform groundCheck; //the specified location that decides whether the pawn is touching the ground or not
    [SerializeField]
    protected float circleRadius; //determines the size of the capsule at the feet of the pawn
    #endregion
    #region Lateral Movement Variables
    [Header("Lateral Movement Variables")]
    protected Vector2 newVelocity;//the horizontal velocity of the pawn
    protected int walkVelocity; //the stand in variable for walk speed on this script
    protected int runVelocity; //the stand in variable for run speedon this script
    protected float inputX; //stores the players x axis input
    protected bool isFacingRight = true; //a bool that signifies whether the character is facing right, initializes as true
    protected CapsuleCollider2D slopeCollider; //stores the collider centered around the pawns feet
    protected Vector2 sColliderSize; //stores the size of the collider centered around the pawns feet
    [SerializeField]
    private float slopeCheckDistance; //the distance of the raycast for slope checking
    protected float slopeDownAngle; //the downward angle of our slope check
    protected float slopeSideAngle;
    protected float slopeDownAngleOld;//stores the old downward angle of our slope check
    protected Vector2 slopeNormalPerp; //the angle perpindecular to our slopeDownAngle
    protected bool isOnSlope; //defines whether the pawn is on a slope or not
    [SerializeField]
    protected PhysicsMaterial2D noFriction; //stores the material when not on slope
    [SerializeField]
    protected PhysicsMaterial2D fullFriction; //stores material when on slope
    #region Animator Variables
    [Header("Animator Variables")]
    public Animator ani;//animator code
    #endregion
    #endregion
    #endregion
    #region Functions
    protected virtual void Awake()
    {
        pawn = GetComponent<PlayerPawn>(); //defines the pawn needed for all stats
        rb2d = GetComponent<Rigidbody2D>(); //defines the Rigidbody needed for pawn physics
        slopeCollider = GetComponent<CapsuleCollider2D>(); //stores the collider centered around the pawns feet
        sColliderSize = slopeCollider.size; //stores the size of the collider centered around the pawns feet
        jumpTimeCounter = jumpTime; //sets the jumpTimeCounter
    }

    protected virtual void Start() 
    {
    
    }

    protected virtual void Update()
    {
        
    }
    protected virtual void FixedUpdate() 
    {
        
    }

    #region Slope Handling Functions
    protected void SlopeCheck()
    {
        if (inputX == 0)
        {
            rb2d.sharedMaterial = fullFriction; //changes Physics Material 2D of the rigidbody to our Full Friction material
        }
        else
        {
            rb2d.sharedMaterial = noFriction; //changes Physics Material 2D of the rigidbody to our No Friction material
        }
    }

    #endregion
    #endregion
}
