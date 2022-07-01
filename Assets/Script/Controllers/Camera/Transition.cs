using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Cinemachine.Utility;

public class Transition : RoomManager
{   public CinemachineConfiner confiner;


    public override void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            StartCoroutine(UpdateConfiner(0.1f));
        }
        
    }
    
    IEnumerator UpdateConfiner(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        confiner.m_BoundingShape2D = gm.currentRoom;
    }
}
