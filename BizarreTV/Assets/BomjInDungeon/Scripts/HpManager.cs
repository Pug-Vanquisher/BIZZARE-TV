using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BID
{
    public class HpManager : MonoBehaviour
    {
        public int maxhp;
        public int currenthp;
        public float invulDelay;
        private bool invulnerability = false;
        void Start()
        {
            currenthp = maxhp;
        }

        private void Update()
        {
            if (currenthp <= 0) 
            {
                if (gameObject.name != "Bomj") { Destroy(gameObject); }

                else
                {
                    Debug.Log("я здох :(");
                    currenthp = maxhp;
                }
            }
        }
        public virtual void TakeDamage(int damage)
        {
            if (!invulnerability)
            {
                currenthp -= damage;
                invulnerability = true;
                Invoke("DamageTaken", invulDelay);
            }
        }
        void DamageTaken()
        {
            invulnerability = false;
        }

    }
}
