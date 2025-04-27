using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LT
{
    public class PlayerAnimationHandler : MonoBehaviour
    {
        public Player Archer;
        public void Aim(int boolean)
        {
            Archer.Aim(boolean == 1);
        }
        public void Ready()
        {
            Archer.Ready();
        }
        public void Shoot()
        {
            Archer.Shoot();
        }
    }
}