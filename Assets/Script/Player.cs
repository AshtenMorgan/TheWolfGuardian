using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Pawn
{
    [SerializeField, Tooltip("Current combo count")]
    private float comboMeterCurrent;

    [SerializeField, Tooltip("Maximum count for combo meter")]
    private float comboMeterMax;

    [SerializeField, Tooltip("Player lives remaining")]
    private float lives;

    [SerializeField, Tooltip("Is the player shapeshifted?")]
    private bool shapeShifted;

    [SerializeField, Tooltip("Is the sword equipped?")]
    private bool swordEquipped;



    // Start is called before the first frame update
    public override void Start()
    {

    }

    // Update is called once per frame
    public override void Update()
    {

    }

}