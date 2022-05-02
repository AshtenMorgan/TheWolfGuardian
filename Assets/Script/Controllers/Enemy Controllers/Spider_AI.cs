using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Pathfinding;

public class Spider_AI : EnemyController
{/*
  * 
  * 
  * 
    [Header("Pathfinding")]
    public float seekDistance,
        pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    public float speed = 200.0f,
        nextWayPointDistance = 3f,
        jumpNodeHeightRequirement = 0.8f,
        jumpModifier = 0.3f,
        jumpCheckOffset = 0.1f;

    [Header("Custom Behavior")]
    public bool isFollowEnabled = true,
        isJumpEnabled = true,
        isDirectionLookEnabled = true;

    [SerializeField]
    private Path path;
    private Seeker seeker;

    private int currentWaypoint = 0;



    // Start is called before the first frame update
    protected override void Start()
    {
        seeker = GetComponent<Seeker>();
        rb2d = GetComponent<Rigidbody2D>();
        InvokeRepeating(nameof(UpdatePath), 0f, pathUpdateSeconds);
        base.Start();
    }
    protected override void FixedUpdate()
    {
        if (TargetInDistance() && isFollowEnabled)
        {
            PathFollow();
        }
        base.FixedUpdate();
    }
    void UpdatePath()
    {
        if (isFollowEnabled && TargetInDistance() && seeker.IsDone())
            seeker.StartPath(rb2d.position, target.position, OnPathComplete);
        
    }
    void PathFollow()
    {
        if ((path == null) || (currentWaypoint >= path.vectorPath.Count))
            return;
        

        //direction calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb2d.position);
        Vector2 force = speed * Time.deltaTime * direction;

        //jump
        if (isJumpEnabled && IsGrounded)
            if (direction.y > jumpNodeHeightRequirement)
                rb2d.AddForce(Vector2.up * speed * jumpModifier);
            
        
        //move
        rb2d.AddForce(force);

        //next waypoint
        float distance = Vector2.Distance(rb2d.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWayPointDistance)
            currentWaypoint++;

        //detect flip
        if (isDirectionLookEnabled)
        {
            if (rb2d.velocity.x > 0.05f)
                transform.localScale = new Vector3(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            if (rb2d.velocity.x < -0.05f)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        }



    }
    bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.position) < seekDistance;
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    */
}
