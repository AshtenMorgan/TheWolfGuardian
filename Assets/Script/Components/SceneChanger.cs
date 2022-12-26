//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;

//public class SceneChanger : MonoBehaviour
//{
//    public string Destination;

//    private GameObject target;

//    private PlayerControllerV2 controller;

//    private void OnTriggerEnter2D(Collider2D collision)
//    {
//        target = collision.gameObject;
//        controller = target.GetComponent<PlayerControllerV2>();
//        if (controller != null)
//        {
//            SceneManager.LoadScene(Destination);
//        }
//    }
//}
