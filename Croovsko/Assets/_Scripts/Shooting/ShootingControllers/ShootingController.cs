using UnityEngine;

namespace _Scripts.Shooting.ShootingControllers
{
    public abstract class ShootingController : MonoBehaviour
    {
        [SerializeField] protected GameObject bulletSpawnPoint;
        
        [SerializeField] protected GameObject normalBullet;
        public GameObject NormalBullet => normalBullet;
        
        public abstract void Shoot(GameObject bullet);
    }
}