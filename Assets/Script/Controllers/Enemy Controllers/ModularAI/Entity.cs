/*
 * Master Entity Class
 * includes functions for things such as AI sences,
 * enemies components (rigidbody, animator, health, etc)
 * along with the movement and flip functions 
 * 
 */
#region Include
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion

#region Requirements
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Health))]
#endregion

#region Class
public class Entity : MonoBehaviour
{

    #region Box Raycast

    public bool PlayerDetected;

    //Moved to SpiderChaseSate
    //public bool PlayerDetected = false;
    [SerializeField, Header("OverlapBox parameters")]
    private Transform detectorOrigin;
    public Vector2 detectorSize = Vector2.one;
    public Vector2 detectorOriginOffset = Vector2.one;
    //public Quaternion boxRotation;

    public float detectionDelay = 0.3f;

    [SerializeField, Header("Gizmo Parameters")]
    public Color gizmoIdleColor = Color.green;
    public Color gizmoDetectedColor = Color.red;
    public bool showGizmos = true;

    private GameObject playerTarget;

    public GameObject PlayerTarget
    {
        get => playerTarget;
        private set
        {
            target = value;
            PlayerDetected = target != null;
        }

    }

    #endregion
    #region Variables
    #region Components
    public StateMachine fsm;
    public Rigidbody2D rb { get; private set; }
    public Animator ani { get; private set; }
    public Health health { get; private set; }
    public Data_Entity entityData;
    public GameObject target { get; private set; }

    #endregion
    #region Movement
    public int facingDirection { get; private set; }
    private Vector2 tempV2;

    [SerializeField]
    private Transform wallCheck,
        ledgeCheck;
    #endregion

    #endregion

    public virtual void Start()
    {
        //get components
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        health = GetComponent<Health>();
        target = GameManager.Instance.player.gameObject;

        fsm = new StateMachine();//Create state machine

        facingDirection = 1;

        //Box cast coroutine
        //StartCoroutine(DetectionCoroutine());
    }

    public virtual void Update()
    {
        fsm.currentState.LogicUpdate();//call logic update in update
    }

    public virtual void FixedUpdate()
    {
        fsm.currentState.PhysicsUpdate();//call physics update in fixedupdate
    }

    //detection delay for AI effictency
    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionDelay);
        PerformDetection();
        StartCoroutine(DetectionCoroutine());
    }
    //This is performed on the expiration of detection delay
    public bool PerformDetection()
    {
        Collider2D collider = Physics2D.OverlapBox(
            (Vector2)wallCheck.position + detectorOriginOffset * Vector3.forward, detectorSize, 0, entityData.whatIsPlayer);
        Debug.Log("Box Drawn");
        if (collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public virtual void SetVelocity(float velocity)
    {
        tempV2.Set(facingDirection * velocity, rb.velocity.y);
        rb.velocity = tempV2;
    }
    public virtual bool CheckWall()
    {
        return Physics2D.Raycast(wallCheck.position, wallCheck.transform.right, entityData.wallCheckDistance, entityData.whatIsGround);
    }
    public virtual bool CheckLedge()
    {
        return Physics2D.Raycast(ledgeCheck.position, Vector2.down, entityData.ledgeCheckDistance, entityData.whatIsGround);
    }
    public bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) <= entityData.viewDistance;
    }
    public bool CanSeeTarget()
    {
        //return Physics2D.Raycast(wallCheck.position, wallCheck.position - target.transform.position, entityData.viewDistance, entityData.whatIsPlayer);
            return Vector2.Distance(transform.position, target.transform.position) <= entityData.viewDistance;

    }
    public virtual void Flip()
    {
        facingDirection *= -1;
        transform.Rotate(0f, 180f, 0f);
    }
    #region Gizmos

    public virtual void OnDrawGizmos()
    {
        //Gizmos.DrawLine(wallCheck.position, wallCheck.position + (Vector3)(entityData.wallCheckDistance * facingDirection * Vector2.right));
        //Gizmos.DrawLine(ledgeCheck.position, ledgeCheck.position + (Vector3)(Vector2.down * entityData.ledgeCheckDistance));
        //Gizmos.DrawLine(wallCheck.position, target.transform.position);

        //Box Cast visual
        //if(showGizmos && wallCheck != null)
        //{
        //    Gizmos.color = gizmoIdleColor;
        //    if (spiderChase.PlayerDetected)
        //        Gizmos.color = gizmoDetectedColor;
        //    Gizmos.DrawCube((Vector2)wallCheck.position + detectorOriginOffset, detectorSize);
        //}
        Gizmos.color = gizmoIdleColor;
        if (PlayerDetected)
        {
            Debug.Log("Gizmo Drawn");
            Gizmos.color = gizmoDetectedColor;
        }
        Gizmos.DrawCube((Vector2)wallCheck.position + detectorOriginOffset, detectorSize);
    }   
    #endregion
}
#endregion