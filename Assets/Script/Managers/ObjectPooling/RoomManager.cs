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
        if (other.CompareTag("Player"))
        {
            gm.currentRoom = roomCollider;
            StartCoroutine(UpdateSpawn(0.2f));
            for (int i = 0; i < _pool.enemyPool.Count; i++)
            {
                _spawnedEnemy = _pool.enemyPool[i];
                _spawnedEnemy.transform.SetPositionAndRotation(_pool.spawnPoints[i].transform.position, _pool.spawnPoints[i].transform.rotation);//spawn with rotation
                Health healthReset = _spawnedEnemy.GetComponent<Health>();//store health component
                healthReset.Respawn();//restore health to max
                _spawnedEnemy.SetActive(true);//activate enemy
                
            }
            
        }
       
    }
    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        //track which confiner room player is currently in
        if (collision.CompareTag("Player"))
        {
            gm.currentRoom = roomCollider;
            gm.confiner.m_BoundingShape2D = this.GetComponent<CompositeCollider2D>();
        }
        
    }

    public virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
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

    IEnumerator UpdateSpawn(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        GameManager.Instance.playerSpawn.transform.position = GameManager.Instance.player.transform.position;
    }
}
