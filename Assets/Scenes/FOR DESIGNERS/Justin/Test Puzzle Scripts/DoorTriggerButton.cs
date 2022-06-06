using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTriggerButton : MonoBehaviour
{
    [SerializeField] private DoorScript door;

    private void Update()
    {
        
        {
            if (Input.GetKeyDown(KeyCode.O))
            {
                door.OpenDoor();
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                door.CloseDoor();
            }
        }
    }
}


