using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    #region Pools
    public List<GameObject> enemy1Pool;
    public List<GameObject> enemy2Pool;
    public List<GameObject> enemy3Pool;
    public List<GameObject> enemy4Pool;
    public List<GameObject> enemy5Pool;
    public List<GameObject> enemy6Pool;
    public List<GameObject> enemy7Pool;
    #endregion

    #region Game Objects
    public GameObject Player;
    public GameObject Enemy1;
    public GameObject Enemy2;
    public GameObject Enemy3;
    public GameObject Enemy4;
    public GameObject Enemy5;
    public GameObject Enemy6;
    public GameObject Enemy7;
    #endregion



    public static ObjectPool instance { get; private set; }//allow other classes to access pools


    void Awake()
    {//make sure there is always only 1 instance
        if (ObjectPool.instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        //create pool lists
        enemy1Pool = new List<GameObject>();
        enemy2Pool = new List<GameObject>();
        enemy3Pool = new List<GameObject>();
        enemy4Pool = new List<GameObject>();
        enemy5Pool = new List<GameObject>();
        enemy6Pool = new List<GameObject>();
        enemy7Pool = new List<GameObject>();

        //initial spawning for object pooling

        //player
        Player = (GameObject)Instantiate(GameManager.instance.playerPrefab, GameManager.instance.playerInstan.position, GameManager.instance.playerInstan.rotation);//spawn player
        Player.gameObject.SetActive(false);//everything is inactivated on initial spawn

        //enemies

        //create a temporary game object to hold each new instantiated object
        GameObject tmp;

        //loop to create all the enemies for a pool

        //enemy 1
        for (int i = 0; i < GameManager.instance.enemy1Max; i++)
        {
            tmp = Instantiate(Enemy1, GameManager.instance.enemyInstan.position, GameManager.instance.enemyInstan.rotation);
            tmp.SetActive(false);
            enemy1Pool.Add(tmp);
        }
        //enemy 2
        for (int i = 0; i < GameManager.instance.enemy2Max; i++)
        {
            tmp = Instantiate(Enemy2, GameManager.instance.enemyInstan.position, GameManager.instance.enemyInstan.rotation);
            tmp.SetActive(false);
            enemy2Pool.Add(tmp);
        }
        //enemy 3
        for (int i = 0; i < GameManager.instance.enemy3Max; i++)
        {
            tmp = Instantiate(Enemy3, GameManager.instance.enemyInstan.position, GameManager.instance.enemyInstan.rotation);
            tmp.SetActive(false);
            enemy3Pool.Add(tmp);
        }
        //enemy 4
        for (int i = 0; i < GameManager.instance.enemy4Max; i++)
        {
            tmp = Instantiate(Enemy4, GameManager.instance.enemyInstan.position, GameManager.instance.enemyInstan.rotation);
            tmp.SetActive(false);
            enemy4Pool.Add(tmp);
        }
        //enemy 5
        for (int i = 0; i < GameManager.instance.enemy5Max; i++)
        {
            tmp = Instantiate(Enemy5, GameManager.instance.enemyInstan.position, GameManager.instance.enemyInstan.rotation);
            tmp.SetActive(false);
            enemy5Pool.Add(tmp);
        }
        //enemy 1
        for (int i = 0; i < GameManager.instance.enemy6Max; i++)
        {
            tmp = Instantiate(Enemy6, GameManager.instance.enemyInstan.position, GameManager.instance.enemyInstan.rotation);
            tmp.SetActive(false);
            enemy6Pool.Add(tmp);
        }
        //enemy 7
        for (int i = 0; i < GameManager.instance.enemy7Max; i++)
        {
            tmp = Instantiate(Enemy7, GameManager.instance.enemyInstan.position, GameManager.instance.enemyInstan.rotation);
            tmp.SetActive(false);
            enemy7Pool.Add(tmp);
        }
    }

    public GameObject GetEnemy1Pool()
    {
        for (int i = 0; i < GameManager.instance.enemy1Max; i++)
        {
            if (!enemy1Pool[i].activeInHierarchy)
            {
                return enemy1Pool[i];
            }
            
        }
        return null;
    }
    public GameObject GetEnemy2Pool()
    {
        for (int i = 0; i < GameManager.instance.enemy2Max; i++)
        {
            if (!enemy2Pool[i].activeInHierarchy)
            {
                return enemy2Pool[i];
            }

        }
        return null;
    }
    public GameObject GetEnemy3Pool()
    {
        for (int i = 0; i < GameManager.instance.enemy3Max; i++)
        {
            if (!enemy3Pool[i].activeInHierarchy)
            {
                return enemy3Pool[i];
            }

        }
        return null;
    }
    public GameObject GetEnemy4Pool()
    {
        for (int i = 0; i < GameManager.instance.enemy4Max; i++)
        {
            if (!enemy4Pool[i].activeInHierarchy)
            {
                return enemy4Pool[i];
            }

        }
        return null;
    }
    public GameObject GetEnemy5Pool()
    {
        for (int i = 0; i < GameManager.instance.enemy5Max; i++)
        {
            if (!enemy5Pool[i].activeInHierarchy)
            {
                return enemy5Pool[i];
            }

        }
        return null;
    }
    public GameObject GetEnemy6Pool()
    {
        for (int i = 0; i < GameManager.instance.enemy6Max; i++)
        {
            if (!enemy6Pool[i].activeInHierarchy)
            {
                return enemy6Pool[i];
            }

        }
        return null;
    }
    public GameObject GetEnemy7Pool()
    {
        for (int i = 0; i < GameManager.instance.enemy7Max; i++)
        {
            if (!enemy7Pool[i].activeInHierarchy)
            {
                return enemy7Pool[i];
            }

        }
        return null;
    }
}
