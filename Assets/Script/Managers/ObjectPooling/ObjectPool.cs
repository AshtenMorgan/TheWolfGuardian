using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    #region variables
    public enum Enemies
    {
        Redcap,
        ShadowWidow
    }

    #region Pools
    [HideInInspector]
    public List<GameObject> enemyPool;
    private string[] _prefabLocation;
    [Header("All enemies you wish to spawn"), Tooltip("Select 1 type per enemy spawn")]
    public Enemies[] m_enemies;
    
    
    #endregion

    #region Game Objects
    [Header("The NUMBER of enemy spawns"), Tooltip("Do not set game objects, just the number")]
    public GameObject[] enemy;
    [Header("Instantiation point - 1 per room")]
    public Transform instanPoint;
    [Header("Spawn Points - Match this to the number of enemies you set Up there ^^")]
    public Transform[] spawnPoint;

    #endregion


    #endregion

    #region Functions
    private void Start()
    {
        EnumToString();
    }
    #region Enemy Pool checks
    public void PoolSetup()
    {
        //create pool lists
        enemyPool = new List<GameObject>();
        
        #region Object Creation
        //initial spawning for object pooling
        //enemies
        // Enemy1 = Resources.Load("Prefabs/Test Prefabs/Test Enemies/Enemy1") as GameObject;
        for (int i = 0; i < enemy.Length; i++)
        {
            enemy[i] = Resources.Load(_prefabLocation[i]) as GameObject;
        }

        GameObject tmp;//create a temporary game object to hold each new instantiated object

        //stick enemies into pool
        for (int i = 0; i < enemy.Length; i++)
        {
            tmp = Instantiate(enemy[i], instanPoint.position, instanPoint.rotation) as GameObject;//instantiate enemy, hold it in tmp GO
            tmp.SetActive(false);//hide and disable the enemy
            enemyPool.Add(tmp);//add enemy to pool
        }
            
        
       
        #endregion
    }
    void EnumToString()
    {
        _prefabLocation = new string[m_enemies.Length];
        for (int i = 0; i < m_enemies.Length; i++)
        {
            switch (m_enemies[i])
            {
                case Enemies.Redcap:
                    _prefabLocation[i] = "Prefabs/Pawn Prefabs/Beastiary Prefabs/Unseelie Fey/Beast_UF_Redcap";
                    break;

                case Enemies.ShadowWidow:
                    _prefabLocation[i] = "Prefabs/Pawn Prefabs/Beastiary Prefabs/Spiders/Beast_SP_Shadow";
                    break;

                default:
                    break;
            }
        }
        PoolSetup();
    }
  
    #endregion
    #endregion
}
