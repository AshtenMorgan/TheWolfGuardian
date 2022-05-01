using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private ObjectPool _pool;
    private GameObject _spawnedEnemy;
    private void Start()
    {
        _pool = GetComponent<ObjectPool>();
    }
    private void OnTriggerEnter2D(Collider2D other)
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
 
    private void OnTriggerExit2D(Collider2D collision)
    {
        for (int i = 0; i < _pool.enemyPool.Count; i++)
        {
            if (_pool.enemyPool[i].activeInHierarchy)
            {
               _pool.enemyPool[i].SetActive(false);
            }
        }
        return;
    }
}
