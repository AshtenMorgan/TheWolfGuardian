using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    private String DataDirPath = "";

    private String DataFileName = "";

    public FileDataHandler(string dataDirPath, string dataFileName)
    {
        this.DataDirPath = dataDirPath;
        this.DataFileName = dataFileName;
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(DataDirPath, DataFileName);

        GameData LoadedData = null;

        if(File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using(StreamReader reader = new StreamReader(stream))
                    {
                        //TODO:  Loaded popup
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                LoadedData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch (Exception e)
            {
                Debug.LogError("Error while trying to load data from " + fullPath + "\n" + e);
            }
        }
        return LoadedData;
    }

    public void Save(GameData Data)
    {
        string fullPath = Path.Combine(DataDirPath, DataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string DataToStore = JsonUtility.ToJson(Data, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {

                using(StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(DataToStore);
                }

            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error while trying to save to " + fullPath + "\n" + e);
        }
    }

}
