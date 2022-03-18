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
    protected bool isCrouching; //determines if the pawn is crouching
    [SerializeField]
    protected LayerMask enemyLayer; //the layer masks that all enemies are on
    [SerializeField]
    protected float damage; //the damage our combatant does
    #endregion
    #region Timers
    [SerializeField]
    protected float animTimer; //Tracks the amount of time before the animation of an attack resets
    protected float animCounter; //actually does the counting for animTimer
    [SerializeField]
    protected float comboTimer; //the set time before your combos restart
    protected float comboCounter; //the counter for the combo timer.
    #endregion
    #region Colliders
    [Header("Hitbox A Attributes")]
    [Header("Hitbox Attributes")]
    #region Hitbox A
    protected float hitAComboCounter; //the counter that allows for combos within the Hit A chain
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
    [SerializeField, Tooltip("the position of Hit A 1's hitbox.")]
    protected Transform hitA1Pos; //the position of hit A 1's hitbox
    [SerializeField, Tooltip("the length and width of Hit A 1's hitbox.")]
    protected Vector3 hitA1Vector; //the size of hit A 1's hitbox
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
        comboCounter = 0f;
    }
    protected virtual void Update() 
    {
        Debug.Log(hitAComboCounter);
        isGrounded = controller.IsGrounded;
        isCrouching = controller.IsCrouching;

        if (animCounter > 0) 
        {
            animCounter -= Time.time; //decrements animCounter
        }
        else if (animCounter <= 0)
        {
            ani.SetBool("HitA", false);
            ani.SetBool("HitA1", false);
            ani.SetBool("HitB", false);
            ani.SetBool("HitC", false);
        }
        if (comboCounter > 0)
        {
            InvokeRepeating("SubtractComboCounter",2.0f,0.3f); //decrements the comboCounter
        }
        else if (comboCounter <= 0)
        {
            hitAComboCounter = 0;
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
    #region Combo Attack Functions
    #region Hit A Function
    public virtual void HitA() 
    {
        if (isGrounded)
        {
            ani.SetBool("HitA", true);
            animCounter = animTimer;
            comboCounter = comboTimer;
            hitAComboCounter++; //incrememnts the hitAComboCounter
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

        if (isGrounded && hitAComboCounter == 2)
        {
            ani.SetBool("HitA1", true);
            animCounter = animTimer;
            //create a circle and return all the colliders within the area into an array
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(hitA1Pos.position, hitA1Vector, enemyLayer);
            //for every collider in that array
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<Health>().Damage(damage);
                Debug.Log("Hit Enemy: " + enemiesToDamage[i].name);
                _canAttack = false;
            }
            hitAComboCounter = 0f;
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
        if (isGrounded && isCrouching)
        {
            ani.SetBool("HitA", true);
            animCounter = animTimer;
            //create a circle and return all the colliders within the area into an array
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(hitACrouchPos.position, hitACrouchVector, enemyLayer);
            //for every collider in that array
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<Health>().Damage(damage);
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
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(hitBPos.position, hitBVector, enemyLayer);
            //for every collider in that array
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<Health>().Damage(damage);
                Debug.Log("Hit Enemy: " + enemiesToDamage[i].name);
                _canAttack = false;
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
            Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(hitCPos.position, hitCVector, enemyLayer);
            //for every collider in that array
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                enemiesToDamage[i].GetComponent<Health>().Damage(damage);
                Debug.Log("Hit Enemy: " + enemiesToDamage[i].name);
                _canAttack = false;
            }
        }
    }
    #endregion
    #endregion
    #region Combo Tools
    protected virtual void SubtractComboCounter() 
    {
        comboCounter--; //decrements the combo counter
    }
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
        Gizmos.DrawWireCube(hitA1Pos.position, hitA1Vector); //displays the size and shape of hitbox A1
        //Gizmos.DrawWireCube(hitAJumpPos.position, hitAJumpVector); //displays the size and shape of hitbox A in the air
        //Gizmos.DrawWireCube(hitACrouchPos.position, hitACrouchVector); //displays the size and shape of hitbox A while crouching
        #endregion
        #region#region Hitbox B Gizmos
        //Gizmos.DrawWireCube(hitBPos.position, hitBVector);// displays the size and shape of Hitbox B
        #endregion
        #region#region Hitbox C Gizmos
        //Gizmos.DrawWireCube(hitCPos.position, hitCVector);// displays the size and shape of Hitbox C
        #endregion
    }
}
    #endregion
    #endregion
