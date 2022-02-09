using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            PlayerPawn player = FindObjectOfType<PlayerPawn>();
            Health health = player.gameObject.GetComponent<Health>();
            health.Damage(10);
            Debug.Log("Health smack");
        }
        if (Input.GetKeyDown(KeyCode.Delete))
        {
            GameManager.Instance.player.gameObject.SetActive(false);
            ObjectPool.instance.enemy1Pool[1].gameObject.SetActive(false);
            ObjectPool.instance.enemy2Pool[1].gameObject.SetActive(false);
            ObjectPool.instance.enemy3Pool[1].gameObject.SetActive(false);
            ObjectPool.instance.enemy4Pool[1].gameObject.SetActive(false);
            ObjectPool.instance.enemy5Pool[1].gameObject.SetActive(false);
            ObjectPool.instance.enemy6Pool[1].gameObject.SetActive(false);
            ObjectPool.instance.enemy7Pool[1].gameObject.SetActive(false);

        }
    }
}
