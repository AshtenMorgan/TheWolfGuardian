/* 
 * Simple Patrol state, walks till a wall or edge is detected,
 * then idles for a time, and walks in the opposite direction
 * until a ledge or wall is detected, and repeats. 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderPatrolState : PatrolState
{
    private SpiderTest enemy;
    public SpiderPatrolState(Entity ent, StateMachine fsm, Data_PatrolState patrolStateData, SpiderTest testEnemy) : base(ent, fsm, patrolStateData)
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
        if (shouldFlip)
            entity.Flip();

        if (entity.PerformDetection() && entity.TargetInDistance())
            stateMachine.ChangeState(enemy.chaseState);

        if (enemy.CheckWall() || !enemy.CheckLedge())//there is a wall, or ther is a hole
        {
            enemy.idleState.SetFlip(true);//flip on
            stateMachine.ChangeState(enemy.idleState);//enter idle
        }
        else
            entity.SetVelocity(stateData.walkSpeed);//set velocity to movement speed

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
