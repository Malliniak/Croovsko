using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSizeProvider 
{
    public float ScreenWidth { get; }
    public float ScreenHeight { get; }
    public float ScreenHalfWidth { get; }
    public float ScreenHalfHeight { get; }

    public ScreenSizeProvider()
    {
        ScreenWidth = Screen.width;
        ScreenHeight = Screen.height;
        ScreenHalfHeight = ScreenHeight / 2;
        ScreenHalfWidth = ScreenWidth / 2;
        
        Debug.Log($"Screen width: {ScreenWidth}");
    }
}
