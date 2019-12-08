using UnityEngine;

namespace _Scripts.Shooting.Bullets
{
    [CreateAssetMenu(fileName = "Forward Bullet", menuName = "Bullets/Forward Bullet", order = 1)]
    public class ForwardBullet : Bullet
    {
        public override void Move(Rigidbody2D rb)
        {
            rb.velocity = bulletBaseSpeed * rb.transform.up;
        }
    }
}