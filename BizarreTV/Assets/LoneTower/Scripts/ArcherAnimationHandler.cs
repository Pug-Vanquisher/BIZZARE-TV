using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LT
{
    public class ArcherAnimationHandler : MonoBehaviour
    {
        public Archer archer;

        public AudioSource aim;
        public AudioSource shoot;
        public void Aim()
        {
            aim.pitch = 1 - Random.Range(-0.05f, 0.05f);
            aim.Play();
        }
        public void Shoot()
        {
            shoot.pitch = 1 - Random.Range(-0.05f, 0.05f);
            shoot.Play();
            archer.Shoot();
        }
        public void Reload()
        {
            archer.Reload();
        }
    }
}