using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BID
{
    public class BossDoorScript : MonoBehaviour
    {
        public GameObject Gloom;
        public GameObject Door;
        private BoxCollider doorCollider;
        private SpriteRenderer doorSprite;
        private Animator doorAnimator;
        private SpriteRenderer gloomColor;
        public int orderZ;
        public float _duration;
        void Start()
        {
            doorCollider = Door.GetComponent<BoxCollider>();
            doorAnimator = Door.GetComponent<Animator>();
            doorSprite = Door.GetComponent<SpriteRenderer>();
            gloomColor = Gloom.GetComponent<SpriteRenderer>();
            doorSprite.sortingOrder = orderZ;
            gloomColor.sortingOrder = orderZ;
            if(DungeonLogic.keystone == "destroyed")
            {
                StartCoroutine("_Entrance");
            }
        }

        IEnumerator _Entrance()
        {
            float timeLeft = Time.time;

            while ((timeLeft + _duration) > Time.time)
            {
                gloomColor.color = new Color(gloomColor.color.r, gloomColor.color.g, gloomColor.color.b,
                    1 - Time.time / (timeLeft)); 
                yield return new WaitForSeconds(0.025f);
            }

            timeLeft = Time.time;
            doorAnimator.SetBool("open", true);

            while ((timeLeft + 0.5f) > Time.time)
            {
                yield return new WaitForSeconds(0.025f);
            }

            doorCollider.enabled = false;
            yield return 0;
        }
    }

}