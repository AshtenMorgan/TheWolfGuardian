using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEntityData", menuName = "Data/Entity Data/Base Data")]
public class Data_Entity : ScriptableObject
{
    public float wallCheckDistance,
        ledgeCheckDistance;

    public LayerMask whatIsGround;
}
