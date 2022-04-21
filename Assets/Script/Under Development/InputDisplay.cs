using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputDisplay : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text inputText; //prints the pressed controls so that I can see whats happening
    [SerializeField] InputRecorder inputRecorder;
    void Awake()
    {
        if (inputText == null)
            inputText = GameObject.FindGameObjectWithTag("Inputs").GetComponent<TMPro.TMP_Text>();
        if (inputRecorder == null)
            inputRecorder = GameObject.FindGameObjectWithTag("Player").GetComponent<InputRecorder>();
    }

    // Update is called once per frame
    void Update()
    {
        PrintControls();
    }
    public virtual void PrintControls() //this prints the inputs recorded on the screen
    {
        inputText.text = "Buttons Pressed:\n";
        foreach (KeyCode kcode in inputRecorder.KeysPressed)
            inputText.text += kcode;
    }
}
