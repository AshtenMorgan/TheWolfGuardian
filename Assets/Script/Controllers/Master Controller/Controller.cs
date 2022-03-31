using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Controller : MonoBehaviour
{
    #region Variables
    #region General Variables
    [Header("General Variables")]
    [SerializeField]
    protected Rigidbody2D rb2d; //the rigidbody of the test character
    
    protected Combat combat; //stores the combat script for the pawn
    [SerializeField]
    protected LayerMask groundLayer; //The layer mask for what is considered "the ground" in the game
    InputRecorder inputRecorder; //assigns the input recorder script so the controller can properly do combos
    int CurrentComboPriority = 0;
    #endregion
    #region Jump Variables
    [Header("Jump Variables")]
    protected float verticalVelocity; //the vertical acceleration of the player pawn
    protected bool isGrounded; //determines if the pawn is touching the ground or not
    protected bool isCrouching; //determines if the pawn is crouching
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
    [SerializeField]
    protected PhysicsMaterial2D noFriction; //stores the friction material for normal movement
    [SerializeField]
    protected PhysicsMaterial2D fullFriction; //stores the friction matieral for idle movement
    protected int walkVelocity; //the stand in variable for walk speed on this script
    protected int runVelocity; //the stand in variable for run speedon this script
    protected float inputX; //stores the players x axis input
    protected bool facingRight = true; //a bool that signifies whether the character is facing right, initializes as true
    #endregion
    #region Animator Variables
    [Header("Animator Variables")]
    public Animator ani;//animator code
    #endregion
    #region Full Properties
    public bool IsGrounded
    {
        get { return isGrounded; }
    }
    public bool IsCrouching
    {
        get { return isCrouching; }
    }

    #endregion
    #endregion
    #region Functions
    protected virtual void Awake()
    {
        
        rb2d = GetComponent<Rigidbody2D>(); //defines the Rigidbody needed for pawn physics
        combat = GetComponent<Combat>();
        if (ani == null)
            ani = GetComponent<Animator>();
        if (inputRecorder == null)
            inputRecorder = FindObjectOfType<InputRecorder>();
        jumpTimeCounter = jumpTime; //sets the jumpTimeCounter
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, circleRadius, groundLayer); //this update checks to see if the pawn is grounded
    }
    protected virtual void FixedUpdate()
    {

    }
    protected void SlopeStick()
    {
        if (isGrounded && inputX == 0)
        {
            rb2d.sharedMaterial = fullFriction; //changes Physics Material 2D of the rigidbody to our Full Friction material
        }
        else
        {
            rb2d.sharedMaterial = noFriction; //changes Physics Material 2D of the rigidbody to our No Friction material
        }
    }
    void ResetTriggers() //Reset All the Animation Triggers so we don't have overlapping animations
    {
        foreach (AnimatorControllerParameter parameter in ani.parameters)
        {
            ani.ResetTrigger(parameter.name);
        }
    }
    public void PlayMove(Moves move, int ComboPriority) //Get the Move and the Priorty
    {
        if (Moves.None != move) //if the move is none ignore the function
        {
            if (ComboPriority >= CurrentComboPriority) //if the new move is higher Priorty play it and ignore everything else
            {
                CurrentComboPriority = ComboPriority; //Set the new Combo
                ResetTriggers(); //Reset All Animation Triggers
                inputRecorder.ResetCombo(); //Reset the List in the ControlsManager
            }
            else
                return;

            //Set the Animation Triggers
            switch (move)
            {
                case Moves.HitA0:
                    ani.SetTrigger("HitA0");
                    break;
                case Moves.HitA1:
                    ani.SetTrigger("HitA1");
                    break;
                case Moves.HitA2:
                    ani.SetTrigger("HitA2");
                    break;
                case Moves.HitA3:
                    ani.SetTrigger("HitA3");
                    break;
                case Moves.HitB0:
                    ani.SetTrigger("HitB0");
                    break;
                case Moves.HitB1:
                    ani.SetTrigger("HitB1");
                    break;
                case Moves.HitB2:
                    ani.SetTrigger("HitB2");
                    break;
                case Moves.HitB3:
                    ani.SetTrigger("HitB3");
                    break;
                case Moves.HitC0:
                    ani.SetTrigger("HitC0");
                    break;
                case Moves.HitC1:
                    ani.SetTrigger("HitC1");
                    break;
                case Moves.HitC2:
                    ani.SetTrigger("HitC2");
                    break;
                case Moves.HitC3:
                    ani.SetTrigger("HitC3");
                    break;
            }

            CurrentComboPriority = 0; //Reset the Combo Priorty
        }
    }
    #endregion
}
