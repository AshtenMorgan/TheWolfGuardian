using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] 

public class GameData
{
    public Transform lastCheckPoint;
    public string lastCheckPointName;
    
    public GameData()
    {
        Debug.Log("New Game Data");
        this.lastCheckPoint = null;
        this.lastCheckPointName = null;
        Debug.Log("Initialized lastCheckPoint at " + lastCheckPoint);
    }

}
