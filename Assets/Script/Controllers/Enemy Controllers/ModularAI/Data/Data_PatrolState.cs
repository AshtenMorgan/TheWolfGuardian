/*
 * Data object for Patrol State
 * This holds variables used
 * while entity is in the patrol state
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NewPatrolStateData", menuName = "Data/State Data/PatrolState Data")]
public class Data_PatrolState : ScriptableObject
{
    public Transform[] waypoints;
    public float walkSpeed = 5.0f;
}
