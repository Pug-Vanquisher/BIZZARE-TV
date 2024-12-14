using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BID
{
    public class UIhealth : MonoBehaviour
    {
        private HpManager playerhealth;
        public GameObject heartPrefab;
        public List<GameObject> Hearts = new List<GameObject>();
        public int playerCurrentHealth;

        public float _shakeDuration = .4f;
        public float _magnitude = .4f;
        public Vector3 _originalPosition;
        public RectTransform origin;
        private bool isShaking;
        // Start is called before the first frame update
        void Start()
        {
            if (playerhealth == null)
            {
                playerhealth = GameObject.FindGameObjectWithTag("Player")?.GetComponent<HpManager>();
            }
            origin = transform.parent.GetComponent<RectTransform>();
            playerCurrentHealth = playerhealth.maxhp;
            for (int i = 0; i < playerhealth.maxhp; i += 2)
            {
                var a = Instantiate(heartPrefab, transform);
                a.GetComponent<HeartScript>().id = i;
                Hearts.Add(a);
            }

        }
        // Update is called once per frame
        void Update()
        {
            _originalPosition = new Vector3(-origin.rect.width/2, origin.rect.height/ 2, 0);
            if (playerCurrentHealth != playerhealth.currenthp)
            {
                playerCurrentHealth = playerhealth.currenthp;
                foreach(GameObject heart in Hearts)
                {
                    heart.GetComponent<HeartScript>().TakeDamage(playerCurrentHealth);
                }
                Shake();
            }
            if (!isShaking)
            {
                transform.localPosition = _originalPosition;
            }
        }
        public void Shake()
        {
            StartCoroutine(_Shake());
        }
        IEnumerator _Shake()
        {

            float x;
            float y;
            float timeLeft = Time.time;
            isShaking = true;
            while ((timeLeft + _shakeDuration) > Time.time)
            {
                x = Random.Range(-_magnitude, _magnitude) * timeLeft / Time.time;
                y = Random.Range(-_magnitude, _magnitude) * timeLeft / Time.time;
                transform.localPosition = new Vector3(x + _originalPosition.x, y + _originalPosition.y, _originalPosition.z); yield return new WaitForSeconds(0.025f);
            }

            transform.localPosition = _originalPosition;
            isShaking = false;
        }

    }
}
