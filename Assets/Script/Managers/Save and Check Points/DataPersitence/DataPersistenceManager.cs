using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;
    //An Instance of GameData
    private GameData GameData;

    private List<IDataPersistence> IDataPersistenceObjects;

    private FileDataHandler FileDataHandler;

    //Makes one Peristence Manager
    public static DataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        //Checks to see if a Manager already exists
        if (instance != null)
        {
        }

        //Else it creates a new one
        instance = this;

    }

    public void Start()
    {
        this.FileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.IDataPersistenceObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    //Creates a new Instance of Game Data to start a new Game
    public void NewGame()
    {
        //TODO: New Game Popup
        this.GameData = new GameData();
    }

    //Loads previous data of a game
    public void LoadGame()
    {
        this.GameData = FileDataHandler.Load();

        if (this.GameData == null)
        {
            NewGame();
        }

        foreach (IDataPersistence dataPersitentObj in IDataPersistenceObjects)
        {
            dataPersitentObj.LoadData(GameData);
        }
    }

    //Saves the game
    public void SaveGame()
    {
        foreach (IDataPersistence dataPersitentObj in IDataPersistenceObjects)
        {
            dataPersitentObj.SaveData(ref GameData);
        }

        FileDataHandler.Save(GameData);
    }

    private List<IDataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<IDataPersistence> dataPersistenceObjects = FindObjectsOfType<MonoBehaviour>().OfType<IDataPersistence>();
        IEnumerable<IDataPersistence> controllerDataPersistenceObjects = FindObjectsOfType<Controller>().OfType<IDataPersistence>();

        return new List<IDataPersistence>(dataPersistenceObjects);

    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }


}
