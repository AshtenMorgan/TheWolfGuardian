//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.InputSystem;
//using Pathfinding;

//[RequireComponent(typeof(Rigidbody))]
//[RequireComponent(typeof(Animator))]
//[RequireComponent(typeof(Health))]
//[RequireComponent(typeof(Seeker))]
//public class EnemyController : MonoBehaviour
//{
//    #region vars
//    #region StateMachine

//    protected enum State
//    {
//        Patrol,
//        Chase,
//        Flee,
//        MeleeAttack,
//        RangedAttack,
//        Hurt,
//        Dead
//    }
//    [SerializeField]
//    protected State currentState = State.Patrol;
//    #endregion
//    [Header("Pathfinding")]
//    public Transform target,
//        patrolTarget;
//    public float activateDistance,
//        patrolDistance = 5.0f,
//        updateSeconds;

//    [Header("Physics")]
//    public float speed = 200.0f,
//        nextWPDistance = 3.0f,
//        jumpNodeHeightRequirement = 2.0f,
//        jumpModifier = 0.3f,
//        jumpCheckOffset = 0.1f;

//    [Header("Other")]
//    public bool isFollowEnabled = true,
//        jumpEnabled = true,
//        isFlipEnabled = true,
//        patrolResume = false,
//        isMovingRight = false;

//    [SerializeField]
//    private Path path;
//    [SerializeField]
//    private int currentWP = 0;
//    [SerializeField]
//    private Transform[] wayPoints;
//    [SerializeField]
//    private Seeker seeker;
//    [SerializeField]
//    private Rigidbody2D rb;
//    [SerializeField]
//    private Animator animator;
//    [SerializeField]
//    Vector2 direction;






//    #endregion
//    #region functions
//    #region start/update
//    protected void OnEnable()
//    {
//        patrolTarget = transform.GetChild(0);
//        seeker = GetComponent<Seeker>();
//        rb = GetComponent<Rigidbody2D>();
//        animator = GetComponent<Animator>();
//        InvokeRepeating(nameof(UpdatePath), 0.0f, updateSeconds);
//        if (GameManager.Instance.player != null)
//        {
//            target = GameManager.Instance.player.transform;
//        }

//    }
//    private void Update()
//    {

//    }
//    protected void FixedUpdate()
//    {
//        #region State Machine
//        switch (currentState)
//        {
//            case State.Patrol:
//                UpdatePatrolState();
//                break;
//            case State.Chase:
//                UpdateChaseState();
//                break;
//            case State.Flee:
//                UpdateFleeState();
//                break;
//            case State.MeleeAttack:
//                UpdateMeleeAttackState();
//                break;
//            case State.RangedAttack:
//                UpdateRangedAttackState();
//                break;
//            case State.Hurt:
//                UpdateHurtState();
//                break;
//            case State.Dead:
//                UpdateDeadState();
//                break;
//        }
//        #endregion

//    }
//    #endregion
//    #region States
//    #region Patrol 
//    // Patrol State
//    protected virtual void EnterPatrolState()
//    {

//    }
//    protected virtual void UpdatePatrolState()
//    {
//        Patrol();
//    }
//    protected virtual void ExitPatrolState()
//    {
//    }
//    #endregion
//    #region Chase
//    //Chase State
//    protected virtual void EnterChaseState()
//    {
//    }
//    protected virtual void UpdateChaseState()
//    {
//        Chase();
//    }
//    protected virtual void ExitChaseState()
//    {
//        patrolResume = true;
//    }
//    #endregion
//    #region Flee
//    //Flee State
//    protected virtual void EnterFleeState()
//    {

//    }
//    protected virtual void UpdateFleeState()
//    {

//    }
//    protected virtual void ExitFleeState()
//    {

//    }
//    #endregion
//    #region Attack
//    protected virtual void EnterMeleeAttackState()
//    {

//    }
//    protected virtual void UpdateMeleeAttackState()
//    {

//    }
//    protected virtual void ExitMeleeAttackState()
//    {

//    }

//    protected virtual void EnterRangedAttackState()
//    {

//    }
//    protected virtual void UpdateRangedAttackState()
//    {

//    }
//    protected virtual void ExitRangedAttackState()
//    {

//    }
//    #endregion
//    #region Hurt
//    //Hurt State
//    protected virtual void EnterHurtState()
//    {

//    }
//    protected virtual void UpdateHurtState()
//    {

//    }
//    protected virtual void ExitHurtState()
//    {

//    }
//    #endregion

//    #region Dead
//    //Dead State
//    protected virtual void EnterDeadState()
//    {
//        //stuff to do on death
//        //this is already mostly handled by object pool and health kill function
//    }
//    protected virtual void UpdateDeadState()
//    {

//    }
//    protected virtual void ExitDeadState()
//    {

//    }
//    #endregion
//    #endregion
//    #region State Manager
//    protected virtual void StateManager(State state)
//    {
//        //exit current state
//        switch (state)
//        {
//            case State.Patrol:
//                ExitPatrolState();
//                break;
//            case State.Chase:
//                ExitChaseState();
//                break;
//            case State.Flee:
//                ExitFleeState();
//                break;
//            case State.MeleeAttack:
//                ExitMeleeAttackState();
//                break;
//            case State.RangedAttack:
//                ExitRangedAttackState();
//                break;
//            case State.Hurt:
//                ExitHurtState();
//                break;
//            case State.Dead:
//                ExitDeadState();
//                break;
//        }

//        //enter new state
//        switch (state)
//        {
//            case State.Patrol:
//                EnterPatrolState();
//                break;
//            case State.Chase:
//                EnterChaseState();
//                break;
//            case State.Flee:
//                EnterFleeState();
//                break;
//            case State.MeleeAttack:
//                EnterMeleeAttackState();
//                break;
//            case State.RangedAttack:
//                EnterRangedAttackState();
//                break;
//            case State.Hurt:
//                EnterHurtState();
//                break;
//            case State.Dead:
//                EnterDeadState();
//                break;
//        }

//        currentState = state;//update state
//    }

//    #endregion

//    #region Movements
//    protected void Chase()
//    {
//        if (TargetInDistance() && isFollowEnabled)
//            PathFollow();
//        else
//            StateManager(State.Patrol);
//    }
//    protected void Jump()
//    {

//        if (jumpEnabled && IsGrounded())
//            if (direction.y > jumpNodeHeightRequirement)
//                rb.velocity = new Vector2(rb.velocity.x, jumpModifier * speed);


//    }
//    protected void Patrol()
//    {
//        float distance = Vector2.Distance(rb.transform.position, patrolTarget.position);

//        if (TargetInDistance())
//            StateManager(State.Chase);

//        if (patrolResume)
//        {
//            if (patrolTarget.position != rb.transform.position)
//            {
//                rb.velocity = MoveTowards();
//                return;
//            }
//            else
//            {
//                patrolResume = false;
//                isMovingRight = true;
//            }

//        }

//        if ((!patrolResume) && (patrolDistance < distance))
//        {
//            rb.velocity = MoveAway();
//            return;
//        }
//        else if ((!patrolResume) && (patrolDistance >= distance))
//        {
//            patrolResume = true;
//            rb.velocity = MoveTowards();
//        }



//    }
//    #endregion

//    /*Alt Damage Function
//     * 
//     * this way we can see what direction an attack comes from (along with other details)
//     * may not actually use this but it is worth thinking about
//    protected void Damage(float[] attackDetails)
//    {
        
//        enemy.currentHealth -= attackDetails[0];
//        if (attackDetails[1] > enemy.transform.position.x)
//        {
//            damageDirection = -1;
//        }
//        else
//        {
//            damageDirection = 1;
//        }
//        //other hit effects
//        if (enemy.currentHealth > 0.0f )
//        {
//            StateManager(State.Hurt);
//        }
//        else if (enemy.currentHealth < 0.0f)
//        {
//            StateManager(State.Dead);
//        }
        
//    }
//    */

//    #region misc
//    protected void UpdatePath()
//    {
//        if (isFollowEnabled && TargetInDistance() && seeker.IsDone())
//        {
//            seeker.StartPath(rb.position, target.position, OnPathComplete);
//        }
//    }
//    protected void PathFollow()
//    {
//        if (path == null)
//        {
//            return;
//        }

//        //end of path
//        if (currentWP >= path.vectorPath.Count)
//        {
//            return;
//        }

//        //removed for bool return, isGrounded check would be here

//        //GetDirections would be here

//        //jump
//        Jump();
//        rb.velocity = GetForce();

//        //look for next waypoint
//        GetNextWP();

//        //check for flip
//        Flip();
//    }
//    public bool IsGrounded()
//    {
//        return Physics2D.Raycast(transform.position, -Vector3.up, GetComponent<Collider2D>().bounds.extents.y + jumpCheckOffset);
//    }
//    protected void GetNextWP()
//    {
//        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWP]);
//        if (distance < nextWPDistance)
//            currentWP++;
//    }
//    protected Vector2 GetForce()
//    {
//        direction = ((Vector2)path.vectorPath[currentWP] - rb.position).normalized;
//        Vector2 force = speed * Time.deltaTime * direction;
//        return force;
//    }
//    protected Vector2 MoveAway()
//    {
//        direction = ((Vector2)patrolTarget.position + rb.position).normalized;
//        Vector2 force = speed * Time.deltaTime * direction;
//        return force;
//    }
//    protected Vector2 MoveTowards()
//    {
//        direction = ((Vector2)patrolTarget.position - rb.position).normalized;
//        Vector2 force = speed * Time.deltaTime * direction;
//        return force;
//    }
//    protected void Flip()
//    {
//        if (isFlipEnabled)
//        {
//            if (rb.velocity.x > 0.05f)//moving right
//                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
//            else if (rb.velocity.x < -0.05f)//moving left
//                transform.localScale = new Vector3(-1.0f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
//        }
//    }
//    protected bool TargetInDistance()
//    {
//        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
//    }
//    protected void OnPathComplete(Path p)
//    {
//        if (!p.error)
//        {
//            path = p;
//            currentWP = 0;
//        }
//    }
//    protected void OnDisable()
//    {
//        patrolTarget.transform.parent = this.transform;

//        CancelInvoke();
//    }

//    #endregion
//    #endregion
//}
