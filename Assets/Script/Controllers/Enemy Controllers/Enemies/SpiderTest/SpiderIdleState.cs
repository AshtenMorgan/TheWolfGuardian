using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderIdleState : IdleState
{
    private SpiderTest enemy;
    public SpiderIdleState(Entity ent, FiniteStateMachine fsm, Data_IdleState data, SpiderTest tesetEnemy) : base(ent, fsm, data)
    {
        enemy = tesetEnemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if (isIdleOver)
            stateMachine.ChangeState(enemy.moveState);
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
