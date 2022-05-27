using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEntityData", menuName = "Data/Entity Data/Base Data")]
public class Data_Entity : ScriptableObject
{
    public float wallCheckDistance = 0.1f,
        ledgeCheckDistance = 0.5f,
        viewDistance = 10.0f;


    public LayerMask whatIsGround,
        whatIsPlayer;
}
