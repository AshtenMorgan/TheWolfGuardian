using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPawn : Pawn
{
    #region Variables
    #region Enemy Combat Attributes
    [Header("Enemy Combat Attributes"), SerializeField, Tooltip("Current target of enemy")]
    protected Pawn target; //specifies the target of the enemy
    protected LayerMask playerLayer; //Specifies the layer this player occupies for enemy attacks.
    #endregion
    #region AI Behavior Attributes
    [HideInInspector]
    /// <summary>
    ///  States the AI is capable of transitioning into and being in, this is not yet implemented.
    /// </summary>
    public enum AIState { }; //this is a public enum because Unity didnt like it when we made it protected and tried to use a full property
    protected AIState _aiState; // the current state of the AI
    #endregion
    #endregion

    #region Full Properties
    public AIState State //the full property for this AI's state.
    {
        get
        { 
            return _aiState;
        }
        set
        {
            _aiState = value;  
        }
    }
    #endregion

    #region Functions
    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    // Update is called once per frame
    protected override void Update()
    {
    }
    #endregion
}
