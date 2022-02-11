using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Combat : MonoBehaviour
{
    #region Variables
    #region General Variables
    protected Pawn pawn; //stores the pawn of our combatant
    protected bool _canAttack; //when this says hit you say how hard
    protected Animator ani; //stores the animator of the combatant
    [SerializeField]
    protected float damage; //the damage our combatant does
    #endregion
    #region Timers
    [SerializeField]
    protected float animTimer; //Tracks the amount of time before the animation of an attack resets
    protected float animCounter; //actually does the counting for animTimer
    #endregion
    #region Colliders
    [SerializeField]
    GameObject hitACollider;
    #endregion
    #endregion
    #region Functions
    #region Startup Functions
    protected virtual void Awake()
    {
        pawn = GetComponent<PlayerPawn>(); //defines the pawn of the combatant
        ani = GetComponent<Animator>(); //defines the animator for the Combatant
        animCounter = 0f;
    }
    protected virtual void Update() 
    {   
        
        if (animCounter > 0) 
        {
            animCounter -= Time.time; //decrements animCounter
        }
        else if (animCounter <= 0)
        {
            ani.SetBool("HitA", false);
            hitACollider.SetActive(false);
        }
        
    }
    #endregion
    
    #region Combat Tools
    protected virtual void OnAttackEnable() 
    {
        _canAttack = true; //gives us permission to hit the things
    }
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
    #region Terrestrial Melee Attacks
    public virtual void HitA() 
    {
        ani.SetBool("HitA", true);
        hitACollider.SetActive(true);
        animCounter = animTimer;
    }
    #endregion
    #endregion
}
