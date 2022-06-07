/*
 * Data object for all Enemies
 * This holds variables used
 * for all or most enemies
 * not including health
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEntityData", menuName = "Data/Entity Data/Base Data")]
public class Data_Entity : ScriptableObject
{
    public float wallCheckDistance = 0.1f,
        ledgeCheckDistance = 0.5f,
        viewDistance = 5.0f;


    public LayerMask whatIsGround,
        whatIsPlayer;
}
