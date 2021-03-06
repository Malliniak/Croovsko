﻿using UnityEngine;

public class ScreenSizeProvider
{
    public ScreenSizeProvider()
    {
        ScreenWidth = Screen.width;
        ScreenHeight = Screen.height;
        ScreenHalfHeight = ScreenHeight / 2;
        ScreenHalfWidth = ScreenWidth / 2;

        Debug.Log($"Screen width: {ScreenWidth}");
    }

    public float ScreenWidth { get; }
    public float ScreenHeight { get; }
    public float ScreenHalfWidth { get; }
    public float ScreenHalfHeight { get; }
}