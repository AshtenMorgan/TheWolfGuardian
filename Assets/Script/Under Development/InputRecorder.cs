using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
/// <summary>
/// created by John Pope, This class is supposed to record all inputs made by the player for other scripts to use when determining
/// which moves to use as this is supposed to have classic arcade fighter movesets. This class consists of a list of keys
/// the player pressed, a list of buttons the player pressed, a timer for when these lists reset and a text that displays what is pressed
/// credit to Fadrik for the assistance in making this script
/// </summary>
public class InputRecorder : MonoBehaviour
{
    [SerializeField] float ComboResetTime; //this is the timer that resets combos when it reaches zero
    [SerializeField] public List<KeyCode> KeysPressed; //lists all the keys pressed by the pawn 
    //[SerializeField] MovesManager movesManager;

    void Awake()
    {
        //if (movesManager == null)
        //    movesManager = FindObjectOfType<MovesManager>(); //assigns a moves manager script if one is not assigned
    }

   
    void Update()
    {
        DetectPressedKey();
    }

    public virtual void DetectPressedKey() 
    {
        foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode))) //this will go through every key pressed
        {
            if (Input.GetKeyDown(kcode))  //if a button is pressed
            {
                KeysPressed.Add(kcode); //add it to the list

                //if (!movesManager.IsMoveAvailable(KeysPressed)) //if there are no moves available from the buttons pressed, reset the list
                //   StopAllCoroutines();

                    StartCoroutine(ResetComboTimer()); //this starts the list reset
            }
        }
    }

    public void ResetCombo() //the function that resets the keys pressed list
    {
        KeysPressed.Clear();
    }

    IEnumerator ResetComboTimer() //resets the timer for combos
    {
        yield return new WaitForSeconds(ComboResetTime);
        //movesManager.PlayMove(KeysPressed);
        KeysPressed.Clear();
    }


}
