using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    #region Variables
    #region General Pawn Attributes
    [Header("General Pawn Attributes")]
    [SerializeField, Tooltip("How fast the pawn walks")]
    protected float _walkSpeed; //this determines the walk speed of the pawn
    [SerializeField, Tooltip("Walk Speed * RunMult = sprint speed")]
    protected float _runMult; //this determines the run speed of the pawn
    [SerializeField, Tooltip("This determines how high the pawn can jump")]
    protected float _jumpHeight; //this determines how high the pawn will jump
    [SerializeField, Tooltip("The base damage the pawn does with each HITA")]
    protected int _damageA; //this will determine the base damage each pawn does with an attack
    [SerializeField, Tooltip("The base damage the pawn does with each HITB")]
    protected int _damageB; //this will determine the base damage each pawn does with an attack
    [SerializeField, Tooltip("The base damage the pawn does with each HITC")]
    protected int _damageC; //this will determine the base damage each pawn does with an attack
    [SerializeField, Tooltip("This is the melee attack range of the pawn")]
    protected float _meleeAttackRange; //determines the range of the pawns melee attack
    [SerializeField, Tooltip("What is the attack range of pawn")]
    protected float _rangedAttackRange; //determines the range of the pawns ranged attack

    private Transform lastSavedPosition;
    #endregion

    #region Full Properties
    public float JumpHeight //the accessor for _jumpHeight
    {
        get { return _jumpHeight; }
        set { _jumpHeight = value; }
    }
    public float WalkSpeed //the accessor for _walkSpeed
    {
        get { return _walkSpeed; }
        set { _walkSpeed = value; }
    }
    public float RunMult //the accessor for _runSpeed
    {
        get { return _runMult;  }
        set { _runMult = value; }
    }
    public int DamageA 
    {
        get { return _damageA; }
        set { _damageA = value; }
    }
    public int DamageB 
    {
        get { return _damageB; }
        set { _damageB = value; }
    }
    public int DamageC 
    {
        get { return _damageC; }
        set { _damageC = value; }
    }
    #endregion

    #region Pawn Components
    [Header("Pawn Components"), SerializeField, Tooltip("The pawn's current location/rotation")]
    protected Transform t; //stores the transform of the current pawn
    [SerializeField, Tooltip("This is the Pawns rigidbody component")]
    protected Rigidbody2D rb; //stores the rigidbody component of the pawn
    protected Animator ani; //stores the animator for the pawn
    #endregion
    #endregion


    #region Functions
    void Awake()
    {
    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
       ani = GetComponent<Animator>();
        t = gameObject.transform;
        rb= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    protected virtual void Update()
    {

    }

    #endregion
}
