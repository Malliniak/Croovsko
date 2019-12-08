using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Helpers;
using UnityEditor.VersionControl;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    public int amountToAdd = 5;
    public IntVariable PointsRuntimeVariable;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        AssetLoader.GetAssetFile(out PointsRuntimeVariable, "PointsRuntime");
    }

    public void AddForce(Vector2 force)
    {
        _rigidbody.AddForce(force, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<LeftRightController>())
        {
            Debug.Log("Add Points Here");
            PointsRuntimeVariable.AddToValue(amountToAdd);
            Destroy(gameObject);
        }
    }
}
