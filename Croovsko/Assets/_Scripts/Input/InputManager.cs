using System.Collections;
using _Scripts.Helpers;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool shouldUseTouches;
    [SerializeField] private Vector2Variable _mousePosition;

    public GameEvent _screenHold;
    public GameEvent _screenTouchUp;
    public GameEvent _screenWithNoInput;
    public GameEvent _screenTouchUpAfterHold;

    private bool isHolding = false;

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
        if(!shouldUseTouches)
            MouseInput();
        if(shouldUseTouches)
            TouchInput();
    }

    private IEnumerator TouchHoldCoroutine(Touch touch)
    {
        yield return new WaitForSeconds(0.25f);
        if (touch.phase != TouchPhase.Ended)
        {
            isHolding = true;
        }
    }

    private IEnumerator MouseHoldCoroutine()
    {
        yield return new WaitForSeconds(0.25f);
        if (Input.GetMouseButton(0))
        {
            isHolding = true;
        }
    }

    private void MouseInput()
    {
        if (!Input.GetMouseButton(0))
        {
            _screenWithNoInput.Raise();
        }

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(nameof(MouseHoldCoroutine));
        }

        if (Input.GetMouseButtonUp(0))
        {
            StopCoroutine(nameof(MouseHoldCoroutine));
            if (isHolding)
            {
                print("Touch up with hold");
                isHolding = false;
                _screenTouchUpAfterHold.Raise();
                return;
            }
            print("Touch up without hold");
            _mousePosition.SetValue(Input.mousePosition);
            _screenTouchUp.Raise();
        }

        if (isHolding)
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
        
        if (firstTouch.phase == TouchPhase.Began)
        {
            StartCoroutine(nameof(TouchHoldCoroutine), firstTouch);
        }
        if (firstTouch.phase == TouchPhase.Ended)
        {
            StopCoroutine(nameof(TouchHoldCoroutine));
            if (isHolding)
            {
                isHolding = false;
                _screenTouchUpAfterHold.Raise();
                return;
            }
            _mousePosition.SetValue(firstTouch.position);
            _screenTouchUp.Raise();
        }

        if (isHolding)
        {
            _screenHold.Raise();
        }
    }
}