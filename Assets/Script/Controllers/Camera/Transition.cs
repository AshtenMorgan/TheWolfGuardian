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
        StartCoroutine(UpdateSpawn(0.2f));

        IEnumerator UpdateSpawn(float seconds)
        {

            yield return new WaitForSeconds(seconds);
            GameManager.Instance.playerSpawn.transform.position = GameManager.Instance.player.transform.position;
        }

    }
}
