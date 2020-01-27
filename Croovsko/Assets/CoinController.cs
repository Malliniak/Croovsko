using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Helpers;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    private bool _shouldMoveToPlayer;
  

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void AddForce(Vector2 force)
    {
        _rigidbody.AddForce(force, ForceMode2D.Impulse);
    }
}
