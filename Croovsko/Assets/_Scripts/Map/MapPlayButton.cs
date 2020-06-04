using _Scripts.Helpers;
using _Scripts.Variables;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapPlayButton : MonoBehaviour
{
    private StringVariable _levelId;
    private Transition _sceneController;

    private void Awake()
    {
        AssetLoader.GetAssetFile(out _levelId, "CurrentLevelID");
        _sceneController = FindObjectOfType<Transition>();
    }

    public void LoadCurrentLevel()
    {
        int.TryParse(_levelId._value, out int index);
        _sceneController.LoadScene(index+1);
    }
}