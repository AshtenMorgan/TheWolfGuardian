/*
 * Master State
 * Things that may be useful for all states
 * right now only sets start time upon state
 * entry
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    #region Vars
    protected StateMachine stateMachine;
    protected Entity entity;

    protected bool shouldFlip;
    protected float startTime;

    #endregion
    #region Constructor
    public State(Entity ent, StateMachine fsm)
    {
        entity = ent;
        stateMachine = fsm;
    }
    #endregion

    public virtual void Enter()
    {
        startTime = Time.time;//track enter time
    }
    public virtual void LogicUpdate()
    {

    }
    public virtual void PhysicsUpdate()
    {

    }
    public virtual void Exit()
    {

    }
    public virtual void SetFlip(bool flip)
    {
        shouldFlip = flip;
    }
}
