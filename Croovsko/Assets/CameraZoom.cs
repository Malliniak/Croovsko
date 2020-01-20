using System;
using System.Collections;
using System.Collections.Generic;
using DigitalRuby.Tween;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private float _zoomValue;
    [SerializeField] private float _zoomTime;

    private float _zoomInValue;
    private float _zoomOutValue;
    private float _ortographicSize;
    private bool alreadyBeenTouched = false;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
        _ortographicSize = _camera.orthographicSize;
        _zoomInValue = _ortographicSize - _zoomValue;
        _zoomOutValue = _ortographicSize;
    }

    public void ZoomOutCamera()
    {

        Action<ITween<float>> CameraZoomOut = t =>
        {
            _camera.orthographicSize = t.CurrentValue;
        };
        
        _camera.gameObject.Tween("ZoomOut", _camera.orthographicSize, _zoomOutValue, _zoomTime, TweenScaleFunctions.CubicEaseInOut, CameraZoomOut);
        alreadyBeenTouched = false;

    }
    
    public void ZoomInCamera()
    {
        if (alreadyBeenTouched)
            return;
        Action<ITween<float>> CameraZoomIn = t =>
        {
            _camera.orthographicSize = t.CurrentValue;
        };
        
        _camera.gameObject.Tween("Zoom", _camera.orthographicSize, _zoomInValue, _zoomTime, TweenScaleFunctions.CubicEaseInOut, CameraZoomIn);
        alreadyBeenTouched = true;

    }
}
