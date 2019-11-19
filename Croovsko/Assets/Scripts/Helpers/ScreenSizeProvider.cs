using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSizeProvider 
{
    public float screenWidth { get; }
    public float screenHeight { get; }
    public float screenHalfWidth { get; }
    public float screenHalfHeight { get; }

    public ScreenSizeProvider()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        screenHalfHeight = screenHeight / 2;
        screenHalfWidth = screenWidth / 2;
    }
}
