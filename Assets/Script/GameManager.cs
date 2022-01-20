using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    #region Variables

    #region Player/enemy Object
    [Header("Objects"), SerializeField, Tooltip("Drag prefabs onto these")]
    public PlayerPawn player;
    public EnemyPawn enemy;
    
    #endregion

    #region Enemy objects for spawning/moving
    [Header("Prefabs"), SerializeField, Tooltip("These are the pre-build player and enemy objects")]
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
    public int enemy1Max;
    public int enemy2Max;
    public int enemy3Max;
    public int enemy4Max;
    public int enemy5Max;
    public int enemy6Max;
    public int enemy7Max;
    #endregion

   

    #region Spawn Timing
    public float playerSpawnDelay;
    private float nextPlayerSpawn;
    public float enemySpawnDelay;
    private float nextEnemySpawn;
    public float buffSpawnDelay;
    private float nextBuffSpawn;
    public float debuffSpawnDelay;
    private float nextDebuffSpawn;
    #endregion

    public static GameManager instance { get; private set; }//allow other classes to access GM

    private bool gameOver;
    #endregion



    //Singleton  only one instance
    private void Awake()
    {
        //make sure there is always only 1 instance
        if (GameManager.instance == null)
        {
            instance = this;
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
       

        

        

    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.time > nextPlayerSpawn)
        {
            if (!ObjectPool.instance.Player.activeInHierarchy && gameOver == false)//no player found and it is not game over
            {
                SpawnPlayer();//run player spawn function
                player = FindObjectOfType<PlayerPawn>();
            }
            else if (ObjectPool.instance.Player.activeInHierarchy)//player is active
            {
                return;//do nothing
            }
            else//should only get here if game over is true
            {
                GameOver();//run game over
            }
            nextPlayerSpawn += Time.time;
        }
    }

    private void FixedUpdate()
    {
        if (Time.time > nextEnemySpawn)
        {


            if (!ObjectPool.instance.Enemy1.activeInHierarchy)
            {
                SpawnEnemy1();
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
            nextEnemySpawn += Time.time;
        }
    }

    public void SpawnPlayer()
    {
        if (ObjectPool.instance.Player != null)
        {
            player = ObjectPool.instance.Player.GetComponent<PlayerPawn>();
            ObjectPool.instance.Player.transform.position = playerSpawn.transform.position;//move player
            ObjectPool.instance.Player.transform.rotation = playerSpawn.transform.rotation;//rotate player
            ObjectPool.instance.Player.SetActive(true);//activate player
            player.Lives = player.Lives - 1;
        }



    }
    public void SpawnEnemy1()
    {
        GameObject spawnedEnemy1 = ObjectPool.instance.GetEnemy1Pool();
        if (spawnedEnemy1 != null)
        {
            spawnedEnemy1.transform.position = enemy1Spawn[0].transform.position;
            spawnedEnemy1.transform.rotation = enemy1Spawn[0].transform.rotation;
            spawnedEnemy1.SetActive(true);

        }
    }
    public void SpawnEnemy2()
    {
        GameObject spawnedEnemy2 = ObjectPool.instance.GetEnemy2Pool();
        if (spawnedEnemy2 != null)
        {
            spawnedEnemy2.transform.position = enemy2Spawn[0].transform.position;
            spawnedEnemy2.transform.rotation = enemy2Spawn[0].transform.rotation;
            spawnedEnemy2.SetActive(true);
        }

    }
    public void SpawnEnemy3()
    {
        GameObject spawnedEnemy3 = ObjectPool.instance.GetEnemy3Pool();
        if (spawnedEnemy3 != null)
        {
            spawnedEnemy3.transform.position = enemy3Spawn[0].transform.position;
            spawnedEnemy3.transform.rotation = enemy3Spawn[0].transform.rotation;
            spawnedEnemy3.SetActive(true);

        }
    }
    public void SpawnEnemy4()
    {
        GameObject spawnedEnemy4 = ObjectPool.instance.GetEnemy4Pool();
        if (spawnedEnemy4 != null)
        {
            spawnedEnemy4.transform.position = enemy4Spawn[0].transform.position;
            spawnedEnemy4.transform.rotation = enemy4Spawn[0].transform.rotation;
            spawnedEnemy4.SetActive(true);

        }

    }
    public void SpawnEnemy5()
    {
        GameObject spawnedEnemy5 = ObjectPool.instance.GetEnemy5Pool();
        if (spawnedEnemy5 != null)
        {
            spawnedEnemy5.transform.position = enemy5Spawn[0].transform.position;
            spawnedEnemy5.transform.rotation = enemy5Spawn[0].transform.rotation;
            spawnedEnemy5.SetActive(true);

        }

    }
    public void SpawnEnemy6()
    {
        GameObject spawnedEnemy6 = ObjectPool.instance.GetEnemy6Pool();
        if (spawnedEnemy6 != null)
        {
            spawnedEnemy6.transform.position = enemy6Spawn[0].transform.position;
            spawnedEnemy6.transform.rotation = enemy6Spawn[0].transform.rotation;
            spawnedEnemy6.SetActive(true);

        }

    }
    public void SpawnEnemy7()
    {
        GameObject spawnedEnemy7 = ObjectPool.instance.GetEnemy7Pool();
        if (spawnedEnemy7 != null)
        {
            spawnedEnemy7.transform.position = enemy7Spawn[0].transform.position;
            spawnedEnemy7.transform.rotation = enemy7Spawn[0].transform.rotation;
            spawnedEnemy7.SetActive(true);

        }

    }



    //function for pause
    public void Pause()
    {
        
    }
    //resume after pause
    public void Unpause()
    {
        
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
}