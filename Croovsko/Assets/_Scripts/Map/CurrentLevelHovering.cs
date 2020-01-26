using _Scripts.Helpers;
using _Scripts.Variables;
using UnityEngine;

[CreateAssetMenu]
public class CurrentLevelHovering : ScriptableObject
{
    public StringVariable _id;
    public StringVariable _maxPointsCollected;
    public StringVariable _maxStarsCollected;

    private void OnEnable()
    {
        AssetLoader.GetAssetFile(out _id, "CurrentLevelID");
        AssetLoader.GetAssetFile(out _maxPointsCollected, "CurrentLevelMaxPointsCollected");
        AssetLoader.GetAssetFile(out _maxStarsCollected, "CurrentLevelMaxStarsCollected");
    }

    public void SetData(LevelState levelState)
    {
        _id._value = levelState._levelId;
        _maxPointsCollected._value = $"{levelState.HighestPointsCollected}";
        _maxStarsCollected._value = $"{levelState.HighestBellsCollected}";
    }
}