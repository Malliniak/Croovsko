using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceWall : MonoBehaviour
{

    public bool _isHorizontal;
    private void OnCollisionEnter2D(Collision2D other)
    {
        LeftRightController player = other.gameObject.GetComponent<LeftRightController>();

        var vector = _isHorizontal ? Vector2.left : Vector2.down;
        if (player != null && other.rigidbody.freezeRotation)
        {
            Debug.Log("Bum bum wall bounce");
            other.rigidbody.AddForce(Vector2.Reflect(player._forceDirection, vector) * 3.5f , ForceMode2D.Impulse);
            player._forceDirection.x = player._forceDirection.x == 3 ? -3 : 3;
            player._MilkShootEvent.Raise();
        }
    }
}
