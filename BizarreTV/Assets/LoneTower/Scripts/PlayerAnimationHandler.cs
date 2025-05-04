using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LT
{
    public class PlayerAnimationHandler : MonoBehaviour
    {
        public Player Archer;
        public AudioSource aim;
        public AudioSource shoot;
        public void Aim(int boolean)
        {
            if(boolean == 0)
            {
                aim.pitch = 1 - Random.Range(-0.05f, 0.05f);
                aim.Play();
            }
            Archer.Aim(boolean == 1);
        }
        public void Ready()
        {
            Archer.Ready();
        }
        public void Shoot()
        {
            shoot.pitch = 1 - Random.Range(-0.05f, 0.05f);
            shoot.Play();
            Archer.Shoot();
        }
    }
}