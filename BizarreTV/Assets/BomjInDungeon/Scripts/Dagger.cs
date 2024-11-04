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
        public Collider2D creator;
        private Collider2D hited;

        public GameObject HitParticle;
        void Start()
        {
            startime = Time.time;
        }

        void Update()
        {
            if (Time.time >= startime + lifetime) { Destroy(gameObject); }
            transform.position += transform.right * Time.deltaTime * speed;
            if(hited == null)
            {
                GetComponent<SpriteRenderer>().sortingOrder = (int)(transform.position.y * -10);
            }
            else
            {
                GetComponent<SpriteRenderer>().sortingOrder = hited.transform.parent.GetComponent<SpriteRenderer>().sortingOrder - 1;
            }

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(creator != collision)
            {
                if (collision.gameObject.layer == 16)
                {
                    if(speed == 1 && collision.gameObject.tag == "Goblin" && creator.gameObject.tag == "Goblin")
                    {

                    }
                    else
                    {
                        Instantiate(HitParticle, transform.position + transform.right * 3, Quaternion.identity);
                        collision.gameObject.GetComponent<HpManager>().TakeDamage(damage);
                        transform.parent = collision.gameObject.transform;
                        if (GetComponent<ParticleSystem>() != null)
                        {
                            GetComponent<ParticleSystem>().Stop();
                        }
                        speed = 0;
                        gameObject.GetComponent<BoxCollider2D>().enabled = false;
                        hited = collision;
                        startime = Time.time;
                    }
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

}