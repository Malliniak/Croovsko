using System.Collections;
using _Scripts.Helpers;
using UnityEngine;

public class LeftRightController : MonoBehaviour
{
    [Header("Joystick and SloMo")] private float _holdTimer;

    [SerializeField] private float _holdToActivate = 1f;

    private bool _joystickControls;

    [SerializeField] private Vector2Variable _mousePosition;

    private Rigidbody2D _rb2D;
    private ScreenSizeProvider _screenSizeProvider;
    private IEnumerator _slowMotionCoroutine;
    private TimeScaleController _timeScaleController;
    [SerializeField] private Vector2 _forceDirection = new Vector2(3f, 6f);

    [SerializeField] private DynamicJoystick _joystick;
    [SerializeField] private int _slowMotionForce = 10;
    [SerializeField] [Range(0.1f, 1f)] private float _slowMotionValue;

    private void Awake()
    {
        _screenSizeProvider = new ScreenSizeProvider();
        _timeScaleController = new TimeScaleController();
        _rb2D = GetComponent<Rigidbody2D>();

        if (_mousePosition) return;
        AssetLoader.GetAssetFile(out _mousePosition, "MousePosition");
    }

    private void Start()
    {
        SetSlowMotionCoroutine();
    }

    private void SetSlowMotionCoroutine()
    {
        _slowMotionCoroutine =
            _timeScaleController.ScaleTimeOverTime(_timeScaleController.TimeScale, _slowMotionValue, 0.5f);
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

        _joystick.background.gameObject.SetActive(false);
    }

    public void JumpOnTouch()
    {
        _timeScaleController.SetTimeScale(1);
        if (_joystickControls == false)
        {
            Debug.Log("ADDING NORMAL");
            _forceDirection.x = _mousePosition._value.x > _screenSizeProvider.ScreenWidth / 2 ? 3 : -3;
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
            Vector3 lookVec = new Vector3(_joystick.input.x, _joystick.input.y, 0);
            transform.LookAt2d(lookVec);
            _forceDirection = lookVec.normalized * -_slowMotionForce;

            _joystick.background.gameObject.SetActive(true);
            if (_timeScaleController.TimeScale >= 1) StartCoroutine(_slowMotionCoroutine);
            _joystickControls = true;
        }
    }
}