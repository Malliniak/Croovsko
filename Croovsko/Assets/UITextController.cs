using System.Collections;
using System.Collections.Generic;
using _Scripts.Variables;
using TMPro;
using UnityEngine;

public class UITextController : MonoBehaviour
{
    [SerializeField] private StringReference VariableToDisplay;

    [SerializeField] private StringReference Prefix;

    private TextMeshProUGUI _text;
    void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText()
    {
        _text.text = $"{Prefix.Value} {VariableToDisplay.Value}";
    }
}
