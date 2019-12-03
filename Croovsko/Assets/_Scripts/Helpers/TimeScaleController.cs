using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaleController
{
    public float TimeScale
    {
        get => Time.timeScale;
        private set { }
    }

    public float DeltaTime => Time.deltaTime;

    public TimeScaleController()
    {
        TimeScale = Time.timeScale;
    }

    public void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
        TimeScale = Time.timeScale;
    }
    
    public IEnumerator ScaleTimeOverTime(float start, float end, float time) //not in Start or Update
    {
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0.0f;

        while (timer < time)
        {
            Time.timeScale = Mathf.Lerp(start, end, timer / time);
            
            timer += (Time.realtimeSinceStartup - lastTime);
            lastTime = Time.realtimeSinceStartup;
            yield return null;
        }

        Time.timeScale = end;
        Time.fixedDeltaTime = end * 0.02f;
    }
}
