using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BID
{
    public class GoblinAnimator : MonoBehaviour
    {
        public Goblin goblin;
        public Animator animator;
        void Start()
        {
            goblin = gameObject.GetComponent<Goblin>();
        }
        void Update()
        {
            if(goblin.velik != Vector2.zero)
            {
                animator.SetFloat("X", goblin.velik.x);
                animator.SetFloat("Y", goblin.velik.y);
            }
            animator.SetFloat("Speed", goblin.velik.magnitude);
            animator.SetBool("Attack", goblin.IsInvoking("Attack"));
        }
    }

}