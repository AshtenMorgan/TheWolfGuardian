using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Pathfinding;

public class Spider_AI : EnemyController
{



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision == GameManager.Instance.player.GetComponent<Collision2D>())
        {

        }
    }


}
