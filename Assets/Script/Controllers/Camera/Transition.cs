using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cinemachine.Utility;

public class Transition : MonoBehaviour
{   [Header("Rooms to transition")]
    public CompositeCollider2D from,
        to;
    public CinemachineConfiner confiner;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(confiner.m_BoundingShape2D == from)
        {
            confiner.m_BoundingShape2D = to;
        }
        else
        {
            confiner.m_BoundingShape2D = from;
        }
    }


}
