using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LeftRightController : MonoBehaviour
{
    private ScreenSizeProvider _screenSizeProvider;
    private TimeScaleController _timeScaleController;
    
    [SerializeField]private Vector2 _forceDirection = new Vector2(3f, 6f);
    [SerializeField] private int slowMotionForce = 10;
    [SerializeField]
    private Vector2Reference _mousePos;
    private Rigidbody2D _rb2D;

    [Header("Joystick and SloMo")]
    private float _holdTimer;
    [SerializeField] private float _holdToActivate = 1f;
    [SerializeField][Range(0.1f, 1f)] private float _slowMotionValue;
    
    private bool _joystickControls;

    public DynamicJoystick Joystick;
    private IEnumerator _slowMotionCoroutine;

    private void Awake()
    {
        _screenSizeProvider = new ScreenSizeProvider();
        _timeScaleController = new TimeScaleController();
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        SetSlowMotionCoroutine();
    }

    private void SetSlowMotionCoroutine()
    {
        _slowMotionCoroutine = _timeScaleController.ScaleTimeOverTime(_timeScaleController.TimeScale, _slowMotionValue, 0.5f);
    }

    public void NoInputAction()
    {
        _holdTimer = 0;
        _timeScaleController.SetTimeScale(1);
        if (_joystickControls)
        {
            _joystickControls = false;
            Debug.Log("ADDING AFTER JOYSTICK");
            AddForceToPlayer();
            StopCoroutine(_slowMotionCoroutine);
            SetSlowMotionCoroutine();
        }
        else
        {
            transform.LookAt2d(-_forceDirection, 15f);
        }

        Joystick.background.gameObject.SetActive(false);
    }

    public void JumpOnTouch()
    {
        _timeScaleController.SetTimeScale(1);
        if (_joystickControls == false)
        {
            Debug.Log("ADDING NORMAL");
            _forceDirection.x = _mousePos.Value.x > _screenSizeProvider.ScreenWidth / 2 ? 3 : -3;
            _forceDirection.y = 6;
            AddForceToPlayer();
        }
    }

    private void AddForceToPlayer()
    {
        _rb2D.velocity = new Vector2(0, 0);
        _rb2D.AddForce(_forceDirection, ForceMode2D.Impulse);
    }

    public void JoystickControl()
    {
        _holdTimer += _timeScaleController.DeltaTime;
        if (_holdTimer >= _holdToActivate)
        {
            var lookVec = new Vector3(Joystick.input.x, Joystick.input.y, 0);
            transform.LookAt2d(lookVec);
            _forceDirection = lookVec.normalized * -slowMotionForce;

            Joystick.background.gameObject.SetActive(true);
            if (_timeScaleController.TimeScale >= 1 )
            {
                StartCoroutine(_slowMotionCoroutine);
            }
            _joystickControls = true;
        }
        
    }
}