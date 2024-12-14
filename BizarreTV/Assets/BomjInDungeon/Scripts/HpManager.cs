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

        public int dangerScale = 0;

        void Start()
        {
            maxhp += 2 * dangerScale;
            currenthp = maxhp;
        }
        public void Restart()
        {
            maxhp += 2 * dangerScale;
            currenthp = maxhp;
        }
        private void Update()
        {
            if (currenthp <= 0)
            {
                var a = GetComponent<DeathEvent>();
                if (a != null)
                {
                    a.Death();
                }
                if (gameObject.tag == "Player")
                {
                    EventManager.Instance.TriggerEvent("PlayerDead");
                }
                Destroy(gameObject);
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
