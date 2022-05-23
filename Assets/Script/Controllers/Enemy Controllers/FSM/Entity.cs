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
    public FiniteStateMachine fsm;
    public Rigidbody2D rb { get; private set; }
    public Animator ani { get; private set; }
    public Health health { get; private set; }
    public Data_Entity entityData;

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

        fsm = new FiniteStateMachine();//Create state machine
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
        return Physics2D.Raycast(ledgeCheck.position, ledgeCheck.transform.right, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }
    public virtual void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }

}
#endregion