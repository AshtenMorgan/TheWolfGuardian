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
    protected Vector2 boxSize; //the size of the square for the groundCheck detection box
 
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
    #region Combat Variables
    [SerializeField]InputRecorder inputRecorder; //assigns the input recorder script so the controller can properly do combos
    MovesManager movesManager;
    int CurrentComboPriority = 0;
    int ComboPriority;
    [SerializeField]
    Moveset move; //stores the Moveset enum
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
    #endregion
    #region Functions
    protected virtual void Awake()
    {
        if (inputRecorder == null)
            inputRecorder = FindObjectOfType<InputRecorder>();
        if (movesManager == null)
            movesManager = FindObjectOfType<MovesManager>();
        rb2d = GetComponent<Rigidbody2D>(); //defines the Rigidbody needed for pawn physics
        combat = GetComponent<Combat>();
        if (ani == null)
            ani = GetComponent<Animator>();
        jumpTimeCounter = jumpTime; //sets the jumpTimeCounter
        
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, boxSize, 0, groundLayer); //this update checks to see if the pawn is grounded
        PlayMove(move, ComboPriority);
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
    #region Combat Functions
    public void PlayMove(Moveset move, int ComboPriority) //Get the Move and the Priorty
    {
        if (Moveset.None != move) //if the move is none ignore the function
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
                case Moveset.HitAS0:
                        Debug.Log("Playing Move HitAS0!");
                        ani.SetTrigger("HitAS0");
                    break;
                case Moveset.HitAS1:
                    Debug.Log("Playing Move HitAS1!");
                    ani.SetTrigger("HitAS1");
                    break;
                case Moveset.HitAS2:
                    Debug.Log("Playing Move HitAS2!");
                    ani.SetTrigger("HitAS2");
                    break;
                case Moveset.HitAS3:
                    Debug.Log("Playing Move HitAS3!");
                    ani.SetTrigger("HitAS3");
                    break;
                case Moveset.HitAA0:
                        Debug.Log("Playing Move HitAA0!");
                        ani.SetTrigger("HitAA0");
                    break;
                case Moveset.HitAC0:
                        Debug.Log("Playing Move HitAC0!");
                        ani.SetTrigger("HitAC0");
                    break;
                case Moveset.HitCS0:
                    Debug.Log("Playing Move HitCS0!");
                    ani.SetTrigger("HitCS0");
                    break;
            }

            CurrentComboPriority = 0; //Reset the Combo Priorty
        }
    }
    void ResetTriggers() //Reset All the Animation Triggers so we don't have overlapping animations
    {
        foreach (AnimatorControllerParameter parameter in ani.parameters)
        {
            ani.ResetTrigger(parameter.name);
        }
    }
    #endregion
    #endregion
    
}
