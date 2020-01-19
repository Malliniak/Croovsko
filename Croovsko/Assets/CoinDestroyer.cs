using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        print("pong");
        if (other.GetComponent<LeftRightController>())
        {
            print("pong");
            Destroy(GetComponentInParent<CoinController>().gameObject);
        }
    }
}
