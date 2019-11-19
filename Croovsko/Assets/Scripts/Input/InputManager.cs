using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager _Manager;

    private void Awake()
    {
        _Manager = this;
    }

    public event Action<Vector3> ScreenTouchUp;
    public event Action ScreenHold;
    public event Action ScreenWithNoInput;

    private void Update()
    {
        if (!Input.GetMouseButton(0))
        {
            Debug.Log("INPUT MANAGER: NO INPUT");
            ScreenWithNoInput?.Invoke();
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("INPUT MANAGER: TOUCH-UP");
            ScreenTouchUp?.Invoke(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Debug.Log("INPUT MANAGER: TOUCH-HOLD");
            ScreenHold?.Invoke();
        }
    }
}
