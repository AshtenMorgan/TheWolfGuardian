using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Cinemachine;
using Cinemachine.Utility;

public class GameManager : MonoBehaviour, IDataPersistence
{
    #region Variables

    #region Player Object
    [Header("Objects"), Tooltip("Drag prefabs onto these")]
    public PlayerPawn player;
    [Header("Prefabs"), Tooltip("These are the pre-build player and enemy objects")]
    public Object playerPrefab;
    #endregion
    #region Hitboxes
    public GameObject hitACollider; //stores colliders for HitA
    #endregion
    #region Spawn Points
    [Header("Initial Spawn Point"), Tooltip("This is where the initial instantiated objects will be placed")]
    public Transform instanPoint;
    [Header("Spawn Points"), Tooltip("All the places where the enemies or player will be spawned")]
    public Transform playerSpawn;
    [Header("Checkpoint Respawn Point"), Tooltip("This is the last checkpoint the player Passed")]
    public Transform lastCheckPoint;

    public string lastCheckPointName;
    #endregion
    #region Camera Control
    public CompositeCollider2D room1,
        currentRoom;
    public CinemachineConfiner confiner;
    public CinemachineVirtualCamera cam;
    public Camera cameraMain;
    #endregion
    #region MiniMap
    public BoxCollider2D currentMap;
    #endregion
    #region instance
    public static GameManager Instance { get; private set; }//allow other classes to access GM
    #endregion
    #region PlayerVars
    public Health playerHealth;
    public int lives;
    public float percent;
    public float maxHealth;
    public float currentHealth;
    public InputRecorder playerRecorder;
    #endregion

    [Header("Game Over tracker"), Tooltip("Tracks weather or not a game over has occured")]
    public bool gameOver;

    public Scene scene;
    #endregion

    //DO NOT PUT ANYTHING ABOVE OR BETWEEN UNITY EVENTS, THEY SHOULD STAY IN ORDER OF EXECUTION!!!!
    #region Unity Events
    //Singleton  only one instance
    private void Awake()
    {
        //make sure there is always only 1 instance
        if (GameManager.Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void OnEnable()
    {
        //check what scene and run scenloaded functions.  This should load every time a scene change occurs
        SceneManager.sceneLoaded += OnSceneLoaded;
        cameraMain = Camera.main;
    }

    private void Start()
    {
        if (lastCheckPoint == null)
        {
            lastCheckPoint = playerSpawn;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameSettings.Instance != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {

                switch (GameSettings.Instance.activeMenu)
                {
                    case ActiveMenu.Game:
                        GameSettings.Instance.PauseUnpause();
                        break;

                    case ActiveMenu.Pause:
                        GameSettings.Instance.PauseUnpause();
                        break;

                    default:
                        break;
                }
            }
        }

        if (scene.name != "MainMenu")//make sure we should have stuff
        {
            CheckSpawn();       //see if it is time to spawn player  //maybe trigger this on death
        }
    }
    #endregion











    #region SaveData
    public void LoadData(GameData Data)
    {
        lastCheckPoint = Data.lastCheckPoint;
        lastCheckPointName = Data.lastCheckPointName;
    }

    public void SaveData(ref GameData Data)
    {
        Data.lastCheckPoint = lastCheckPoint;
        Data.lastCheckPointName = lastCheckPointName;
    }

    #endregion
    #region Functions

   

    

    private void OnSceneLoaded(Scene loaded, LoadSceneMode mode)
    {
        scene = loaded;//update what scene we are in
        if(GameSettings.Instance != null)
        {
            if (scene.name != "MainMenu")
            {
                GameSettings.Instance.activeMenu = ActiveMenu.Game;
                VarCheck();

            }
            else if (scene.name == "MainMenu")
            {

            }
        }
        else
            VarCheck();
    }

    

    
    public void VarCheck()
    {
        //set objects
        playerPrefab = Resources.Load("Prefabs/Pawn Prefabs/Player Prefabs/Ashlynn");
        //instantiation point
        instanPoint = GameObject.FindGameObjectWithTag("InstanPoint").transform;
        //spawn points
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawnPoint").transform.GetChild(0);
        //vcam
        confiner = GameObject.FindWithTag("Vcam").GetComponent<CinemachineConfiner>();
        room1 = GameObject.FindWithTag("Room1").GetComponent<CompositeCollider2D>();

        //set up player
        if (player == null)
        {
            player = Instantiate(playerPrefab, instanPoint.position, instanPoint.rotation) as PlayerPawn;//spawn player
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPawn>();
            playerHealth = player.GetComponent<Health>();
            hitACollider = GameObject.FindGameObjectWithTag("HitA");
            player.gameObject.SetActive(false);//everything is inactivated on initial spawn
        }

    }
    #region Player Spawning
    void CheckSpawn()
    {
        if (player != null)
            if (player.gameObject.activeInHierarchy != true)
                    if (gameOver == false)//no player active and it is not game over
                        SpawnPlayer();//run player spawn function
    }

    public void SpawnPlayer()
    {
        if (player.Lives > 0)
        {
            player.transform.SetPositionAndRotation(lastCheckPoint.transform.position, lastCheckPoint.transform.rotation);//Set player position/rotation
            playerHealth.Respawn();//return player to max health

            //return current health to max value
            player.gameObject.SetActive(true);//Appear the player
            player.Lives--;//decrement lives
            lives = player.Lives;//track how many lives
            playerRecorder = player.GetComponent<InputRecorder>();
            confiner.m_BoundingShape2D = currentRoom.GetComponent<PolygonCollider2D>();

            if (GameSettings.Instance != null)
            {
                UpdateHealthBar();
            }
            
        }
        else
        {
            GameOver();
        }
        
    }
    public void ResetSpawn()
    {
        player.Lives = 4;

        confiner = FindObjectOfType<CinemachineConfiner>();
        room1 = GameObject.FindGameObjectWithTag("Room1").GetComponent<CompositeCollider2D>();

        playerSpawn.position = playerSpawn.parent.position;
        confiner.m_BoundingShape2D = room1;
        
        SpawnPlayer();
    }
    #endregion
    public void UpdateHealthBar()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != "MainMenu")
        {
            currentHealth = playerHealth.CurrentHealth;
            maxHealth = playerHealth.MaxHealth;
            percent = currentHealth / maxHealth;
            GameSettings.Instance.healthBarSlider.value = percent;
        }
        else
        {
            return;
        }
    }
    //handle game over
    public void GameOver()
    {
        if (GameSettings.Instance != null)
        {
            Time.timeScale = 0.0f;//stop time
            GameSettings.Instance.SelectMenu("GameOverCanvas");
        }
    }
        


    public void OnDisable()
    {
        //just because it is proper to unsub delegates
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    #endregion
}