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
        this.lastCheckPoint = null;
        this.lastCheckPointName = null;
    }

}
