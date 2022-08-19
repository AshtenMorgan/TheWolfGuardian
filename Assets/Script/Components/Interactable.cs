using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Interactable : MonoBehaviour
{
    public Canvas notificationIcon;
    protected PlayerInput playerInput; //defines the input that the pawn is utlizing 
    protected PlayerInputActions playerInputActions; //variable for storing the input schemes the pawn will be using
    public UnityEvent Interact;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerControllerV2 pc = other.GetComponent<PlayerControllerV2>();
            pc.InteractRange = true;
            //GameManager.Instance.NotifyPlayer();
            notificationIcon.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerControllerV2 pc = other.GetComponent<PlayerControllerV2>();
            pc.InteractRange = false;
            //GameManager.Instance.DeNotifyPlayer();
            notificationIcon.enabled = false;
        }
    }
}