using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelState : ScriptableObject
{
    public int LevelID;
    public int MaxBellsToCollect;

    public int HighestBellsCollected
    {
        get;
        private set;
    }

    public int HighestPointsCollected
    {
        get;
        private set;
    }
    
    [Header("Runtime State")]
    public int BellsCollectedDuringRuntime;

    public int PointsCollected;
    
    private void OnDisable()
    {
        SaveLevelState();
    }

    private void SaveLevelState()
    {
        int bells = BellsCollectedDuringRuntime > HighestBellsCollected
            ? BellsCollectedDuringRuntime
            : HighestBellsCollected;
        HighestBellsCollected = bells;

        int points = PointsCollected > HighestPointsCollected
            ? PointsCollected
            : HighestPointsCollected;
        HighestPointsCollected = points;
    }
    
}
