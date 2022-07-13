using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cinemachine.Utility;

public class Hut : MonoBehaviour
{
    [Header("Rooms to transition")]
    public CompositeCollider2D setOne;
    public CinemachineConfiner confiner;


    private void OnTriggerEnter2D(Collider2D other)
    {  
            confiner.m_BoundingShape2D = setOne;
    }
}
