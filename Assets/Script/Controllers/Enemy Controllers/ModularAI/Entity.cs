/*
 * Master Entity Class
 * includes functions for things such as AI sences,
 * enemies components (rigidbody, animator, health, etc)
 * along with the movement and flip functions 
 * 
 */
#region Include
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

#region Requirements
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
#endregion

#region Class
public class Entity : MonoBehaviour
{
    #region Variables
    #region Components
    public StateMachine fsm;
    public Rigidbody2D rb { get; private set; }
    public Animator ani { get; private set; }
    public Health health { get; private set; }
    public Data_Entity entityData;
    public GameObject target { get; private set; }

    #endregion
    #region Movement
    public int facingDirection { get; private set; }
    private Vector2 tempV2;
    

    [SerializeField]
    private Transform wallCheck,
        ledgeCheck;
    #endregion
    #endregion

    public virtual void Start()
    {
        //get components
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        health = GetComponent<Health>();
        target = GameManager.Instance.player.gameObject;

        fsm = new StateMachine();//Create state machine

        facingDirection = 1;
    }

    public virtual void Update()
    {
        fsm.currentState.LogicUpdate();//call logic update in update
    }

    public virtual void FixedUpdate()
    {
        fsm.currentState.PhysicsUpdate();//call physics update in fixedupdate
    }

    public virtual void SetVelocity(float velocity)
    {
        tempV2.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = tempV2;
    }
    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, wallCheck.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }
    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }
    public virtual bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < entityData.viewDistance;
    }
    public virtual bool CanSeeTarget()
    {
        return Vector2.Distance(transform.position, target.transform.position) < entityData.viewDistance;//this is a simple range check
    }
    public virtual bool LeftRight()
    {
        if (target.transform.position.x > transform.position.x)//target is on right side of entity, should be facing right
        {
            if (facingDirection == 1)//entity is facing right
            {
                return false;//no flip needed
            }
            else
            {
                return true;//need to flip
            }
        }
        else//target is on the left (or on top of) entity
        {
            if(facingDirection == -1)//entity facing left
            {
                return false;//no flip
            }
            else//entity facing right
            {
                return true;//flip
            }
        }
    }
    public virtual void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }
    #region Gizmos

    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(entityData.wallCheckDistance * facingDirection * Vector2.right));
        Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));
    }
    #endregion
}
#endregion