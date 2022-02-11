using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    #region Variables
    #region General Pawn Attributes
    [Header("General Pawn Attributes")]
    [SerializeField, Tooltip("How fast the pawn walks")]
    protected int _walkSpeed; //this determines the walk speed of the pawn
    [SerializeField, Tooltip("How fast the pawn runs")]
    protected int _runSpeed; //this determines the run speed of the pawn
    [SerializeField, Tooltip("This determines how high the pawn can jump")]
    protected float _jumpHeight; //this determines how high the pawn will jump
    [SerializeField, Tooltip("The base damage the pawn does with each attack")]
    protected int _damage; //this will determine the base damage each pawn does with an attack
    [SerializeField, Tooltip("This is the melee attack range of the pawn")]
    protected float _meleeAttackRange; //determines the range of the pawns melee attack
    [SerializeField, Tooltip("What is the attack range of pawn")]
    protected float _rangedAttackRange; //determines the range of the pawns ranged attack
    #endregion

    #region Full Properties
     public float JumpHeight //the accessor for _jumpHeight
    {
        get { return _jumpHeight; }
        set { _jumpHeight = value; }
    }
    public int WalkSpeed //the accessor for _walkSpeed
    {
        get { return _walkSpeed; }
        set { _walkSpeed = value; }
    }
    public int RunSpeed //the accessor for _runSpeed
    {
        get { return _runSpeed;  }
        set { _runSpeed = value; }
    }
    public int Damage //the accessor for _runSpeed
    {
        get { return _damage; }
        set { _damage = value; }
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
       
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
    #endregion
}
