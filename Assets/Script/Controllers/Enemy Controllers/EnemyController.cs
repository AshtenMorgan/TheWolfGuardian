using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyController : Controller
{
    #region vars
    //vars to be visable for testing purposes only.
    #region StateMachine
    
    protected enum State
    {
        Patrol,
        Chase,
        Flee,
        Attack,
        Hurt,
        Dead
    }
    protected State currentState;
    #endregion

    #region objects
    [Header("Enemy Variables")]
    [SerializeField]
    PlayerPawn target;
    [SerializeField]
    EnemyPawn enemy;
    [SerializeField]
    protected Transform wallCheck,
        eGroundCheck;

    [SerializeField, Header("Movement stats")]
    protected Vector2 movement,
                      knockBack;

    //LayerMask groundLayer
    [SerializeField]
    private LayerMask enemyLayer;
    #endregion
    #region General variables
    [SerializeField]
    protected int   facingDirection,
                    damageDirection;

    [SerializeField]
    protected float patrolRange = 5.0f,
                    groundCheckDistance = .5f,
                    wallCheckDistance = .5f,
                    harmStart,
                    harmDuration;

    [SerializeField]
    protected bool isGroundDetected = true,
                   isWallDetected = false,
                   isEnemyDetected = false;
    #endregion

    #endregion
    #region functions
    #region start/update
    // Start is called before the first frame update
    Health health;
    protected override void Start()
    {
        health = GameObject.FindWithTag("Player").GetComponent<Health>();
        facingDirection = 1;//face right
        ani = GetComponent<Animator>();//get animator component
        enemy = GetComponent<EnemyPawn>();//reference this objects pawn
        target = GameManager.Instance.Player;//get player from game manager
        if (!eGroundCheck)
        {
            eGroundCheck = GetComponent("GroundCheck").transform;//reference ground check
        }
        if (!wallCheck)
        {
            wallCheck = GetComponent("WallCheck").transform; ;//get wall check component
        }
        

        StateManager(State.Patrol);//start patrol
        base.Start();//call parents start function
    }

    // Update is called once per frame
    protected override void Update()
    {
        #region State Machine
        switch (currentState)
        {
            case State.Patrol:
                UpdatePatrolState();
                break;
            case State.Chase:
                UpdateChaseState();
                break;
            case State.Flee:
                UpdateFleeState();
                break;
            case State.Attack:
                UpdateAttackState();
                break;
            case State.Hurt:
                UpdateHurtState();
                break;
            case State.Dead:
                UpdateDeadState();
                break;
        }
        #endregion

        base.Update();//calls whatever is in parents update function
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();//call parent function   
    }
    #endregion



    #region States
    #region Patrol 
    // Patrol State
    protected virtual void EnterPatrolState()
    {

    }
    protected virtual void UpdatePatrolState()
    {
        isGroundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, groundLayer);//raycast for ground
        isWallDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, groundLayer);//raycast for wall
        isEnemyDetected = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, enemyLayer);//look for other enemies

        if (!isGroundDetected || isWallDetected || isEnemyDetected)//there is no ground or there is a wall
        {
            Flip();//turn around
        }
        else
        {
            //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, enemy.WalkSpeed * Time.deltaTime);//move towards player by "speed" units per second
            movement.Set(enemy.WalkSpeed * facingDirection, rb2d.velocity.y);
            rb2d.velocity = movement;
        }
    }
    protected virtual void ExitPatrolState()
    {

    }
    #endregion

    #region Chase
    //Chase State
    protected virtual void EnterChaseState()
    {

    }
    protected virtual void UpdateChaseState()
    {

    }
    protected virtual void ExitChaseState()
    {

    }
    #endregion

    #region Flee
    //Flee State
    protected virtual void EnterFleeState()
    {

    }
    protected virtual void UpdateFleeState()
    {

    }
    protected virtual void ExitFleeState()
    {

    }
    #endregion

    #region Attack
    protected virtual void EnterAttackState()
    {

    }
    protected virtual void UpdateAttackState()
    {

    }
    protected virtual void ExitAttackState()
    {

    }
    #endregion

    #region Hurt
    //Hurt State
    protected virtual void EnterHurtState()
    {
        harmStart = Time.time;//track when enemy was hit
        //hit animation
        //apply a knockback/stun/slow
    }
    protected virtual void UpdateHurtState()
    {
        if (Time.time >= harmStart + harmDuration)
        {
            StateManager(State.Patrol);
        }
    }
    protected virtual void ExitHurtState()
    {

    }
    #endregion

    #region Dead
    //Dead State
    protected virtual void EnterDeadState()
    {
        //stuff to do on death
    }
    protected virtual void UpdateDeadState()
    {

    }
    protected virtual void ExitDeadState()
    {

    }
    #endregion
    #endregion

    #region State Manager
    protected virtual void StateManager(State state)
    {
        //exit current state
        switch (currentState)
        {
            case State.Patrol:
                ExitPatrolState();
                break;
            case State.Chase:
                ExitChaseState();
                break;
            case State.Flee:
                ExitFleeState();
                break;
            case State.Attack:
                ExitAttackState();
                break;
            case State.Hurt:
                ExitHurtState();
                break;
            case State.Dead:
                ExitDeadState();
                break;
        }

        //enter new state
        switch (state)
        {
            case State.Patrol:
                EnterPatrolState();
                break;
            case State.Chase:
                EnterChaseState();
                break;
            case State.Flee:
                EnterFleeState();
                break;
            case State.Attack:
                EnterAttackState();
                break;
            case State.Hurt:
                EnterHurtState();
                break;
            case State.Dead:
                EnterDeadState();
                break;
        }

        currentState = state;//update state
    }

    #endregion

    //this way we can see what direction an attack comes from (along with other details)
   // may not actually use this but it is worth thinking about
    protected virtual void Damage(float[] attackDetails)
    {
        /*
        enemy.currentHealth -= attackDetails[0];
        if (attackDetails[1] > enemy.transform.position.x)
        {
            damageDirection = -1;
        }
        else
        {
            damageDirection = 1;
        }
        //other hit effects
        if (enemy.currentHealth > 0.0f )
        {
            StateManager(State.Hurt);
        }
        else if (enemy.currentHealth < 0.0f)
        {
            StateManager(State.Dead);
        }
        */
    }




    protected virtual void Flip()
    {
        facingDirection *= -1;
        gameObject.transform.Rotate(0, 180, 0);//rotate this game object 180 degrees (y)
    }

    protected virtual PlayerPawn Locate()
    {
        //locate player
        PlayerPawn pawn = null;
        return pawn;
    }





    #endregion
}
