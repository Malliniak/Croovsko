//
//  Square
//
//  Created by Krzysztof Maliński, Adam Kolinski, 
// Damian Klabuhn, Mikolaj Mikolajczak
//  
// Code inspired by Croovsko, project by Krzysztof Malinski
// 


using System.Collections;
using _Scripts.Helpers;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public enum InputType
    {
        Touches,
        Mouse
    }

    [SerializeField] private Vector2Variable _mousePosition;

    public GameEvent _screenHold;
    public GameEvent _screenTouchUp;
    public GameEvent _screenTouchUpAfterHold;
    public GameEvent _screenWithNoInput;

    private bool _isHolding;

    public InputType _shouldUseTouches;

    private void Awake()
    {
        AssetLoader.GetAssetFile(out _mousePosition, "MousePosition");
        AssetLoader.GetAssetFile(out _screenHold, "Hold");
        AssetLoader.GetAssetFile(out _screenTouchUp, "TouchUp");
        AssetLoader.GetAssetFile(out _screenWithNoInput, "NoInput");
        AssetLoader.GetAssetFile(out _screenTouchUpAfterHold, "TouchUpAfterHold");
    }

    private void Update()
    {
        if (_shouldUseTouches == InputType.Mouse)
            MouseInput();
        if (_shouldUseTouches == InputType.Touches)
            TouchInput();
    }

    private IEnumerator TouchHoldCoroutine(Touch touch)
    {
        yield return new WaitForSeconds(0.25f);
        if (touch.phase != TouchPhase.Ended) _isHolding = true;
    }

    private IEnumerator MouseHoldCoroutine()
    {
        yield return new WaitForSeconds(0.25f);
        if (Input.GetMouseButton(0)) _isHolding = true;
    }

    private void MouseInput()
    {
        if (!Input.GetMouseButton(0)) _screenWithNoInput.Raise();

        if (Input.GetMouseButtonDown(0)) StartCoroutine(nameof(MouseHoldCoroutine));

        if (Input.GetMouseButtonUp(0))
        {
            StopCoroutine(nameof(MouseHoldCoroutine));
            if (_isHolding)
            {
                print("Touch up with hold");
                _isHolding = false;
                _screenTouchUpAfterHold.Raise();
                return;
            }

            print("Touch up without hold");
            _mousePosition.SetValue(Input.mousePosition);
            _screenTouchUp.Raise();
        }

        if (_isHolding)
        {
            print("HOLD");
            _screenHold.Raise();
        }
    }

    private void TouchInput()
    {
        if (Input.touches.Length == 0)
        {
            _screenWithNoInput.Raise();
            return;
        }

        Touch firstTouch = Input.GetTouch(0);

        if (firstTouch.phase == TouchPhase.Began) StartCoroutine(nameof(TouchHoldCoroutine), firstTouch);
        if (firstTouch.phase == TouchPhase.Ended)
        {
            StopCoroutine(nameof(TouchHoldCoroutine));
            if (_isHolding)
            {
                _isHolding = false;
                _screenTouchUpAfterHold.Raise();
                return;
            }

            _mousePosition.SetValue(firstTouch.position);
            _screenTouchUp.Raise();
        }

        if (_isHolding) _screenHold.Raise();
    }
}