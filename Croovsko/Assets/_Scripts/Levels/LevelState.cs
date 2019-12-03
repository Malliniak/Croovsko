using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class LevelState : ScriptableObject
{
    public string LevelID;
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

    public static LevelState GetLevelStateAsset()
    {
        var id = SceneManager.GetActiveScene().name;
        LevelState LevelState;
        AssetLoader.GetAssetFile(out LevelState, $"Lvl{id}State");
        Debug.Log(LevelState);
        return LevelState;
    }

    public void SaveLevelState()
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
