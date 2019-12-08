using _Scripts.Helpers;
using UnityEngine;

namespace _Scripts.Shooting.ShootingControllers
{
    public class ShootingCowController : ShootingController
    {
        [SerializeField] private GameObject slowMoBullet;
        private Vector2Variable mousePos;
        public GameObject SlowMoBullet => slowMoBullet;

        private GameObject bulletInstance;

        public override void Shoot(GameObject bullet)
        {
            bulletInstance = Instantiate(bullet, bulletSpawnPoint.transform.position, Quaternion.identity);

            if (bullet == slowMoBullet)
            {
                AssetLoader.GetAssetFile(out mousePos, "MousePosition");
                if (Camera.main != null)
                    bulletInstance.transform.LookAt2d(bulletInstance.transform.position -
                                                Camera.main.ScreenToWorldPoint(mousePos._value));
            }
        }

        public void RotateBulletToward(Vector3 lookVector)
        {
            bulletInstance.transform.LookAt2d(lookVector);
        }
    }
}