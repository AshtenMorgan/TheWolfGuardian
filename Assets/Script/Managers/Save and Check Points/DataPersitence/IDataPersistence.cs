using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDataPersistence 
{

    void LoadData(GameData Data);

    void SaveData(ref GameData Data);

}
