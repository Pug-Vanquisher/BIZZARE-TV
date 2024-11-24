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

        public int dangerScale = 1;

        void Start()
        {
            maxhp += 2 * dangerScale;
            currenthp = maxhp;
        }

        private void Update()
        {
            if (currenthp <= 0) 
            {
                if (gameObject.name != "Bomj") 
                {
                    Destroy(gameObject);
                    var a = GetComponent<DeathEvent>();
                    if(a != null)
                    {
                        a.Death();
                    }
                }

                else
                {
                    Debug.Log("� ���� :(");
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