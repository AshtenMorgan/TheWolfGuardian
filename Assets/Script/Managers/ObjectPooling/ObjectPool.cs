using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    #region variables
    #region Pools
    [HideInInspector] public List<GameObject> enemyPool;

    [SerializeField, HideInInspector] private List<string> _prefabLocation;
    [SerializeField, HideInInspector] private List<GameObject> _allPoints;
    #endregion
    #region Game Objects
    [Header("Instantiation point - 1 per room")]
    public Transform instanPoint;

    //hide these after testing
    public List<GameObject> enemy;
    [Header("This should show all of the spawn points")]
    public List<Transform> spawnPoints;
    #endregion
    #endregion

    #region Functions
    private void Start()
    {
        GetSpawnPoints();
        CreateSpawnLists();
        PoolSetup();
    }
    #region Enemy Pool checks
    public void PoolSetup()
    {
        //create pool lists
        enemyPool = new List<GameObject>();

        #region Object Creation
        //initial spawning for object pooling
        //enemies
        for (int i = 0; i < _allPoints.Count; i++)
        {
            enemy.Add(Resources.Load(_prefabLocation[i]) as GameObject);
        }

        GameObject tmp;//create a temporary game object to hold each new instantiated object

        //stick enemies into pool
        for (int i = 0; i < enemy.Count; i++)
        {
            tmp = Instantiate(enemy[i], instanPoint.position, instanPoint.rotation) as GameObject;//instantiate enemy, hold it in tmp GO
            tmp.SetActive(false);//hide and disable the enemy
            enemyPool.Add(tmp);//add enemy to pool
        }

        #endregion
    }
    #endregion
    public void GetSpawnPoints()
    {
        Transform[] t = gameObject.GetComponentsInChildren<Transform>();//create an array of child transforms


        foreach (Transform child in t)
        {
            if (child.CompareTag("EnemySpawn"))
                _allPoints.Add(child.gameObject);
        }

        spawnPoints = new List<Transform>();
        for (int i = 0; i < _allPoints.Count; i++)
        {
            spawnPoints.Add(_allPoints[i].GetComponent<Transform>());
        }
    }
    void CreateSpawnLists()
    {   //set up enemy types
        _prefabLocation = new List<string>();

        for (int i = 0; i < _allPoints.Count; i++)
        {
            if (_allPoints[i].GetComponent<Beast_UF_Redcap>())//redcap
                _prefabLocation.Add("Prefabs/Pawn Prefabs/Beastiary Prefabs/Unseelie Fey/Beast_UF_Redcap");
            
            else if (_allPoints[i].GetComponent<Beast_SP_Webspitter>())//spider
                _prefabLocation.Add("Prefabs/Pawn Prefabs/Beastiary Prefabs/Spiders/Beast_SP_Webspitter");
            else
                return;
        }

    }

    #endregion
}
