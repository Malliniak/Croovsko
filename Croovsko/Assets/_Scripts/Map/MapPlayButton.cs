using _Scripts.Helpers;
using _Scripts.Variables;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapPlayButton : MonoBehaviour
{
    private StringVariable _levelId;

    private void Awake()
    {
        AssetLoader.GetAssetFile(out _levelId, "CurrentLevelID");
    }

    public void LoadCurrentLevel()
    {
        SceneManager.LoadScene(_levelId._value);
    }
}