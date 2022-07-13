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
    [Header("Distances")]
    [Tooltip("How close to a wall entity will get")] public float wallCheckDistance = 0.1f;
    [Tooltip("Distance to look down for ground")] public float ledgeCheckDistance = 0.5f;
    [Tooltip("Player detection distance")] public float viewDistance = 5.0f;
    [Tooltip("Melee attack distance")] public float melee = 1;
    [Tooltip("Ranged attack distance")] public float ranged = 3;
    [Tooltip("Stopping distance")] public float stop = 0.5f;

    public LayerMask whatIsGround,
        whatIsPlayer;
}
