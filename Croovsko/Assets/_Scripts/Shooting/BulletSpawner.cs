//
//  Square
//
//  Created by Krzysztof Maliński, Adam Kolinski, 
// Damian Klabuhn, Mikolaj Mikolajczak
//  
// Code inspired by Croovsko, project by Krzysztof Malinski
// 


using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Object _bulletPrefab;
    private ParticleSystem _particle;

    private void Awake()
    {
        _particle = GetComponent<ParticleSystem>();
    }

    public void Shoot(Vector2 direction)
    {
        GameObject bullet = Instantiate(_bulletPrefab, transform.position, Quaternion.identity) as GameObject;
        bullet.GetComponent<Bullet>().AddForce(direction);
        _particle.Emit(3);
    }
}