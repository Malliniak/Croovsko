using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    private ParticleSystem _particle;

    [SerializeField] private Object _bulletPrefab;

    private void Awake()
    {
        _particle = GetComponent<ParticleSystem>();
    }

    public void Shoot(Vector2 direction)
    {
        var bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity) as GameObject;
        bullet.GetComponent<Bullet>().AddForce(direction);
        _particle.Emit(3);
    }
}
