using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [SerializeField, Tooltip("How much health current pawn has")]
    protected float currentHealth;

    [SerializeField, Tooltip("Maximum health of current pawn")]
    protected float maxHealth;

    [SerializeField, Tooltip("How fast does pawn walk")]
    protected float walkSpeed;

    [SerializeField, Tooltip("How fast does pawn run")]
    protected float runSpeed;

    [SerializeField, Tooltip("What is this pawns jump height")]
    protected float jumpHeight;

    [SerializeField, Tooltip("How much damage does this pawn do")]
    protected float damage;

    [SerializeField, Tooltip("What is the attack range of pawn")]
    protected float attackRange;

    [SerializeField, Tooltip("What is the pawn's location/rotation")]
    protected Transform t;

    [SerializeField, Tooltip("Pawns rigidbody")]
    protected Rigidbody rb;
    



    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
