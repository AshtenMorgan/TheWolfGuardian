/* 
 * Ranged Attack state, rolls a random number, used to determine
 * attack A or B, then creates prefab and "throws" it in direction
 * entity is facing 
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderRangedAttackState : RangedAttackState
{
    private float attackTime;
 
    private SpiderTest enemy;
    public SpiderRangedAttackState(Entity ent, StateMachine fsm, Data_RangedAttackState data, SpiderTest testEnemy) : base(ent, fsm, data)
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
            stateMachine.ChangeState(enemy.idleState);

        if (Time.time >= (attackTime + stateData.attackCooldown)) 
            DetermineAttack();
 
        base.LogicUpdate();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }
    private void DetermineAttack()
    {
        int check = Random.Range(0, 101);
        if(check > 49)
        {
            Debug.Log("Attack A");
            enemy.RangedAttackA();
            attackTime = Time.time;
        }
        else
        {
            Debug.Log("Attack B");
            enemy.RangedAttackB();
            attackTime = Time.time;
        }
    }
}
