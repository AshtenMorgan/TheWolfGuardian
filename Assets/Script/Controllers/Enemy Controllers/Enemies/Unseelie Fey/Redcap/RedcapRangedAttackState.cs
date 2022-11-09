/*
 * The Redcap Ranged Attack State. 
 * Handles what happens when the
 * Redcap throws rocks.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedcapRangedAttackState : RangedAttackState
{
    private float attackTime;

    private RedcapTest enemy;
    public RedcapRangedAttackState(Entity ent, StateMachine fsm, Data_RangedAttackState data, RedcapTest testEnemy) : base(ent, fsm, data)
    {
        enemy = testEnemy;
    }

    public override void Enter()
    {
        enemy.ani.SetBool("insideAttackDistance", true);
        enemy.ani.SetTrigger("fireRangedAttack");
        attackTime = Time.time;
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void LogicUpdate()
    {
        if (!enemy.CanSeeTarget())
        {
            stateMachine.ChangeState(enemy.idleState);
        }

        if (Time.time > attackTime + stateData.redAttackCooldown)
        {
            enemy.redProjectile();
        }
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
