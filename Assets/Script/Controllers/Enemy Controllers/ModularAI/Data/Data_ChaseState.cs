/*
 * Data object for Chase State
 * This holds variables used
 * while entity is in the chase state
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewChaseStateData", menuName = "Data/State Data/ChaseState Data")]
public class Data_ChaseState : ScriptableObject
{
    public float runSpeed = 10.0f;
}
