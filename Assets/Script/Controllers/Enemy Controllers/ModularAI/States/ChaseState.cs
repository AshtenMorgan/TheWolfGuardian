/*
 * Master Chase State
 * Things that may be useful for all chase states
 * nothing really here yet
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    protected Data_ChaseState stateData;
    public ChaseState(Entity ent, StateMachine fsm, Data_ChaseState chaseStateData) : base(ent, fsm)
    {
        stateData = chaseStateData;
    }
    
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
}
