using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : MonoBehaviour
{
    [SerializeField, Tooltip("How much health current pawn has")]
    private float currentHealth;

    [SerializeField, Tooltip("Maximum health of current pawn")]
    private float maxHealth;

    [SerializeField, Tooltip("How fast does pawn walk")]
    private float walkSpeed;

    [SerializeField, Tooltip("How fast does pawn run")]
    private float runSpeed;

    [SerializeField, Tooltip("What is this pawns jump height")]
    private float jumpHeight;

    [SerializeField, Tooltip("How much damage does this pawn do")]
    private float damage;

    [SerializeField, Tooltip("What is the attack range of pawn")]
    private float attackRange;

    [SerializeField, Tooltip("What is the pawn's location/rotation")]
    private Transform t;

    [SerializeField, Tooltip("Pawns rigidbody")]
    private Rigidbody rb;
    



    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }
}
