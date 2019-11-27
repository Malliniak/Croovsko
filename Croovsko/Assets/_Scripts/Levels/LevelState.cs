using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelState : ScriptableObject
{
    public int LevelID;
    public IntReference MaxBellsToCollect;
    public IntReference HighestBellsCollected;
    public IntReference BellsCollectedDuringRuntime;
    public IntReference PointsCollected;
    public IntReference HighestPointsCollected;

    private void OnDisable()
    {
        SaveLevelState();
    }

    private void SaveLevelState()
    {
        int bells = BellsCollectedDuringRuntime.Value > HighestBellsCollected.Value
            ? BellsCollectedDuringRuntime.Value
            : HighestBellsCollected.Value;
        HighestBellsCollected.Variable.SetValue(bells);

        int points = PointsCollected.Value > HighestPointsCollected.Value
            ? PointsCollected.Value
            : HighestPointsCollected.Value;
        HighestPointsCollected.Variable.SetValue(points);
    }
    
}
