//
//  Square
//
//  Created by Krzysztof Maliński, Adam Kolinski, 
// Damian Klabuhn, Mikolaj Mikolajczak
//  
// Code inspired by Croovsko, project by Krzysztof Malinski
// 


using System;
using DigitalRuby.Tween;
using UnityEngine;

public class TimeScaleController
{
    public TimeScaleController()
    {
        TimeScale = Time.timeScale;
    }

    public float TimeScale
    {
        get => Time.timeScale;
        private set { }
    }

    public void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
    }

    public void SlowDownTime(float end, float time)
    {
        Action<ITween<float>> cameraZoomOut = t => { Time.timeScale = t.CurrentValue; };
        TweenFactory.Tween("SlowDown", TimeScale, end, time, TweenScaleFunctions.CubicEaseInOut, cameraZoomOut);
    }

    public void NormalTime(float time)
    {
        Action<ITween<float>> cameraZoomOut = t => { Time.timeScale = t.CurrentValue; };
        TweenFactory.Tween("SlowDown", Time.timeScale, 1, time, TweenScaleFunctions.CubicEaseInOut, cameraZoomOut);
    }
}