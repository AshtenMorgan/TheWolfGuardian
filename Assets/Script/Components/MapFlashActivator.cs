using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFlashActivator : MonoBehaviour
{
    private readonly List<Flasher> squares = new List<Flasher>();

    public void OnTriggerEnter2D(Collider2D other)
    {
        squares.Add(other.GetComponent<Flasher>());
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        squares.Remove(other.GetComponent<Flasher>());
        CancelInvoke();
    }
    public void OnTriggerStay2D(Collider2D other)
    {
        var square = squares[1];
        InvokeRepeating(nameof(square.Flashing), 0.0f, 0.1f);
        
    }
}
