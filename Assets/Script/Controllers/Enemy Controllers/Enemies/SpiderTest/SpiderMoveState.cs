using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMoveState : MoveState
{
    private SpiderTest enemy;
    public SpiderMoveState(Entity ent, FiniteStateMachine fsm, Data_MoveState moveStateData, SpiderTest testEnemy) : base(ent, fsm, moveStateData)
    {
        enemy = testEnemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(isDetectingWall || !isDetectingLedge)
        {
            enemy.idleState.SetFlip(true);
            stateMachine.ChangeState(enemy.idleState);

        }

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
