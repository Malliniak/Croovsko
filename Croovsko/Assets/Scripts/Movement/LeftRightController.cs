using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LeftRightController : MonoBehaviour
{
    private ScreenSizeProvider _screenSizeProvider;
    private TimeScaleController _timeScaleController;

    public Vector2 forceDirection;
    private Vector2 mousePos;
    private Rigidbody2D _rb2D;

    [SerializeField] private float _holdTimer, _holdToActivate = 1f;

    [Range(0.1f, 1f)] public float slowMotionValue;
    
    private bool joystickControls;

    [FormerlySerializedAs("analogForceDirection")] public Vector2 joystickForceDirection;

    public DynamicJoystick joystick;

    private IEnumerator _slowMotionCourutine;

    private void Awake()
    {
        _screenSizeProvider = new ScreenSizeProvider();
        _timeScaleController = new TimeScaleController();
        
        InputManager._Manager.ScreenTouchUp += JumpOnTouch;
        InputManager._Manager.ScreenHold += JoystickControl;
        InputManager._Manager.ScreenWithNoInput += NoInputAction;
    }
    
    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();
        _slowMotionCourutine = _timeScaleController.ScaleTimeOverTime(1, slowMotionValue, 0.5f);
        Debug.Log($"Screen width: {_screenSizeProvider.screenWidth}");
    }
    
    void Update()
    {
        if (joystickControls)
        {
            var lookVec = new Vector3(joystick.input.x, joystick.input.y, 0);
            transform.LookAt2d(lookVec);
            JoystickForceDirection(lookVec);
        }
    }

    //TODO: RENAME THIS METHOD!
    private void NoInputAction()
    {
        _holdTimer = 0;
        StopCoroutine(_slowMotionCourutine);
        _timeScaleController.SetTimeScale(1);
        if (joystickControls)
        {
            joystickControls = false;
            _rb2D.velocity = new Vector2(0, 0);
            _rb2D.AddForce(joystickForceDirection, ForceMode2D.Impulse);
            forceDirection.x = 3 * Mathf.Sign(joystickForceDirection.x);
        }
        else
        {
            transform.LookAt2d(new Vector3(-Mathf.Sign(forceDirection.x), -Mathf.Sign(forceDirection.y), 0), 15f);
        }

        joystick.background.gameObject.SetActive(false);
    }

    private void JumpOnTouch(Vector3 mousePosition)
    {
        _timeScaleController.SetTimeScale(1);
        if (joystickControls == false)
        {
            mousePos = mousePosition;
            _rb2D.velocity = new Vector2(0, 0);

            if (mousePos.x > _screenSizeProvider.screenHalfWidth)
            {
                forceDirection.x = Mathf.Abs(forceDirection.x);
            }
            else
            {
                forceDirection.x = -Mathf.Abs(forceDirection.x);
            }
            _rb2D.AddForce(forceDirection, ForceMode2D.Impulse);
        }
    }

    private void JoystickControl()
    {
        _holdTimer += _timeScaleController.deltaTime;
        if (_holdTimer >= _holdToActivate)
        {
            joystick.background.gameObject.SetActive(true);
            if (_timeScaleController.timeScale >= 1)
            {
                StartCoroutine(_slowMotionCourutine);
            }
            joystickControls = true;
        }
        
    }

    private void JoystickForceDirection(Vector3 lookVec)
    {
        joystickForceDirection = new Vector2(-lookVec.x, -lookVec.y).normalized * 8f;
    }
}