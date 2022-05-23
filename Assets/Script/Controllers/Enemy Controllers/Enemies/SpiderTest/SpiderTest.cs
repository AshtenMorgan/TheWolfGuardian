using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderTest : Entity
{
    public SpiderIdleState idleState { get; private set; }
    public SpiderMoveState moveState { get; private set; }
    
    [SerializeField]
    private Data_IdleState idleStateData;
    [SerializeField]
    private Data_MoveState moveStateData;

    public override void Start()
    {
        base.Start();
        idleState = new SpiderIdleState(this, fsm, idleStateData, this);
        moveState = new SpiderMoveState(this, fsm, moveStateData, this);

        fsm.Initialize(moveState);
    }
}
