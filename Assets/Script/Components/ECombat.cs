using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ECombat : MonoBehaviour
{
    #region Variables
    #region General Variables
    [Header("General Pawn Attributes")]
    protected bool _canAttack; //when this says hit you say how hard
    protected Pawn pawn; //stores the pawn of our combatant
    protected Animator ani; //stores the animator of the combatant 
    protected EnemyController EC; //store enemy controller
    protected int nextHit = 0; //counter for what hit AI is on
    [SerializeField]
    protected LayerMask playerLayer; //the layer masks that all enemies are on
    [SerializeField]
    protected float damageA,
        damageB,
        damageC;//the damage our combatant does
    #endregion
    #region Timers
    [SerializeField]
    protected float animTimer; //Tracks the amount of time before the animation of an attack resets
    protected float animCounter; //actually does the counting for animTimer
    protected float comboTimer; //track time for combo
    protected float comboCounter; //countdown for combo
    protected float decayDelay = 3.0f; //how long before combo is reset
    #endregion
    #region Colliders
    [Header("Hitbox A Attributes")]
    [Header("Hitbox Attributes")]
    #region Hitbox A
    [SerializeField, Tooltip("the position of Hit A's hitbox.")]
    protected Transform hitAPos; //the position of hit A's hitbox
    [SerializeField, Tooltip("the length and width of Hit A's hitbox.")]
    protected Vector3 hitAVector; //the size of hit A's hitbox
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
        EC = GetComponent<EnemyController>();
        animCounter = 0f;
        damageA = pawn.DamageA;
        damageB = pawn.DamageB;
        damageC = pawn.DamageC;
        _canAttack = true;

    }
    protected virtual void Update()
    {
        /* 
        * if time.time > next 
        * (timer ran out, do stuff)
        * 
        * if time.time < next 
        * (timer not out, do stuff)
        * 
        * next = time.time + delay
        * 
        */
        if (Time.time > animCounter)
        {
            ani.SetBool("HitA", false);
            ani.SetBool("HitB", false);
            ani.SetBool("HitC", false);
            _canAttack = true;
        }
    }
    #endregion

    #region Combat Tools
    protected virtual void OnAttackEnable()
    {
        _canAttack = true; //gives us permission to hit the things
    }

    #endregion
    #region Combo Attack Functions
    public void ECombo1()
   {
        if (EC.IsGrounded())
        {
            if (_canAttack && (Time.time < comboTimer))//timer has not run out
            {
                switch (nextHit)
                {
                    case 0:
                        HitA();
                        nextHit = 1;
                        comboTimer = Time.time + decayDelay;
                        Debug.Log("Hit A done");
                        break;
                    case 1:
                        HitB();
                        nextHit = 2;
                        comboTimer = Time.time + decayDelay;
                        Debug.Log("Hit B done");
                        break;
                    case 2:
                        HitC();
                        nextHit = 0;
                        comboTimer = Time.time + decayDelay;
                        Debug.Log("Hit C done");
                        break;
                }
            }
            else if (_canAttack && (Time.time > comboTimer))//timer has hit 0
            {
                HitA();
                comboTimer = Time.time + decayDelay;
                nextHit = 1;
                Debug.Log("Hit A done");
            }
        }
    }
    #region Hit A Function
    public virtual void HitA()
    {
        ani.SetBool("HitA", true);
        animCounter = Time.time + animTimer;
        //create a circle and return all the colliders within the area into an array
        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(hitAPos.position, hitAVector, 0, playerLayer);
        //for every collider in that array
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<Health>().Damage(damageA);
            _canAttack = false;
        }
    }
    #endregion
    #region Hit B Function
    public virtual void HitB()
    {
        ani.SetBool("HitB", true);
        animCounter = Time.time + animTimer;
        //create a circle and return all the colliders within the area into an array
        //enemy layer check is a good idea, but what about when we want to hit breakable walls?
        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(hitBPos.position, hitBVector, 0, playerLayer);
        //for every collider in that array
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            if (enemiesToDamage[i].CompareTag("Enemy"))//make sure this is tagged as enemy
            {
                enemiesToDamage[i].GetComponent<Health>().Damage(damageB);
                _canAttack = false;
            }
        }
    }
    #endregion
    #region Hit C Function
    public virtual void HitC()
    {
        ani.SetBool("HitC", true);
        animCounter = Time.time + animTimer;
        //create a circle and return all the colliders within the area into an array
        Collider2D[] enemiesToDamage = Physics2D.OverlapBoxAll(hitCPos.position, hitCVector, 0, playerLayer);
        //for every collider in that array
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<Health>().Damage(damageC);
            _canAttack = false;
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
        Gizmos.DrawWireCube(hitAPos.position, hitAVector); //displays the size and shape of hitbox A
        //Gizmos.DrawWireCube(hitAJumpPos.position, hitAJumpVector); //displays the size and shape of hitbox A in the air
        //Gizmos.DrawWireCube(hitACrouchPos.position, hitACrouchVector); //displays the size and shape of hitbox A while crouching
        #endregion
        #region#region Hitbox B Gizmos
        Gizmos.DrawWireCube(hitBPos.position, hitBVector);// displays the size and shape of Hitbox B
        #endregion
        #region#region Hitbox C Gizmos
        Gizmos.DrawWireCube(hitCPos.position, hitCVector);// displays the size and shape of Hitbox B
        #endregion
    }
}
#endregion
#endregion
