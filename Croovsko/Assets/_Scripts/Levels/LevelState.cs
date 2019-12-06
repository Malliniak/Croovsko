using _Scripts.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class LevelState : ScriptableObject
{
    [Header("Runtime State")] public int _bellsCollectedDuringRuntime;

    public string _levelId;
    public int _maxBellsToCollect;

    public int _pointsCollected;

    public bool Unlocked;

    public int HighestBellsCollected { get; private set; }

    public int HighestPointsCollected { get; private set; }

    private void OnDisable()
    {
        SaveLevelState();
    }

    public static LevelState GetLevelStateAsset()
    {
        string id = SceneManager.GetActiveScene().name;
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