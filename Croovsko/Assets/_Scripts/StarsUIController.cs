using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Helpers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StarsUIController : MonoBehaviour
{
    [SerializeField] private List<Sprite> images = new List<Sprite>();
    private UnityEngine.UI.Image _image;
    private IntVariable _bellsRuntime;
    private int previousValue;

    private void Awake()
    {
        _image = GetComponent<UnityEngine.UI.Image>();
    }

    private void Start()
    {
        AssetLoader.GetAssetFile(out _bellsRuntime, $"BellsRuntime");
        previousValue = _bellsRuntime._value;
        images.Sort((p1, p2) => String.Compare(p1.name, p2.name, StringComparison.Ordinal));
        _image.sprite = images[previousValue];
    }

    private void Update()
    {
        if (_bellsRuntime._value != previousValue)
        {
            previousValue = _bellsRuntime._value;
            _image.sprite = images[previousValue];
        }
    }
}
