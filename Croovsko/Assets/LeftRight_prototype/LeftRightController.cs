using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRightController : MonoBehaviour
{

    public Vector2 forceDirection;

    private float _screenWidth, _halfScreenWidth;
    private Vector2 mousePos;
    private Rigidbody2D _rb2D;

    [SerializeField]
    private float _holdTimer, _holdToActivate = 1f;

    [Range(0.1f, 1f)]
    public float slowMotionValue;

    [SerializeField]
    private float currentTimeScale;

    void Start()
    {
        _rb2D = GetComponent<Rigidbody2D>();

        _screenWidth = Screen.width;
        _halfScreenWidth = _screenWidth / 2f;
        Debug.Log($"Screen width: {_screenWidth}");
    }


    void FixedUpdate()
    {
        if (Input.GetMouseButton(0))
        {
            _holdTimer += Time.deltaTime;

            if (Input.GetMouseButtonDown(0) && _holdTimer < _holdToActivate)
            {
                mousePos = Input.mousePosition;

                _rb2D.velocity = new Vector2(0, 0);

                if (mousePos.x < _halfScreenWidth)
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
                Debug.Log("Free shot");
                if (Time.timeScale >= 1)
                {
                    StartCoroutine(ScaleTime(1, slowMotionValue, 0.5f));
                }
                //Time.timeScale = Mathf.Lerp(Time.timeScale, slowMotionValue, Time.unscaledDeltaTime * 15f);
                //Time.timeScale = slowMotionValue;
                _holdTimer = 0;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            _holdTimer = 0;
            Time.timeScale = 1;
        }

        currentTimeScale = Time.timeScale;
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
    }

}
