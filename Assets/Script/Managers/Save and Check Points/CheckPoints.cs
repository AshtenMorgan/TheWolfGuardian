using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{

    public string CheckpointName;
    public Transform CheckpointLocation;
    private void Start()
    {
        CheckpointLocation = gameObject.transform;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Ashlynn(Clone)")
        {
            GameManager.Instance.lastCheckPoint = CheckpointLocation;
            GameManager.Instance.lastCheckPointName = CheckpointName;
        }
    }

}
