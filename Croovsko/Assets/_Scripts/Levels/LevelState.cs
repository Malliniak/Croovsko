using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class LevelState : ScriptableObject
{
    public string _levelId;
    public int _maxBellsToCollect;

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
    public int _bellsCollectedDuringRuntime;

    public int _pointsCollected;
    
    private void OnDisable()
    {
        SaveLevelState();
    }

    public static LevelState GetLevelStateAsset()
    {
        var id = SceneManager.GetActiveScene().name;
        LevelState levelState;
        AssetLoader.GetAssetFile(out levelState, $"Lvl{id}State");
        Debug.Log(levelState);
        return levelState;
    }

    public void SaveLevelState()
    {
        int bells = _bellsCollectedDuringRuntime > HighestBellsCollected
            ? _bellsCollectedDuringRuntime
            : HighestBellsCollected;
        HighestBellsCollected = bells;

        int points = _pointsCollected > HighestPointsCollected
            ? _pointsCollected
            : HighestPointsCollected;
        HighestPointsCollected = points;
    }
}
