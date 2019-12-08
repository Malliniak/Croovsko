using System;
using _Scripts.Helpers;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private LevelState _levelState;
    private IntVariable PointsRuntime;
    private IntVariable BellsRuntime;

    private void Awake()
    {
        _levelState = LevelState.GetLevelStateAsset();
        AssetLoader.GetAssetFile(out BellsRuntime, "BellsRuntime");
        AssetLoader.GetAssetFile(out PointsRuntime, "PointsRuntime");
    }

    private void Start()
    {
        PointsRuntime.SetValue(0);
        BellsRuntime.SetValue(0);
    }
}