using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string Destination;

    private GameObject target;

    private PlayerControllerV2 controller;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        target = collision.gameObject;
        controller = target.GetComponent<PlayerControllerV2>();
        if (controller != null)
        {
            SceneManager.LoadScene(Destination);
        }
    }
}
