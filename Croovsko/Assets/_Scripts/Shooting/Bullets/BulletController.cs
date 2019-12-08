using UnityEngine;

namespace _Scripts.Shooting.Bullets
{
    public class BulletController : MonoBehaviour
    {
        public Bullet bulletSo;
        private Rigidbody2D rb;

        private void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            Destroy(gameObject, bulletSo.bulletLifetime);
        }

        private void Update()
        {
            bulletSo.Move(rb);
        }
    }
}