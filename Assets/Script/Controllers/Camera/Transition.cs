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
            StartCoroutine(UpdateSpawn(0.2f));
            StartCoroutine(UpdateConfiner(0.1f));
        }
        
    }
    IEnumerator UpdateSpawn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GameManager.Instance.playerSpawn.transform.position = GameManager.Instance.player.transform.position;
    }
    IEnumerator UpdateConfiner(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        confiner.m_BoundingShape2D = gm.currentRoom;
    }
}
