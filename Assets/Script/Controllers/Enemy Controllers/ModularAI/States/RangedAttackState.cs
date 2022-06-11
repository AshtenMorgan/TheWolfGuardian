/*
 * Master Ranged Attack State
 * Things that may be useful for all return states
 * nothing here yet
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttackState : State
{
    protected Data_RangedAttackState stateData;

    public RangedAttackState(Entity ent, StateMachine fsm, Data_RangedAttackState rangedAttackStateData) : base(ent, fsm)
    {
        stateData = rangedAttackStateData;
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