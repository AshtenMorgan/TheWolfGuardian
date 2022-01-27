using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResolutionManager : MonoBehaviour
{
    public int screenWidth;
    public int screenHeight;

    public void SetWidth(int newWidth)
    {
        screenWidth = newWidth;
    }

    public void SetHeight(int newHeight)
    {
        screenHeight = newHeight;
    }

    public void SetRes()
    {
        Screen.SetResolution(screenWidth, screenHeight, false);
    }
}
