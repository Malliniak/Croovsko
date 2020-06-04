using System;
using _Scripts.Variables;
using TMPro;
using UnityEngine;

public class IntTextController : MonoBehaviour
{
    private TextMeshProUGUI _text;

    [SerializeField] private StringReference _prefix;
    [SerializeField] private IntReference _variableToDisplay;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        _text.text = $"{_prefix.Value} {_variableToDisplay.Value}";
    }
}