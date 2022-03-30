using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region variables
    Vector3 cameraPos = Vector3.zero;           //Variable for camera location
    public float dampTime = 0.2f;               //how fast the camera tracks the player
    private Vector3 velocity = Vector3.zero;    //movement speed
    private GameManager gm;
    #endregion


    private void Start()
    {
        gm = GameManager.Instance;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (gm.player)
        {
            cameraPos = new Vector3(gm.player.transform.position.x, gm.player.transform.position.y, -10f);//store player position as vector3
            transform.position = Vector3.SmoothDamp(gameObject.transform.position, cameraPos, ref velocity, dampTime);//move camera to match player, delayed a bit by dampTime
        }
        
    }
}
