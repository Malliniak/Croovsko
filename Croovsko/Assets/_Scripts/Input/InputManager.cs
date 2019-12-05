using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Helpers;
using UnityEditor;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameEvent _screenTouchUp;
    public GameEvent _screenHold;
    public GameEvent _screenWithNoInput;

    [SerializeField]
    private Vector2Variable _mousePosition;

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
        
        if (Input.GetMouseButtonUp(0))
        {
            _screenTouchUp.Raise();
        }
        
        if (!Input.GetMouseButton(0))
        {
            _screenWithNoInput.Raise();
        }
    }
}
