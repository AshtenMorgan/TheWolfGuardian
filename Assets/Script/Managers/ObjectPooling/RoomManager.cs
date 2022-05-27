using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    protected ObjectPool _pool;
    protected GameObject _spawnedEnemy;
    protected CompositeCollider2D roomCollider;
    protected GameManager gm;
    private void Start()
    {
        _pool = GetComponent<ObjectPool>();
        roomCollider = GetComponent<CompositeCollider2D>();
        gm = GameManager.Instance;
        
    }
    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerPawn>())
        {
            for (int i = 0; i < _pool.enemyPool.Count; i++)
            {
                _spawnedEnemy = _pool.enemyPool[i];
                _spawnedEnemy.transform.SetPositionAndRotation(_pool.spawnPoint[i].transform.position, _pool.spawnPoint[i].transform.rotation);//spawn with rotation
                Health healthReset = _spawnedEnemy.GetComponent<Health>();//store health component
                healthReset.Respawn();//restore health to max
                _spawnedEnemy.SetActive(true);//activate enemy
            }
            
        }
       
    }
    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        //track which confiner room player is currently in
        gm.currentRoom = roomCollider;
    }
    public virtual void OnTriggerExit2D(Collider2D collision)
    {if (collision.GetComponent<PlayerPawn>())
        {
            for (int i = 0; i < _pool.enemyPool.Count; i++)
            {
                if (_pool.enemyPool[i].activeInHierarchy)
                {
                    _pool.enemyPool[i].SetActive(false);
                }
            }
        }
    }
}