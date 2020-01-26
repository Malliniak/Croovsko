using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Helpers;
using UnityEngine;

public class CoinDestroyer : MonoBehaviour
{

    private bool shouldDis;
    private LeftRightController _player;
    public int amountToAdd = 5;
    public IntVariable PointsRuntimeVariable;

    private void Start()
    {
        StartCoroutine(nameof(ColliderDisable));
        AssetLoader.GetAssetFile(out PointsRuntimeVariable, "PointsRuntime");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<LeftRightController>())
        {
            _player = other.GetComponent<LeftRightController>();
            Debug.Log("Add Points Here");
            PointsRuntimeVariable.AddToValue(amountToAdd);
            Destroy(GetComponentInParent<CoinController>().gameObject);

        }
    }
    
    private IEnumerator ColliderDisable()
    {
        yield return new WaitForSecondsRealtime(0.6f);
        shouldDis = true;
    }
}
