using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Combat : MonoBehaviour
{ //temporarily disabling class until new mechanics are put in place
    /*
    #region Variables
    #region General Variables
    [Header("General Pawn Attributes")]
    protected bool _canAttack; //when this says hit you say how hard
    protected Pawn pawn; //stores the pawn of our combatant
    protected Animator ani; //stores the animator of the combatant
    protected Controller controller; //stores 
    protected bool isGrounded; //saves the grounded status of the pawn from the controller
    protected bool isCrouching; //determines if the pawn is crouching
    [SerializeField]
    protected LayerMask enemyLayer; //the layer masks that all enemies are on
    [SerializeField]
    protected float damageA,
        damageB,
        damageC; //the damage our combatant does
    #endregion
    #region Timers
    [SerializeField]
    protected float animTimer; //Tracks the amount of time before the animation of an attack resets
    protected float animCounter; //actually does the counting for animTimer
    #endregion
    #region Colliders
    [Header("Hitbox A Attributes")]
    [Header("Hitbox Attributes")]
    #region Hitbox A
    [SerializeField, Tooltip("the position of Hit A's hitbox.")]
    protected Transform hitAPos; //the position of hit A's hitbox
    [SerializeField, Tooltip("the length and width of Hit A's hitbox.")]
    protected Vector3 hitAVector; //the size of hit A's hitbox
    [SerializeField, Tooltip("the position of Hit A's hitbox in the air.")]
    protected Transform hitAJumpPos; //the position of hit A's hitbox in the air
    [SerializeField, Tooltip("the length and width of Hit A's hitbox in the air.")]
    protected Vector3 hitAJumpVector; //the size of hit A's hitbox in the air
    [SerializeField, Tooltip("the position of Hit A's hitbox while crouched.")]
    protected Transform hitACrouchPos; //the position of hit A's hitbox while crouched
    [SerializeField, Tooltip("the length and width of Hit A's hitbox while crouched.")]
    protected Vector3 hitACrouchVector; //the size of hit A's hitbox while crouched
    [Header("Hitbox B Attributes")]
    [SerializeField, Tooltip("the position of Hit B's hitbox.")]
    protected Transform hitBPos; //the position of hit B's hitbox
    [SerializeField, Tooltip("the length and width of Hit B's hitbox.")]
    protected Vector3 hitBVector; //The length and width of Hit B's hitbox
    [Header("Hitbox C Attributes")]
    [SerializeField, Tooltip("the position of Hit C's hitbox.")]
    protected Transform hitCPos; //the position of hit C's hitbox
    [SerializeField, Tooltip("the length and width of Hit C's hitbox.")]
    protected Vector3 hitCVector; //The length and width of Hit C's hitbox
    #endregion
    #endregion
    #endregion
    #region Functions
    #region Startup Functions
    protected virtual void Awake()
    {
        pawn = GetComponent<Pawn>(); //defines the pawn of the combatant
        ani = GetComponent<Animator>(); //defines the animator for the Combatant
        controller = GetComponent<Controller>();//defines the controller of the combatant
        animCounter = 0f;
        damageA = pawn.DamageA;
        damageB = pawn.DamageB;
        damageC = pawn.DamageC;
    }
    protected virtual void Update() 
    {
        isGrounded = controller.IsGrounded;
        isCrouching = controller.IsCrouching;

        if (animCounter > 0) 
        {
            animCounter -= Time.time; //decrements animCounter
        }
        else if (animCounter <= 0)
        {
            ani.SetBool("HitA", false);
            ani.SetBool("HitB", false);
            ani.SetBool("HitC", false);
        }
        
    }
    #endregion
    
    #region Combat Tools
    protected virtual void OnAttackEnable() 
    {
        _canAttack = true; //gives us permission to hit the things
    }
    /*
    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        var hit = other.GetComponent<Health>();
        if (hit != null && _canAttack) 
        {
            hit.Damage(damage);
            _canAttack = false;
        }
    }
    
    #endregion
    #region Combo Attack Functions
    #region Hit A Function
    public virtual void HitA() 
    {
        if (isGrounded)
        {
            ani.SetBool("HitA", true);
            animCounter = animTimer;
            //create a circle and return all the colliders within the area into an array
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(hitAPos.position, hitAVector, 0, enemyLayer);
            //for every collider in that array
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<Health>().Damage(damageA);
                Debug.Log("Hit Enemy: " + enemiesToDamage[i].name);
                _canAttack = false;
            }
        }
        if (!isGrounded)
        {
            ani.SetBool("HitA", true);
            animCounter = animTimer;
            //create a circle and return all the colliders within the area into an array
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(hitAJumpPos.position, hitAJumpVector, 0, enemyLayer);
            //for every collider in that array
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<Health>().Damage(damageA);
                Debug.Log("Hit Enemy: " + enemiesToDamage[i].name);
                _canAttack = false;
            }
        }
        if (isGrounded && isCrouching)
        {
            ani.SetBool("HitA", true);
            animCounter = animTimer;
            //create a circle and return all the colliders within the area into an array
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(hitACrouchPos.position, hitACrouchVector, 0, enemyLayer);//this fixes your layer problem
            //for every collider in that array
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<Health>().Damage(damageA);
                Debug.Log("Hit Enemy: " + enemiesToDamage[i].name);
                _canAttack = false;
            }
        }
    }
    #endregion
    #region Hit B Function
    public virtual void HitB()
    {
        if (isGrounded)
        {
            ani.SetBool("HitB", true);
            animCounter = animTimer;
            //create a circle and return all the colliders within the area into an array
            //enemy layer check is a good idea, but what about when we want to hit breakable walls?
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(hitBPos.position, hitBVector, 0, enemyLayer);//<-- This is where your problem is, it is registering enemyLayer as an angle
            //for every collider in that array
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                if  (enemiesToDamage[i].CompareTag("Enemy"))//make sure this is tagged as enemy
                {
                    enemiesToDamage[i].GetComponent<Health>().Damage(damageB);
                    Debug.Log("Hit Enemy: " + enemiesToDamage[i].name);
                    _canAttack = false;
                }
                
            }
        }
    }
    #endregion
    #region Hit B Function
    public virtual void HitC()
    {
        if (isGrounded)
        {
            ani.SetBool("HitC", true);
            animCounter = animTimer;
            //create a circle and return all the colliders within the area into an array
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(hitCPos.position, hitCVector, 0, enemyLayer);
            //for every collider in that array
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<Health>().Damage(damageC);
                Debug.Log("Hit Enemy: " + enemiesToDamage[i].name);
                _canAttack = false;
            }
        }
    }
    #endregion
    #endregion
    #region Gizmos
    /// <summary>
    /// Gizmo for visually displaying the attack range
    /// </summary>
    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; //makes Gizmo for Hitboxes red
        #region Hitbox A Gizmos
        //Gizmos.DrawWireCube(hitAPos.position, hitAVector); //displays the size and shape of hitbox A
        //Gizmos.DrawWireCube(hitAJumpPos.position, hitAJumpVector); //displays the size and shape of hitbox A in the air
        //Gizmos.DrawWireCube(hitACrouchPos.position, hitACrouchVector); //displays the size and shape of hitbox A while crouching
        #endregion
        #region#region Hitbox B Gizmos
        //Gizmos.DrawWireCube(hitBPos.position, hitBVector);// displays the size and shape of Hitbox B
        #endregion
        #region#region Hitbox C Gizmos
        //Gizmos.DrawWireCube(hitCPos.position, hitCVector);// displays the size and shape of Hitbox B
        #endregion
    }
    #endregion
    #endregion
    */
}

