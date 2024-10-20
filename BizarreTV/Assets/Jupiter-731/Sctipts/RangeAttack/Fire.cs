using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jupiter731
{
    public class Fire : MonoBehaviour
    {
        [SerializeField] Transform firePoint;
        [SerializeField] GameObject bulletPrefab;
        [SerializeField] float bulletSpeed;
        [SerializeField] float bulletLifeTime;
        [SerializeField] float bulletDamage;
        [SerializeField] BaseAnimator animator;


        public void OpenFire()
        {
            animator.PlayAnimations();
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Cos(Mathf.Deg2Rad * firePoint.rotation.eulerAngles.z) *bulletSpeed,
                Mathf.Sin(Mathf.Deg2Rad * firePoint.rotation.eulerAngles.z) * bulletSpeed);
            var baseBullet = bullet.GetComponent<BaseBullet>();
            baseBullet.LifeTime = bulletLifeTime;
            baseBullet.BulletDamage = bulletDamage;
        }
    }
}