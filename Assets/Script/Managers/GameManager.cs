using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Cinemachine;
using Cinemachine.Utility;

public class GameManager : MonoBehaviour
{
    #region Variables

    #region Player/enemy Object
    [Header("Objects"), SerializeField, Tooltip("Drag prefabs onto these")]
    public PlayerPawn Player;
    public GameObject player;//Delete this one later
    public Health playerHealth;
    #region Hitboxes
    public GameObject hitACollider; //stores colliders for HitA
    #endregion
    public EnemyPawn[] enemy;

    #endregion

    #region Enemy objects for spawning/moving
    [Header("Prefabs"), Tooltip("These are the pre-build player and enemy objects")]
    public Object playerPrefab;

    #endregion

    #region Spawn Points
    [Header("Initial Spawn Point"), SerializeField, Tooltip("This is where the initial instantiated objects will be placed")]
    public Transform instanPoint;
    [Header("Spawn Points"), SerializeField, Tooltip("All the places where the enemies or player will be spawned")]
    public Transform playerSpawn;
    
    public CompositeCollider2D room1;
    public CinemachineConfiner confiner;
    #endregion

    
   
    #region instance
    public static GameManager Instance { get; private set; }//allow other classes to access GM
    #endregion
    #region PlayerVars
    public int lives;
    public float percent;
    public float maxHealth;
    public float currentHealth;
    #endregion

    [Header("Game Over tracker"), SerializeField, Tooltip("Tracks weather or not a game over has occured")]
    public bool gameOver;

    [SerializeField]
    public Scene scene;
    //private string mainMenu = "James Test"; //name of the main menu scene as a string
    #endregion


    #region Functions

    private void OnEnable()
    {
        //check what scene and run scenloaded functions.  This should load every time a scene change occurs
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene loaded, LoadSceneMode mode)
    {
        scene = loaded;//update what scene we are in

        if (scene.name != "MainMenu")
        {
            GameSettings.Instance.activeMenu = ActiveMenu.Game;
            VarCheck();

        }
        else if (scene.name == "MainMenu")
        {
            
        }

    }

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

    // Use this for initialization
    private void Start()
    {
        VarCheck();
    }

    // Update is called once per frame
    private void Update()
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
        if (scene.name != "MainMenu")//make sure we should have stuff
        {
            UpdateHealthBar();
            CheckSpawn();       //see if it is time to spawn player  //maybe trigger this on death
        }
    }

    public void VarCheck()
    {
        //set objects
        playerPrefab = Resources.Load("Prefabs/Pawn Prefabs/Player Prefabs/Ashlynn");
        //instantiation point
        instanPoint = GameObject.FindGameObjectWithTag("InstanPoint").transform;
        //spawn points
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawnPoint").gameObject.transform.GetChild(0);

        //set up player
        if (player == null)
        {
            player = (GameObject)Instantiate(playerPrefab, instanPoint.position, instanPoint.rotation) as GameObject;//spawn player
            Player = player.GetComponent<PlayerPawn>();
            playerHealth = Player.GetComponent<Health>();
            lives = Player.Lives;
            hitACollider = GameObject.FindGameObjectWithTag("HitA");
            player.gameObject.SetActive(false);//everything is inactivated on initial spawn
        }

    }
    #region Player Spawning
    void CheckSpawn()
    {
        if (player != null)
            if (player.activeInHierarchy != true)
                    if (gameOver == false)//no player active and it is not game over
                        SpawnPlayer();//run player spawn function
    }

    public void SpawnPlayer()
    {
        if (lives > 0)
        {
            player.transform.SetPositionAndRotation(playerSpawn.transform.position, playerSpawn.transform.rotation);//Set player position/rotation
            playerHealth.Respawn();//return player to max health

            //return current health to max value
            player.gameObject.SetActive(true);//Appear the player
            Player.Lives--;//decrement lives
            lives = Player.Lives;//track how many lives
        }
        else
        {
            GameOver();
        }

    }
    public void ResetSpawn()
    {
        confiner = FindObjectOfType<CinemachineConfiner>();
        room1 = GameObject.FindGameObjectWithTag("Room1").GetComponent<CompositeCollider2D>();

        playerSpawn.position = playerSpawn.parent.position;
        confiner.m_BoundingShape2D = room1;
    }
    #endregion
    public void UpdateHealthBar()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != "MainMenu")
        {
            currentHealth = playerHealth.currentHealth;
            maxHealth = playerHealth.maxHealth;
            percent = currentHealth / maxHealth;
        }
        else
        {
            return;
        }
    }
    //handle game over
    public void GameOver()
    {
        Time.timeScale = 0.0f;//stop time;//stop time
        //UIManager.Instance.EnableGameOverMenu();//show gameover
    }


    public void OnDisable()
    {
        //just because it is proper to unsub delegates
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    #endregion
}