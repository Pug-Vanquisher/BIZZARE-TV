using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LT
{
    public class ArcherAnimationHandler : MonoBehaviour
    {
        public Archer archer;
        public void Shoot()
        {
            archer.Shoot();
        }
        public void Reload()
        {
            archer.Reload();
        }
    }
}