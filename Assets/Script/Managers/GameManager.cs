using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Variables

    #region Player/enemy Object
    [Header("Objects"), SerializeField, Tooltip("Drag prefabs onto these")]
    public PlayerPawn Player;
    public GameObject player;
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
    public Transform[] enemy1Spawn,
        enemy2Spawn,
        enemy3Spawn,
        enemy4Spawn,
        enemy5Spawn,
        enemy6Spawn,
        enemy7Spawn;
    #endregion

    #region Spawn Numbers
    [Header("Max amount of spawns"), SerializeField, Tooltip("The number of each object that will be generated on load")]
    public int enemy1Max;
    public int enemy2Max,
        enemy3Max,
        enemy4Max,
        enemy5Max,
        enemy6Max,
        enemy7Max;
    #endregion

    #region Spawn Timing
    [Header("Timers"), SerializeField, Tooltip("Time delay between spawns")]
    private float _nextEnemySpawn,//the time when the next spawn will occur
        _nextPlayerSpawn,
        _nextBuffSpawn,
        _nextDebuffSpawn,
        //delay timers
        playerSpawnDelay,
        enemySpawnDelay,
        debuffSpawnDelay,
        buffSpawnDelay,
        //current time
        current;

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
    private string mainMenu = "James Test"; //name of the main menu scene as a string
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

        if (scene.name != mainMenu)
        {
          ObjectPool.instance.PoolSetup();
          VarCheck();
            
        }
        else if (scene.name == mainMenu)
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
        ObjectPool.instance.PoolSetup();
    }

    // Update is called once per frame
    private void Update()
    {
        if (scene.name != mainMenu)//make sure we should have stuff
        {
            current = Time.time;//for testing purposes  delete after tests are complete
            CheckSpawn();       //see if it is time to spawn player  //maybe trigger this on death
            CheckEnemySpawn();  //checking if we should spawn an enemy.
            UpdateHealthBar();
        }
        
    }

    private void FixedUpdate()
    {

    }

    public void VarCheck()
    {

        //set objects
        playerPrefab = Resources.Load("Prefabs/Pawn Prefabs/Player Prefabs/Ashlynn");
        //instantiation point
        instanPoint = GameObject.FindGameObjectWithTag("InstanPoint").transform;
        //spawn points
        playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawnPoint").transform;
        //set these to individual spawn points once we have them set up, for now they all default to 1 enemy spawn point
        if (enemy1Spawn.Length > 0)
        {
            enemy1Spawn[0] = GameObject.FindGameObjectWithTag("EnemySpawn1").transform;
        }
        if (enemy2Spawn.Length > 0)
        {
            enemy2Spawn[0] = GameObject.FindGameObjectWithTag("EnemySpawn2").transform;
        }
        if (enemy3Spawn.Length > 0)
        {
            enemy3Spawn[0] = GameObject.FindGameObjectWithTag("EnemySpawn3").transform;
        }
        if (enemy4Spawn.Length > 0)
        {
            enemy4Spawn[0] = GameObject.FindGameObjectWithTag("EnemySpawn4").transform;
        }
        if (enemy5Spawn.Length > 0)
        {
            enemy5Spawn[0] = GameObject.FindGameObjectWithTag("EnemySpawn5").transform;
        }
        if (enemy6Spawn.Length > 0)
        {
            enemy6Spawn[0] = GameObject.FindGameObjectWithTag("EnemySpawn6").transform;
        }
        if (enemy7Spawn.Length > 0)
        {
            enemy7Spawn[0] = GameObject.FindGameObjectWithTag("EnemySpawn7").transform;
        }
        
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
        {
            if (player.activeInHierarchy != true)
            {
                if (Time.time > _nextPlayerSpawn)//check time against spawn delay
                {
                    if (gameOver == false)//no player active and it is not game over
                    {
                        SpawnPlayer();//run player spawn function
                    }

                    _nextPlayerSpawn = Time.time + playerSpawnDelay;//update spawn timer
                }
            }
        }
        else
        {
            
        }

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
    #endregion
    
    #region Enemy Spawn Checks
    public void CheckEnemySpawn()
    {
        
        if (Time.time > _nextEnemySpawn)//check spawn timer
        {

            #region Spawning
            if (!ObjectPool.instance.Enemy1.activeInHierarchy)//check for active enemies
            {
                SpawnEnemy1();//spawn enemies
            }
            if (!ObjectPool.instance.Enemy2.activeInHierarchy)
            {
                SpawnEnemy2();
            }
            if (!ObjectPool.instance.Enemy3.activeInHierarchy)
            {
                SpawnEnemy3();
            }
            if (!ObjectPool.instance.Enemy4.activeInHierarchy)
            {
                SpawnEnemy4();
            }
            if (!ObjectPool.instance.Enemy5.activeInHierarchy)
            {
                SpawnEnemy5();
            }
            if (!ObjectPool.instance.Enemy6.activeInHierarchy)
            {
                SpawnEnemy6();
            }
            if (!ObjectPool.instance.Enemy7.activeInHierarchy)
            {
                SpawnEnemy7();
            }

            _nextEnemySpawn = Time.time + enemySpawnDelay;//update spawn timer
        
        }
        #endregion
    }
    #endregion
    #region Enemy Spawn Functions
    public void SpawnEnemy1()
    {
        GameObject spawnedEnemy1 = ObjectPool.instance.GetEnemy1Pool();//check spawn pool for inactive enemies
        if (spawnedEnemy1 != null)//if inactive enemies exist in pool
        {
            EnemyPawn enemy = spawnedEnemy1.GetComponent<EnemyPawn>();//get pawn component from enemy
            spawnedEnemy1.transform.SetPositionAndRotation(enemy1Spawn[0].transform.position, enemy1Spawn[0].transform.rotation);//spawn with rotation
            Health healthReset = spawnedEnemy1.GetComponent<Health>();//store health component
            healthReset.Respawn();//restore health to max
            spawnedEnemy1.SetActive(true);//activate enemy


        }
    }
    public void SpawnEnemy2()
    {
        GameObject spawnedEnemy2 = ObjectPool.instance.GetEnemy2Pool();
        if (spawnedEnemy2 != null)
        {
            EnemyPawn enemy = spawnedEnemy2.GetComponent<EnemyPawn>();
            spawnedEnemy2.transform.SetPositionAndRotation(enemy2Spawn[0].transform.position, enemy2Spawn[0].transform.rotation);
            Health healthReset = spawnedEnemy2.GetComponent<Health>();//store health component
            healthReset.Respawn();//restore health to max
            spawnedEnemy2.SetActive(true);
        }

    }
    public void SpawnEnemy3()
    {
        GameObject spawnedEnemy3 = ObjectPool.instance.GetEnemy3Pool();
        if (spawnedEnemy3 != null)
        {
            EnemyPawn enemy = spawnedEnemy3.GetComponent<EnemyPawn>();
            spawnedEnemy3.transform.SetPositionAndRotation(enemy3Spawn[0].transform.position, enemy3Spawn[0].transform.rotation);
            Health healthReset = spawnedEnemy3.GetComponent<Health>();//store health component
            healthReset.Respawn();//restore health to max
            spawnedEnemy3.SetActive(true);

        }
    }
    public void SpawnEnemy4()
    {
        GameObject spawnedEnemy4 = ObjectPool.instance.GetEnemy4Pool();
        if (spawnedEnemy4 != null)
        {
            EnemyPawn enemy = spawnedEnemy4.GetComponent<EnemyPawn>();
            spawnedEnemy4.transform.SetPositionAndRotation(enemy4Spawn[0].transform.position, enemy4Spawn[0].transform.rotation);
            Health healthReset = spawnedEnemy4.GetComponent<Health>();//store health component
            healthReset.Respawn();//restore health to max
            spawnedEnemy4.SetActive(true);

        }

    }
    public void SpawnEnemy5()
    {
        GameObject spawnedEnemy5 = ObjectPool.instance.GetEnemy5Pool();
        if (spawnedEnemy5 != null)
        {
            EnemyPawn enemy = spawnedEnemy5.GetComponent<EnemyPawn>();
            spawnedEnemy5.transform.SetPositionAndRotation(enemy5Spawn[0].transform.position, enemy5Spawn[0].transform.rotation);
            Health healthReset = spawnedEnemy5.GetComponent<Health>();//store health component
            healthReset.Respawn();//restore health to max
            spawnedEnemy5.SetActive(true);

        }

    }
    public void SpawnEnemy6()
    {
        GameObject spawnedEnemy6 = ObjectPool.instance.GetEnemy6Pool();
        if (spawnedEnemy6 != null)
        {
            EnemyPawn enemy = spawnedEnemy6.GetComponent<EnemyPawn>();
            spawnedEnemy6.transform.SetPositionAndRotation(enemy6Spawn[0].transform.position, enemy6Spawn[0].transform.rotation);
            Health healthReset = spawnedEnemy6.GetComponent<Health>();//store health component
            healthReset.Respawn();//restore health to max
            spawnedEnemy6.SetActive(true);

        }

    }
    public void SpawnEnemy7()
    {
        GameObject spawnedEnemy7 = ObjectPool.instance.GetEnemy7Pool();
        if (spawnedEnemy7 != null)
        {
            EnemyPawn enemy = spawnedEnemy7.GetComponent<EnemyPawn>();
            spawnedEnemy7.transform.SetPositionAndRotation(enemy7Spawn[0].transform.position, enemy7Spawn[0].transform.rotation);
            Health healthReset = spawnedEnemy7.GetComponent<Health>();//store health component
            healthReset.Respawn();//restore health to max
            spawnedEnemy7.SetActive(true);
        }

    }
    #endregion
   
    public void UpdateHealthBar()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name != mainMenu)
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
        UIManager.Instance.EnableGameOverMenu();//show gameover
    }

    
    public void OnDisable()
    {
        //just because it is proper to unsub delegates
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    #endregion
}