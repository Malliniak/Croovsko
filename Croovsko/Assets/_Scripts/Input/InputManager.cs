using _Scripts.Helpers;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Vector2Variable _mousePosition;

    public GameEvent _screenHold;
    public GameEvent _screenTouchUp;
    public GameEvent _screenWithNoInput;

    private void Awake()
    {
        AssetLoader.GetAssetFile(out _mousePosition, "MousePosition");
        AssetLoader.GetAssetFile(out _screenHold, "Hold");
        AssetLoader.GetAssetFile(out _screenTouchUp, "TouchUp");
        AssetLoader.GetAssetFile(out _screenWithNoInput, "NoInput");
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _mousePosition.SetValue(Input.mousePosition);
            _screenHold.Raise();
        }

        if (Input.GetMouseButtonUp(0)) _screenTouchUp.Raise();

        if (!Input.GetMouseButton(0)) _screenWithNoInput.Raise();
    }
}