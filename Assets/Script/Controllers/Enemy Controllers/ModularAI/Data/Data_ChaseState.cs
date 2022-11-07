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
    //Spider Run Speed
    public float runSpeed = 10.0f;

    //Redcap Run Speed
    public float redRunSpeed = 15.0f;
}
