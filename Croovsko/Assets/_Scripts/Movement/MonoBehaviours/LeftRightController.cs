using System;
using System.Collections;
using _Scripts.Helpers;
using DG.Tweening;
using UnityEngine;

public class LeftRightController : MonoBehaviour
{
    [SerializeField] public Vector2 _forceDirection = new Vector2(3f, 6f);
    [Header("Joystick and SloMo")] private float _holdTimer;
    [SerializeField] private DynamicJoystick _joystick;

    private bool _joystickControls;

    [SerializeField] private Vector2Variable _mousePosition;

    private Rigidbody2D _rb2D;
    private ScreenSizeProvider _screenSizeProvider;
    [SerializeField] private int _slowMotionForce = 10;
    [SerializeField] [Range(0.1f, 1f)] private float _slowMotionValue;
    private TimeScaleController _timeScaleController;
    private bool _alreadyHolding = false;

    private BulletSpawner _bulletSpawner;

    [SerializeField] public GameEvent _MilkShootEvent;
    public bool _shouldLookToRotation = false;

    private float flySpeed = 600f;
    [SerializeField] private ParticleSystem _pressurePArticleSystem;

    private bool _canShoot = true;
    
    private void Awake()
    {
        _bulletSpawner = GetComponentInChildren<BulletSpawner>();
        _screenSizeProvider = new ScreenSizeProvider();
        _timeScaleController = new TimeScaleController();
        _rb2D = GetComponent<Rigidbody2D>();
        _rb2D.gravityScale = 0;

        if (_mousePosition) return;
        AssetLoader.GetAssetFile(out _mousePosition, "MousePosition");
    }

    public void BlockShoot()
    {
        _canShoot = false;
    }

    public void UnlockShoot()
    {
        _canShoot = true;  
    }

    private void Update() {
        if (_shouldLookToRotation)
        {
            Vector2 v = _rb2D.velocity;
            var angle = Mathf.Atan2(v.y, v.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    public void AfterHoldTouch()
    {
        _joystick.background.gameObject.SetActive(false);
        _alreadyHolding = false;
        _timeScaleController.NormalTime(0.05f);
        _joystickControls = false;
        
        if (!_canShoot)
            return;

        Debug.Log("ADDING AFTER JOYSTICK");
        AddForceToPlayer(2.5f);
        
        _MilkShootEvent.Raise();
    }

    public void JumpOnTouch()
    {
        _rb2D.gravityScale = 2;
        if (!_canShoot)
            return;
        _timeScaleController.NormalTime(0.05f);
        if (_joystickControls == false)
        {
            Debug.Log("ADDING NORMAL");
            _forceDirection.x = _mousePosition._value.x > _screenSizeProvider.ScreenWidth / 2 ? 3 : -3;
            _forceDirection.y = 6;
            AddForceToPlayer(1.9f);
            _MilkShootEvent.Raise();
        }
    }

    public void NoInput()
    {
        transform.LookAt2d(-_forceDirection, 15f);
        _timeScaleController.SetTimeScale(1f);
    }

    private void AddForceToPlayer(float speed)
    {
        _rb2D.velocity = new Vector2(0, 0);
        _rb2D.AddForce(_forceDirection * speed, ForceMode2D.Impulse);
        _bulletSpawner.Shoot(-_forceDirection);
    }

    public void JoystickControl()
    {
        if (!_canShoot)
            return;
        if(!_alreadyHolding)
            _timeScaleController.SlowDownTime(_slowMotionValue, 0.3f);
        _alreadyHolding = true;
        Vector3 lookVec = new Vector3(_joystick.input.x, _joystick.input.y, 0);
        transform.LookAt2d(lookVec);
        _forceDirection = lookVec.normalized * -_slowMotionForce;

        _joystick.background.gameObject.SetActive(true);
        _joystickControls = true;
    }

    public void PressureRelease()
    {
        Debug.Log("PressureRelease");
        _rb2D.Sleep();
        _rb2D.WakeUp();
        _rb2D.AddRelativeForce (Vector2.up * flySpeed, ForceMode2D.Force);
        _rb2D.gravityScale = 0;
        _rb2D.mass = 0.1f;
        _rb2D.drag = 0;
        _rb2D.sharedMaterial.bounciness = 3f;
        _rb2D.freezeRotation = false;
        _shouldLookToRotation = true;
        _pressurePArticleSystem.Play();
        StartCoroutine(EnableMass());
    }

    IEnumerator EnableMass()
    {
        yield return new WaitForSeconds(3f);
        _shouldLookToRotation = false;
        _rb2D.drag = 1;
        _rb2D.mass = 1;
        _rb2D.gravityScale = 2;
        _rb2D.freezeRotation = true;
        _rb2D.sharedMaterial.bounciness = 0.5f;
        _pressurePArticleSystem.Stop();
    }

    private void OnDisable()
    {
        _rb2D.sharedMaterial.bounciness = 0.5f;
    }
}