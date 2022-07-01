using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
   public void OpenDoor()
    {
        gameObject.SetActive(false);
    }
    public void CloseDoor()
    {
        gameObject.SetActive(true);

    }
}
