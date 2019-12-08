using UnityEngine;

namespace _Scripts.Shooting.Bullets
{
    public abstract class Bullet : ScriptableObject
    {
        public float bulletBaseSpeed;
        public float bulletDamage;
        public float bulletLifetime;
        public GameObject destroyEffect;
        
        public abstract void Move(Rigidbody2D transform);
    }
}