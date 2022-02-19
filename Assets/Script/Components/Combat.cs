using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Combat : MonoBehaviour
{
    #region Variables
    #region General Variables
    [Header("General Pawn Attributes")]
    protected bool _canAttack; //when this says hit you say how hard
    protected Pawn pawn; //stores the pawn of our combatant
    protected Animator ani; //stores the animator of the combatant
    protected Controller controller; //stores 
    protected bool isGrounded; //saves the grounded status of the pawn from the controller
    [SerializeField]
    protected LayerMask enemyLayer; //the layer masks that all enemies are on
    [SerializeField]
    protected float damage; //the damage our combatant does
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
    }
    protected virtual void Update() 
    {
        isGrounded = controller.IsGrounded;


        if (animCounter > 0) 
        {
            animCounter -= Time.time; //decrements animCounter
        }
        else if (animCounter <= 0)
        {
            ani.SetBool("HitA", false);
            
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
    */
    #endregion
    #region Terrestrial Melee Attacks
    public virtual void HitA() 
    {
        if (isGrounded)
        {
            ani.SetBool("HitA", true);
            animCounter = animTimer;
            //create a circle and return all the colliders within the area into an array
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(hitAPos.position, hitAVector, enemyLayer);
            //for every collider in that array
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<Health>().Damage(damage);
                Debug.Log("Hit Enemy: " + enemiesToDamage[i].name);
                _canAttack = false;
            }
        }
        if (!isGrounded)
        {
            ani.SetBool("HitA", true);
            animCounter = animTimer;
            //create a circle and return all the colliders within the area into an array
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(hitAJumpPos.position, hitAJumpVector, enemyLayer);
            //for every collider in that array
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<Health>().Damage(damage);
                Debug.Log("Hit Enemy: " + enemiesToDamage[i].name);
                _canAttack = false;
            }
        }
        Debug.Log("Hit A Complete!");
    }
    #endregion
    #region Gizmos
    /// <summary>
    /// Gizmo for visually displaying the attack range
    /// </summary>
    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red; //makes Gizmo for Hitbox A red
        //Gizmos.DrawWireCube(hitAPos.position, hitAVector); //displays the size and shape of hitbox A
        Gizmos.DrawWireCube(hitAJumpPos.position, hitAJumpVector); //displays the size and shape of hitbox A in the air
    }
}
    #endregion
    #endregion
