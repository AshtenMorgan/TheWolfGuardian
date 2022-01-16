using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    #region Variables
    #region General Pawn Attributes
    [Header("General Pawn Attributes"), SerializeField, Tooltip("How much health the pawn currently has")]
    protected float currentHealth; //this is the current health of the pawn
    [SerializeField, Tooltip("Maximum health of the pawn")]
    protected float maxHealth; //this is the max health of the pawn
    [SerializeField, Tooltip("How fast the pawn walks")]
    protected float walkSpeed; //this determines the walk speed of the pawn
    [SerializeField, Tooltip("How fast the pawn runs")]
    protected float runSpeed; //this determines the run speed of the pawn
    [SerializeField, Tooltip("This determines how high the pawn can jump")]
    protected float jumpHeight; //this determines how high the pawn will jump
    [SerializeField, Tooltip("The base damage the pawn does with each attack")]
    protected float damage; //this will determine the base damage each pawn does with an attack
    [SerializeField, Tooltip("This is the melee attack range of the pawn")]
    protected float meleeAttackRange; //determines the range of the pawns melee attack
    [SerializeField, Tooltip("What is the attack range of pawn")]
    protected float rangedAttackRange; //determines the range of the pawns ranged attack
    #endregion

    #region Pawn Components
    [Header("Pawn Components"), SerializeField, Tooltip("The pawn's current location/rotation")]
    protected Transform t; //stores the transform of the current pawn
    [SerializeField, Tooltip("This is the Pawns rigidbody component")]
    protected Rigidbody rb; //stores the rigidbody component of the pawn
    #endregion
    #endregion


    #region Functions
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
