using _Scripts.Variables;
using TMPro;
using UnityEngine;

public class StringTextController : MonoBehaviour
{
    private TextMeshProUGUI _text;

    [SerializeField] private StringReference _prefix;
    [SerializeField] private StringReference _variableToDisplay;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateText()
    {
        _text.text = $"{_prefix.Value} {_variableToDisplay.Value}";
    }
}