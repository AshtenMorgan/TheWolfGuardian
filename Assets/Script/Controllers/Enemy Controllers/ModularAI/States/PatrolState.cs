/*
 * Master Patrol State
 * Things that may be useful for all patrol states
 * nothing really here yet
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : State
{
    protected Data_PatrolState stateData;

    public PatrolState(Entity ent, StateMachine fsm, Data_PatrolState patrolStateData) : base(ent, fsm)
    {
        stateData = patrolStateData;
    }

    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    public override void Exit()
    {
        base.Exit();
    }


}
