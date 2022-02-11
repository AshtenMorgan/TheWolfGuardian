using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPawn : Pawn
{
    #region Variables
    [Header("General Player Attributes"), SerializeField, Tooltip("The current amount of points in the combo meter")]
    protected float comboMeterCurrent; //this is the current combo meter count, it should initialize at 0.
    [SerializeField, Tooltip("The maximum amount of points needed to fill the combo meter")]
    protected float comboMeterMax; //the maximum amount of points needed to fill the combo meter
    [SerializeField, Tooltip("The amount of extra lives the player has")]
    protected int _lives = 4; //stores the amount of extra lives the player has
    [SerializeField, Tooltip("The max amount of extra lives the player has")]
    protected int _maxLives = 5;

    protected bool shapeShifted; //the boolean for deciding of the player is shape shifted into wolf form or not
    protected bool _isSprinting; //the boolean determining if the player is sprinting or not
    protected bool swordEquipped; //the boolean for deciding whether the player is holding their sword or not


    #region Full Properties
    public int Lives
    {
        get { return _lives; }
        set { _lives = value; }
    }

    public bool IsSprinting 
    {
        get { return _isSprinting; }
        set { _isSprinting = value; }
    }

    #endregion



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