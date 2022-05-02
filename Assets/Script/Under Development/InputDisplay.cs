using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputDisplay : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text inputText; //prints the pressed controls so that I can see whats happening
    [SerializeField] InputRecorder inputRecorder;
    void OnEnable()
    {
        if (inputText == null)
            inputText = GameObject.FindGameObjectWithTag("Inputs").GetComponent<TMPro.TMP_Text>();
        // GetComponent input recorder changed to fix a bug
        
    }

    // Update is called once per frame
    void Update()
    {
        if (inputRecorder == null)
            inputRecorder = GameManager.Instance.playerRecorder;
        PrintControls();
    }
    public virtual void PrintControls() //this prints the inputs recorded on the screen
    {
        inputText.text = "Buttons Pressed:\n";
        foreach (KeyCode kcode in inputRecorder.KeysPressed)
            inputText.text += kcode;
    }
}
