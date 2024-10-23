using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BID
{
    public class HeartScript : MonoBehaviour
    {
        public float _duration = .4f;
        public int id;

        public Image damage;
        public Image health;

        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void TakeDamage(int HeartId)
        {
            Pulse();
            if (HeartId == id || HeartId == id + 1)
            {
                health.fillAmount -= 0.5f;
            }
            if(HeartId == id-1 || HeartId == id)
            {
                damage.fillAmount -= 0.5f;
            }
        }
        public void NonEffDamage(int HeartId)
        {
            if (HeartId == id || HeartId == id + 1)
            {
                health.fillAmount -= 0.5f;
            }
        }
        public void Cure(int HeartId)
        {
            if (HeartId >= id)
            {
                health.fillAmount = 0.5f;
            }
            else if(HeartId >= id + 1)
            {
                health.fillAmount = 1f;
            }
        }

        public void Pulse()
        {
            StartCoroutine(_Pulse());
        }

        IEnumerator _Pulse()
        {
            float timeLeft = Time.time;
            damage.color = Color.white;

            while ((timeLeft + _duration) > Time.time)
            {
                damage.color = Color.white * (timeLeft / Time.time); yield return new WaitForSeconds(0.025f);
            }
            damage.color = Color.black;
        }
    }
}