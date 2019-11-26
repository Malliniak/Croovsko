using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class LeftRightController : MonoBehaviour
{
    private ScreenSizeProvider _screenSizeProvider;
    private TimeScaleController _timeScaleController;
    
    private Vector2 forceDirection = new Vector2(3f, 6f);
    [SerializeField]
    private Vector2Reference mousePos;
    private Rigidbody2D _rb2D;

    [SerializeField] private float _holdTimer, _holdToActivate = 1f;
    [Range(0.1f, 1f)] public float slowMotionValue;
    
    private bool joystickControls;

    public DynamicJoystick joystick;

    private IEnumerator _slowMotionCoroutine;

    private void Awake()
    {
        _screenSizeProvider = new ScreenSizeProvider();
        _timeScaleController = new TimeScaleController();
        _rb2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _slowMotionCoroutine = _timeScaleController.ScaleTimeOverTime(1, slowMotionValue, 0.5f);
    }

    //TODO: RENAME THIS METHOD!
    public void NoInputAction()
    {
        _holdTimer = 0;
        _timeScaleController.SetTimeScale(1);
        if (joystickControls)
        {
            joystickControls = false;
            Debug.Log("ADDING AFTER JOUSTICK");
            AddForceToPlayer();
            StopCoroutine(_slowMotionCoroutine);
        }
        else
        {
            transform.LookAt2d(new Vector3(Mathf.Sign(-forceDirection.x), Mathf.Sign(forceDirection.y), 0), 15f);
        }

        joystick.background.gameObject.SetActive(false);
    }

    public void JumpOnTouch()
    {
        _timeScaleController.SetTimeScale(1);
        if (joystickControls == false)
        {
            Debug.Log("ADDING NORMAL");
            forceDirection.x = mousePos.Value.x > _screenSizeProvider.screenWidth / 2 ? 3 : -3;
            AddForceToPlayer();
        }
    }

    private void AddForceToPlayer()
    {
        _rb2D.velocity = new Vector2(0, 0);
        _rb2D.AddForce(forceDirection, ForceMode2D.Impulse);
    }

    public void JoystickControl()
    {

        _holdTimer += _timeScaleController.deltaTime;
        if (_holdTimer >= _holdToActivate)
        {
            var lookVec = new Vector3(joystick.input.x, joystick.input.y, 0);
            transform.LookAt2d(lookVec);
            forceDirection.x = lookVec.x > 0 ? -3 : 3;

            joystick.background.gameObject.SetActive(true);
            if (_timeScaleController.timeScale >= 1)
            {
                StartCoroutine(_slowMotionCoroutine);
            }
            joystickControls = true;
        }
        
    }
}