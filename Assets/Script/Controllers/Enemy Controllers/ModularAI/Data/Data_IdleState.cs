using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewIdleStateData", menuName = "Data/State Data/IdleState Data")]
public class Data_IdleState : ScriptableObject
{
    public float
        minIdleTime,
        maxIdleTime;
}
