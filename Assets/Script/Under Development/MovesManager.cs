using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovesManager : MonoBehaviour
{
    [SerializedField] List<Move> availableMoves;
    Controller controller;
    InputRecorder inputRecorder;
    // Start is called before the first frame update
    void Awake()
    {
        controller = FindObjectOfType<Controller>();
        inputRecorder = FindObjectOfType<InputRecorder>();

        availableMoves.Sort(Compare); //Sorts all the moves based on their priority
    }

    public bool IsMoveAvailable(List<KeyCode> keycodes) 
    {
        foreach (Moves moves in availableMoves) 
        {
            if (moves.IsMoveAvailable(keycodes))
                return true;
        }
        return false;
    }

    public void PlayMove(List<KeyCode> keycodes) 
    {
        foreach (Moves moves in availableMoves) 
        {
            if (moves.isMoveAvailable(keycodes)) 
            {
                controller.PlayMove(moves.GetMove(), moves.GetMoveComboPriority());
                break;
            }
        }
    }

    public int Compare(Moves move1, Moves move2) 
    {
        return Comparer<int>.Default.Compare(move2.GetMoveComboPriority(), move1.GetMoveComboPriority());
    }
}
