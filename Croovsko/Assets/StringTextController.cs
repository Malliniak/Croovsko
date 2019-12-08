using _Scripts.Variables;
using TMPro;
using UnityEngine;

public class UiTextController : MonoBehaviour
{
    private TextMeshProUGUI _text;

    [SerializeField] private StringReference _prefix;
    [SerializeField] private  _variableToDisplay;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText()
    {
        _text.text = $"{_prefix.Value} {_variableToDisplay.Value}";
    }
}