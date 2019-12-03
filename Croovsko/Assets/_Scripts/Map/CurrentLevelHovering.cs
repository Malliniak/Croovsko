using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Helpers;
using _Scripts.Variables;
using UnityEngine;

[CreateAssetMenu]
public class CurrentLevelHovering : ScriptableObject
{
    public StringVariable ID;
    public StringVariable MaxPointsCollected;
    public StringVariable MaxStarsCollected;

    private void OnEnable()
    {
        AssetLoader.GetAssetFile(out ID, "CurrentLevelID");
        AssetLoader.GetAssetFile(out MaxPointsCollected, "CurrentLevelMaxPointsCollected");
        AssetLoader.GetAssetFile(out MaxStarsCollected, "CurrentLevelMaxStarsCollected");
    }

    public void SetData(LevelState levelState)
    {
        ID.Value = levelState.LevelID;
        MaxPointsCollected.Value = $"{levelState.HighestPointsCollected}";
        MaxStarsCollected.Value = $"{levelState.HighestBellsCollected}";
    }
}
