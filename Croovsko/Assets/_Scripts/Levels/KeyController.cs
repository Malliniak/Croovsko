using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class KeyController : MonoBehaviour
{
    private Rigidbody2D _rigidbody;

    public GameObject Gate;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        Gate = GameObject.Find("Gate");
    }

    public void AddForce(Vector2 force)
    {
        _rigidbody.AddForce(force, ForceMode2D.Impulse);
        _rigidbody.simulated = false;
        StartCoroutine(nameof(Enable));
    }

    IEnumerator Enable()
    {
        yield return new WaitForSeconds(0.5f);
        _rigidbody.simulated = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<LeftRightController>())
        {
            Gate.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
