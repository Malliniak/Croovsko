using UnityEngine;

public class LevelManager : MonoBehaviour
{
    private LevelState _levelState;

    private void Awake()
    {
        _levelState = LevelState.GetLevelStateAsset();
    }
}
