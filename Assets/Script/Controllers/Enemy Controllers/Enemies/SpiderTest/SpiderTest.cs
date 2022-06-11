/*
 * This is the AI "Controller"
 * If a state should be available to a specific enemy, it
 * should be listed here
 * 
 * Under developement
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderTest : Entity
{
    //list all available states for this entity
    public SpiderIdleState idleState { get; private set; }
    public SpiderPatrolState patrolState { get; private set; }
    public SpiderChaseState chaseState { get; private set; }
    public SpiderRangedAttackState rangedAttackState { get; private set; }

    //state data
    [SerializeField]
    private Data_IdleState idleStateData;
    [SerializeField]
    private Data_PatrolState patrolStateData;
    [SerializeField]
    private Data_ChaseState chaseStateData;
    [SerializeField]
    private Data_RangedAttackState rangedAttackStateData;


    public override void Start()
    {
        base.Start();
        idleState = new SpiderIdleState(this, fsm, idleStateData, this);
        patrolState = new SpiderPatrolState(this, fsm, patrolStateData, this);
        chaseState = new SpiderChaseState(this, fsm, chaseStateData, this);
        rangedAttackState = new SpiderRangedAttackState(this, fsm, rangedAttackStateData, this);

        fsm.Initialize(patrolState);
    }

    public void RangedAttackA()
    {
        GameObject firedShot = Instantiate(rangedAttackStateData.projectilePrefabA, originAttackA.position, Quaternion.identity);
        Rigidbody2D rb = firedShot.GetComponent<Rigidbody2D>();
        rb.AddForce(wallCheck.forward * rangedAttackStateData.projectialSpeedA);
        //play sound
    }
    public void RangedAttackB()
    {
        GameObject firedShot = Instantiate(rangedAttackStateData.projectilePrefabB, originAttackB.position, Quaternion.identity);
        Rigidbody2D rb = firedShot.GetComponent<Rigidbody2D>();
        rb.AddForce(wallCheck.forward * rangedAttackStateData.projectialSpeedB);
        //play sound
    }
}
