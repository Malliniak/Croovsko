using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public GameEvent ScreenTouchUp;
    public GameEvent ScreenHold;
    public GameEvent ScreenWithNoInput;

    [SerializeField]
    private Vector2Variable mousePosition;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            mousePosition.SetValue(Input.mousePosition);
            ScreenHold.Raise();
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            ScreenTouchUp.Raise();
        }
        
        if (!Input.GetMouseButton(0))
        {
            ScreenWithNoInput.Raise();
        }
    }
}
