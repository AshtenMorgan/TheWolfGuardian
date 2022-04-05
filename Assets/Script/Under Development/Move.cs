using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Move", menuName = "New Move")]
public class Move : ScriptableObject
{
    [SerializeField] List<KeyCode> movesKeyCodes;
    [SerializeField] Moveset moveType;
    [SerializeField] int ComboPriority = 0;

    public bool IsMoveAvailable(List<KeyCode> playerKeyCodes)
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

    public Moveset GetMove()
    {
        return moveType;
    }
}

