/*
 * Chase state for the Redcap Enemy.
 * Handles what happens with the chase
 * state.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedcapChaseState : ChaseState
{
    private RedcapTest enemy;

    public RedcapChaseState (Entity ent, StateMachine fsm, Data_ChaseState chaseStateData, RedcapTest testEnemy) : base(ent, fsm, chaseStateData)
    {
        enemy = testEnemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.PlayerDetected = true;
    }

    public override void Exit()
    {
        base.Exit();
        enemy.PlayerDetected = false;
    }

    public override void LogicUpdate()
    {
        shouldFlip = entity.LeftRight();

        if(!enemy.CanSeeTarget())
        {
            stateMachine.ChangeState(enemy.idleState);
        }

        if (shouldFlip)
        {
            entity.Flip();
        }

        if (Vector2.Distance(entity.transform.position, GameManager.Instance.player.transform.position) <= entity.entityData.ranged)
        {
            stateMachine.ChangeState(enemy.rangedAttackState);
        }

        base.LogicUpdate();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        entity.SetVelocity(stateData.redRunSpeed);
    }

}
