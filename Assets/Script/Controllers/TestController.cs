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
    protected Rigidbody2D rb2d; //the rigidbody of the test character
    protected PlayerPawn pawn; //the PlayerPawn class so that we can access the player characters movement data
    protected int verticalVelocity; //the vertical acceleration of the player pawn
    #endregion

    #region Functions
    protected virtual void Start()
    {
        pawn = GetComponent<PlayerPawn>(); //defines the pawn needed for all test character stats
        rb2d = GetComponent<Rigidbody2D>(); //defines the Gameobjects needed for the test character
    }


    public virtual void Jump()
    {
        verticalVelocity = pawn.JumpHeight;
        Debug.Log("Vertical Velocity is: " + verticalVelocity);
        Debug.Log("Jump Height is: " + pawn.JumpHeight);
        rb2d.velocity = Vector2.up * verticalVelocity;
        Debug.Log("Jumped!");
    }
    
    #endregion
}
