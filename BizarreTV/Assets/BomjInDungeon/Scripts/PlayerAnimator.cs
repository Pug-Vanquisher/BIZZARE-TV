using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BID
{
    public class PlayerAnimator : MonoBehaviour
    {
        public PlayerMover pm;
        public Animator animator;
        public SpriteRenderer sr;
        public ParticleSystem dashParticles;
        void Start()
        {
            pm = gameObject.GetComponent<PlayerMover>();
        }
        void Update()
        {
            dashParticles.textureSheetAnimation.SetSprite(0, sr.sprite);
            if(pm.velik != Vector2.zero)
            {
                animator.SetFloat("X", pm.velik.x);
                animator.SetFloat("Y", pm.velik.y);
            }
            animator.SetFloat("Speed", pm.velik.magnitude);
        }
    }

}