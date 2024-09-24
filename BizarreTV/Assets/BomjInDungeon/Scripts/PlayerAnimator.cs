using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BID
{
    public class PlayerAnimator : MonoBehaviour
    {
        public PlayerMover pm;
        public Animator animator;
        void Start()
        {
            pm = gameObject.GetComponent<PlayerMover>();
        }
        void Update()
        {
            animator.SetFloat("X", pm.velik.x);
            animator.SetFloat("Y", pm.velik.y);
            animator.SetFloat("Speed", pm.velik.magnitude);
        }
    }

}