/*
 John Pope
 This is a test script for the player controller. It will be used to test various settings and functions with the new input system before implementing a new permanent player controller
this script should not be used outside of testing environments and it should not remain implemented outside of the testing phase for movement related to the player controller
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestController : MonoBehaviour
{

    #region Variables
    #region General Variables
    [Header("General Variables")]
    protected Rigidbody2D rb2d; //the rigidbody of the test character
    protected PlayerPawn pawn; //the PlayerPawn class so that we can access the player characters movement data
    [SerializeField]
    protected LayerMask groundLayer; //The layer mask for what is considered "the ground" in the game
    #endregion
    #region Jump Variables
    [Header("Jump Variables")]
    protected int verticalVelocity; //the vertical acceleration of the player pawn
    protected bool grounded; //determines if the pawn is touching the ground or not
    [SerializeField]
    protected Transform groundCheck; //the specified location that decides whether the pawn is touching the ground or not
    [SerializeField]
    protected float circleRadius; //determines the size of the capsule at the feet of the pawn
    #endregion
    #endregion

    #region Functions
    protected virtual void Start()
    {
        pawn = GetComponent<PlayerPawn>(); //defines the pawn needed for all test character stats
        rb2d = GetComponent<Rigidbody2D>(); //defines the Gameobjects needed for the test character
    }
    protected virtual void Update() 
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, circleRadius, groundLayer); //this update checks to see if the player is grounded
    }
    public virtual void Jump()
    {
        if (grounded) //only allows the player to jump if they're on the ground
        {
            verticalVelocity = pawn.JumpHeight; //sets the verticalVelocity variable equal to that of the protected variable jumpHeight on playerpawn
            Debug.Log("Vertical Velocity is: " + verticalVelocity); 
            Debug.Log("Jump Height is: " + pawn.JumpHeight);
            rb2d.velocity = Vector2.up * verticalVelocity; //applies velocity to the upward vector causing the character to jump
            Debug.Log("Jumped!");
        }
    }

    #endregion

    #region Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundCheck.position, circleRadius); //draws a sphere around our ground check empty so that we can visualize it
    }
    #endregion
}
