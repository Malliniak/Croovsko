using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerParticleEmitter : MonoBehaviour
{
    private ParticleSystem _particle;

    private void Awake()
    {
        _particle = GetComponentInChildren<ParticleSystem>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<BoxController>() != null)
            return;
    
        _particle.gameObject.transform.position = new Vector3(other.contacts[0].point.x,other.contacts[0].point.y, 0);
        _particle.gameObject.transform.rotation = Quaternion.Euler(0, 0,other.gameObject.transform.localRotation.eulerAngles.z);
        Debug.Log(other.gameObject.transform.localRotation.eulerAngles, other.gameObject);
        _particle.Emit(25);
    }
}
