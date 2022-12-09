/*
 * Master Melee Attack State
 * Things that may be usefull for all return states
 * not much here yet
 * */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackState : State
{
    protected Data_MeleeAttackState stateData;
    public MeleeAttackState(Entity ent, StateMachine fsm, Data_MeleeAttackState meleeAttackStateData): base(ent, fsm)
    {
        stateData = meleeAttackStateData;
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
