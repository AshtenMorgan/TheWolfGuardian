using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedcapPatrolState : PatrolState
{
    private RedcapTest enemy;
    public RedcapPatrolState(Entity ent, StateMachine fsm, Data_PatrolState patrolStateData, RedcapTest testEnemy) : base(ent, fsm, patrolStateData)
    { 
        enemy = testEnemy;
    }
    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        shouldFlip = entity.LeftRight();
        if(shouldFlip)
        {
            entity.Flip();
        }
        if(entity.PerformDetection() && entity.TargetInDistance())
        {
            stateMachine.ChangeState(enemy.chaseState);
        }

        if(enemy.CheckWall() || enemy.CheckLedge())
        {
            enemy.idleState.SetFlip(true);
            stateMachine.ChangeState(enemy.idleState);
        }
        else
        {
            entity.SetVelocity(stateData.walkSpeed);
        }
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
