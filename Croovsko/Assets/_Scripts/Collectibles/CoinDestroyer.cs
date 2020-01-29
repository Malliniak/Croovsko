using System.Collections;
using DefaultNamespace;
using TMPro;
using UnityEngine;

public class CoinDestroyer : MonoBehaviour
{
    private static int pointsCollected = 0;
    public int _amountToAdd = 5;
    private bool _shouldDis;
    private TextMeshProUGUI _text;

    delegate void CoinPickUp();

    private CoinPickUp _coinPickUp;

    private void Start()
    {
        StartCoroutine(nameof(ColliderDisable));
        _text = GameObject.Find("COINS_TEXT").GetComponent<TextMeshProUGUI>();
        _coinPickUp += AddPoints;
        _coinPickUp += updateText;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IInputResponder player = other.GetComponent<IInputResponder>();
        if (player != null)
        {
            _coinPickUp();
            Destroy(GetComponentInParent<CoinController>().gameObject);
        }
    }

    private IEnumerator ColliderDisable()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        _shouldDis = true;
    }

    private void AddPoints()
    {
        CoinDestroyer.pointsCollected += _amountToAdd;
    }

    private void updateText()
    {
        _text.text = $"Points {CoinDestroyer.pointsCollected}";
    }
}