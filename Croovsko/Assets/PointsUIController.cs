using System.Collections;
using System.Collections.Generic;
using _Scripts.Helpers;
using TMPro;
using UnityEngine;

public class PointsUIController : MonoBehaviour
{
    private TextMeshProUGUI _text;
    private IntVariable _pointsRuntime;
    private int previousValue;

    private void Awake()
    {
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        AssetLoader.GetAssetFile(out _pointsRuntime, $"PointsRuntime");
        previousValue = _pointsRuntime._value;
        _text.text = $"{previousValue}";
    }

    private void Update()
    {
        if (_pointsRuntime._value != previousValue)
        {
            previousValue = _pointsRuntime._value;
            _text.text = $"{previousValue}";
        }
    }
}
