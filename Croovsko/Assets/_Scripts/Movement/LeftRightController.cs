//
//  Square
//
//  Created by Krzysztof Maliński, Adam Kolinski, 
// Damian Klabuhn, Mikolaj Mikolajczak
//  
// Code inspired by Croovsko, project by Krzysztof Malinski
// 


using _Scripts.Helpers;
using DefaultNamespace;
using UnityEngine;

public class LeftRightController : MonoBehaviour, IInputResponder
{
    private bool _alreadyHolding;

    private BulletSpawner _bulletSpawner;
    [SerializeField] private Vector2 _forceDirection = new Vector2(3f, 6f);
    [Header("Joystick and SloMo")] private float _holdTimer;
    [SerializeField] private DynamicJoystick _joystick;

    private bool _joystickControls;

    [SerializeField] private Vector2Variable _mousePosition;

    private Rigidbody2D _rb2D;
    private ScreenSizeProvider _screenSizeProvider;
    [SerializeField] private int _slowMotionForce = 10;
    [SerializeField] [Range(0.1f, 1f)] private float _slowMotionValue;
    private TimeScaleController _timeScaleController;

    public void AfterHoldTouch()
    {
        _alreadyHolding = false;
        _timeScaleController.NormalTime(0.05f);
        _joystickControls = false;
        Debug.Log("ADDING AFTER JOYSTICK");
        AddForceToPlayer(2f);

        _joystick.background.gameObject.SetActive(false);
    }

    public void OnTouchUp()
    {
        _timeScaleController.NormalTime(0.05f);
        if (_joystickControls == false)
        {
            Debug.Log("ADDING NORMAL");
            _forceDirection.x = _mousePosition._value.x > _screenSizeProvider.ScreenWidth / 2 ? 3 : -3;
            _forceDirection.y = 6;
            AddForceToPlayer(1.2f);
        }
    }

    public void NoInput()
    {
        transform.LookAt2d(-_forceDirection, 15f);
        _timeScaleController.SetTimeScale(1f);
    }

    public void OnHold()
    {
        if (!_alreadyHolding)
            _timeScaleController.SlowDownTime(_slowMotionValue, 0.3f);
        _alreadyHolding = true;
        Vector3 lookVec = new Vector3(_joystick.input.x, _joystick.input.y, 0);
        transform.LookAt2d(lookVec);
        _forceDirection = lookVec.normalized * -_slowMotionForce;

        _joystick.background.gameObject.SetActive(true);
        _joystickControls = true;
    }

    private void Awake()
    {
        _bulletSpawner = GetComponentInChildren<BulletSpawner>();
        _screenSizeProvider = new ScreenSizeProvider();
        _timeScaleController = new TimeScaleController();
        _rb2D = GetComponent<Rigidbody2D>();

        if (_mousePosition) return;
        AssetLoader.GetAssetFile(out _mousePosition, "MousePosition");
    }

    private void AddForceToPlayer(float speed)
    {
        _rb2D.velocity = new Vector2(0, 0);
        _rb2D.AddForce(_forceDirection * speed, ForceMode2D.Impulse);
        _bulletSpawner.Shoot(-_forceDirection);
    }
}