using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State
{
    #region Vars
    protected FiniteStateMachine stateMachine;
    protected Entity entity;

    protected float startTime;

    #endregion
    #region Constructor
    public State(Entity ent, FiniteStateMachine fsm)
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
}
