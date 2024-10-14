using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jupiter731
{
    public class BaseAnimator : MonoBehaviour
    {
        [SerializeField] Animator animator;
        [SerializeField] float animationTime;
        [SerializeField] string triggerName = "MeleeAttack";
        public void PlayAnimations()
        {
            animator.SetTrigger(triggerName);
            StartCoroutine(EndAnimation());
        }

        IEnumerator EndAnimation()
        {
            yield return new WaitForSeconds(animationTime);
            animator.ResetTrigger(triggerName);
        }
    }
}
