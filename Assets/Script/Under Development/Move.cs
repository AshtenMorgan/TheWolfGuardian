using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : ScriptableObject
{
    [SerializeField] List<KeyCode> movesKeyCodes;
    [SerializeField] Move moveType;
    [SerializeField] int ComboPriority = 0;

    public bool isMoveAvailable(List<KeyCode> playerKeyCodes)
    {
        int comboIndex = 0;

        for (int i = 0; i < playerKeyCodes.Count; i++)
        {
            if (playerKeyCodes[i] == movesKeyCodes[comboIndex])
            {
                if (playerKeyCodes[i] == movesKeyCodes[comboIndex])
                {
                    comboIndex++;
                    if (comboIndex == movesKeyCodes.Count)
                        return true;
                }
                else
                    comboIndex = 0;
            }
        }
        return false;
    }

    public int GetMoveComboCount()
    {
        return movesKeyCodes.Count;
    }

    public int GetMoveComboPriority()
    {
        return ComboPriority;
    }

    public Move GetMove()
    {
        return moveType;
    }
}

