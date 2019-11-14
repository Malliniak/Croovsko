using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightController : MonoBehaviour
{

    public Vector2 forceDirection;

    private float _screenWidth, _screenHeight, _halfScreenWidth, _halfScreenHeight;
    private Vector2 mousePos;
    private Rigidbody2D _rb2D;

    [SerializeField]
    private float _holdTimer, _holdToActivate = 1f;

    [Range(0.1f, 1f)]
    public float slowMotionValue;

    [SerializeField]
    private float currentTimeScale;

    private bool joystickControls = false;

    public Vector2 analogForceDirection;

    public DynamicJoystick joystick;

    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();

        _screenWidth = Screen.width;
        _halfScreenWidth = _screenWidth / 2f;

        _screenHeight = Screen.height;
        _halfScreenHeight = _screenHeight / 2f;

        Debug.Log($"Screen width: {_screenWidth}");
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            _holdTimer += Time.deltaTime;

            if (Input.GetMouseButtonDown(0) && _holdTimer < _holdToActivate)
            {
                mousePos = Input.mousePosition;

                _rb2D.velocity = new Vector2(0, 0);

                if (mousePos.x > _halfScreenWidth)
                {
                    forceDirection.x = Mathf.Abs(forceDirection.x);
                }
                else
                {
                    forceDirection.x = -Mathf.Abs(forceDirection.x);
                }
                _rb2D.AddForce(forceDirection, ForceMode2D.Impulse);
            }
            else if(_holdTimer >= _holdToActivate)
            {
                joystick.background.gameObject.SetActive(true);
                if (Time.timeScale >= 1)
                {
                    StartCoroutine(ScaleTime(1, slowMotionValue, 0.5f));
                }
                _holdTimer = 0;
                joystickControls = true;
            }
        }
        if (!Input.GetMouseButton(0))
        {
            _holdTimer = 0;
            Time.timeScale = 1;
            if (joystickControls)
            {
                joystickControls = false;
                _rb2D.velocity = new Vector2(0, 0);
                _rb2D.AddForce(analogForceDirection, ForceMode2D.Impulse);
                forceDirection.x = 3 * Mathf.Sign(analogForceDirection.x);
            } else
            {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(0, 0, 45 * -Mathf.Sign(forceDirection.x)), Time.deltaTime * 15f);
            }
            joystick.background.gameObject.SetActive(false);
        }

        if (joystickControls)
        {
            JoystickControl();
        }

        currentTimeScale = Time.timeScale;
        
    }

    private void JoystickControl()
    {
        //transform.rotation = Quaternion.Euler(joystick.difference);
        LookAt(new Vector3(joystick.input.x, joystick.input.y, 0));
    }

    public void LookAt(Vector3 lookVec)
    {
        Debug.DrawRay(transform.position, lookVec, Color.red, 0.5f);
        //Debug.Log(lookVec);
        float _rot_z;

        _rot_z = Mathf.Atan2(lookVec.y, lookVec.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, _rot_z + 90f);

        analogForceDirection = new Vector2(-lookVec.x, -lookVec.y).normalized * 8f;
        //tmp.x = Remap(tmp.x, 0, 1, 0, forceDirection.x);
        //tmp.x = Remap(tmp.y, 0, 1, 0, forceDirection.y);
    }

    IEnumerator ScaleTime(float start, float end, float time)     //not in Start or Update
    {
        float lastTime = Time.realtimeSinceStartup;
        float timer = 0.0f;

        while (timer < time)
        {
            Time.timeScale = Mathf.Lerp(start, end, timer / time);
            timer += (Time.realtimeSinceStartup - lastTime);
            lastTime = Time.realtimeSinceStartup;
            yield return null;
        }

        Time.timeScale = end;
        Time.fixedDeltaTime = end * 0.02f;
    }

    public  float Remap(float from, float fromMin, float fromMax, float toMin, float toMax)
    {
        var fromAbs = from - fromMin;
        var fromMaxAbs = fromMax - fromMin;

        var normal = fromAbs / fromMaxAbs;

        var toMaxAbs = toMax - toMin;
        var toAbs = toMaxAbs * normal;

        var to = toAbs + toMin;

        return to;
    }

}
