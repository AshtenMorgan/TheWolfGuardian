using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Pawn
{
    [SerializeField, Tooltip("Current combo count")]
    protected float comboMeterCurrent;

    [SerializeField, Tooltip("Maximum count for combo meter")]
    protected float comboMeterMax;

    [SerializeField, Tooltip("Player lives remaining")]
    protected float lives;

    [SerializeField, Tooltip("Is the player shapeshifted?")]
    protected bool shapeShifted;

    [SerializeField, Tooltip("Is the sword equipped?")]
    protected bool swordEquipped;



    // Start is called before the first frame update
    protected override void Start()
    {

    }

    // Update is called once per frame
    protected override void Update()
    {

    }

}