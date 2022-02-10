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
    //public EnemyPawn enemy;

    #endregion

    #region Enemy objects for spawning/moving
    [Header("Prefabs"), Tooltip("These are the pre-build player and enemy objects")]
    public Object playerPrefab;
    public Object enemy1Prefab;
    public Object enemy2Prefab;
    public Object enemy3Prefab;
    public Object enemy4Prefab;
    public Object enemy5Prefab;
    public Object enemy6Prefab;
    public Object enemy7Prefab;
    #endregion

    #region Spawn Points
    [Header("Initial Spawn Points"), SerializeField, Tooltip("This is where the initial instantiated objects will be placed")]
    public Transform playerInstan;
    public Transform enemyInstan;
    [Header("Spawn Points"), SerializeField, Tooltip("All the places where the enemies or player will be spawned")]
    public Transform playerSpawn;
    public Transform[] enemy1Spawn;
    public Transform[] enemy2Spawn;
    public Transform[] enemy3Spawn;
    public Transform[] enemy4Spawn;
    public Transform[] enemy5Spawn;
    public Transform[] enemy6Spawn;
    public Transform[] enemy7Spawn;
    #endregion

    #region Spawn Numbers
    [Header("Max amount of spawns"), SerializeField, Tooltip("The number of each object that will be generated on load")]
    public int enemy1Max;
    public int enemy2Max;
    public int enemy3Max;
    public int enemy4Max;
    public int enemy5Max;
    public int enemy6Max;
    public int enemy7Max;
    #endregion

    #region Spawn Timing
    [Header("Timers"), SerializeField, Tooltip("Time delay between spawns")]
    public float playerSpawnDelay;
    public float _nextPlayerSpawn;//private after tests
    public float enemySpawnDelay;
    public float _nextEnemySpawn;//private after tests
    public float current;//Only for tests
    public float buffSpawnDelay;
    private float _nextBuffSpawn;//private after tests
    public float debuffSpawnDelay;
    private float _nextDebuffSpawn;//private after tests
    #endregion
    #region instance
    public static GameManager Instance { get; private set; }//allow other classes to access GM
    #endregion

    [Header("Game Over tracker"), SerializeField, Tooltip("Tracks weather or not a game over has occured")]
    private bool gameOver;

    [SerializeField]
    private Scene _scene;
    #endregion

    #region Functions

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

        VarCheck();
    }

    private void FixedUpdate()
    {
        VarCheck();         //make sure variables are still set
        CheckSpawn();       //see if it is time to spawn player
        //CheckEnemySpawn();  //checking if we should spawn an enemy.

        current = Time.time;//for testing purposes  delete after tests are complete
    }

    public void VarCheck()
    {
        if (!player)
        {
            playerPrefab = Resources.Load("Prefabs/Pawn Prefabs/Player Prefabs/Ashlynn");
            playerSpawn = GameObject.FindGameObjectWithTag("PlayerSpawnPoint").transform;
            playerInstan = GameObject.FindGameObjectWithTag("PlayerInstantiate").transform;

            //player
            player = (GameObject)Instantiate(playerPrefab, playerInstan.position, playerInstan.rotation) as GameObject;//spawn player
            Player = player.GetComponent<PlayerPawn>();
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
                    else//should only get here if game over is true
                    {
                        GameOver();//run game over
                    }

                    _nextPlayerSpawn = Time.time + playerSpawnDelay;//update spawn timer
                }
            }
        }

    }

    public void SpawnPlayer()
    {
        if (Player != null)//make sure there is a player
        {
            Health healthReset = player.GetComponent<Health>();//store health component
            player.transform.SetPositionAndRotation(playerSpawn.transform.position, playerSpawn.transform.rotation);//Set player position/rotation
            healthReset.FullHeal();//return player to max health
            //return current health to max value
            player.gameObject.SetActive(true);//Appear the player
            Player.Lives--;//decrement lives
        }
    }
    #endregion
    /*
    #region Enemy Spawn Checks
    void CheckEnemySpawn()
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
            healthReset.Heal(enemy.MaxHealth);//restore health to max
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
            healthReset.Heal(enemy.MaxHealth);//restore health to max
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
            healthReset.Heal(enemy.MaxHealth);//restore health to max
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
            healthReset.Heal(enemy.MaxHealth);//restore health to max
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
            healthReset.Heal(enemy.MaxHealth);//restore health to max
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
            healthReset.Heal(enemy.MaxHealth);//restore health to max
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
            healthReset.Heal(enemy.MaxHealth);//restore health to max
            spawnedEnemy7.SetActive(true);

        }

    }
    #endregion
    */
    #region NYI
    //function for pause
    public void Pause()
    {
        //pause game
    }

    //resume after pause
    public void Unpause()
    {
        //resume
    }

    public void QuitGame()
    {
        Application.Quit();//quit game

        //This only runs if we are running inside of Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//stop editor
#endif
    }

    //handle game over
    public void GameOver()
    {

    }

    //continue after a game over
    public void GameOverResume()
    {

    }
    #endregion

    #endregion
}