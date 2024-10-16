using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BID
{
    public class Dagger : MonoBehaviour
    {
        public float lifetime = 1f;
        private float startime;
        public int speed;

        public int damage;
        void Start()
        {
            startime = Time.time;
        }

        void Update()
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
            if (Time.time >= startime + lifetime) { Destroy(gameObject); }
            transform.position += transform.right * Time.deltaTime * speed;

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ( collision.gameObject.layer == 16)
            {
                collision.gameObject.GetComponent<HpManager>().currenthp -= damage;
                transform.parent = collision.gameObject.transform;
                if(GetComponent<ParticleSystem>() != null)
                {
                    GetComponent<ParticleSystem>().Stop();
                }
                speed = 0;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                startime = Time.time;
            }
            else if (collision.gameObject.layer == 15)
            {
                transform.parent = collision.gameObject.transform;
                speed = 0;
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                startime = Time.time;
            }
        }
    }

}