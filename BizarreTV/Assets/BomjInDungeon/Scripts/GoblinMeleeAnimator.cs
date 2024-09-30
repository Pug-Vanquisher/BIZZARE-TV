using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BID
{
    public class GoblinMeleeAnimator : MonoBehaviour
    {
        public EnemyAi gmm;
        public Animator animator;
        void Start()
        {
            gmm = gameObject.GetComponent<EnemyAi>();
        }
        void Update()
        {
            if(gmm.velik != Vector2.zero)
            {
                animator.SetFloat("X", gmm.velik.x);
                animator.SetFloat("Y", gmm.velik.y);
            }
            animator.SetFloat("Speed", gmm.velik.magnitude);
        }
    }

}