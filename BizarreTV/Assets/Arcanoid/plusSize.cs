using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arcanoid
{
    public class plusSize : MonoBehaviour
    {
        [SerializeField] gameMAnager gameManager;
        [SerializeField] List<Sprite> sprites;
        private bool hasReal;
        public float speed = 2;

        public Vector2 direction = new Vector2(0, 1);
        public Vector2 velocity;

        public int buff;
        public string type;

        void Start()
        {
            gameManager = FindObjectOfType<gameMAnager>();
            
            if (type.Contains("Coal Block")) buff = 0;
            if (type.Contains("Iron Block")) buff = 1;
            if (type.Contains("Gold Block")) buff = 2;
            if (type.Contains("Diamond Block")) buff = 3;
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[buff];
        }


        private void FixedUpdate()
        {
            velocity = direction * speed;
            Vector2 pos = transform.position;
            pos -= velocity * Time.deltaTime;
            transform.position = pos;

            if (!hasReal)
            {
                Destroy(gameObject);
            }

        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                switch (buff)
                {
                    case 0:
                        gameManager.plusSize();
                        break;
                    case 1:
                        gameManager.addBalls();
                        break;
                    case 2:
                        gameManager.addScore(true);
                        break;
                    case 3:
                        gameManager.addScore(false);
                        break;

                }
                Destroy(gameObject);
            }
        }

        private void OnBecameInvisible()
        {
            hasReal = false;
        }

        private void OnBecameVisible()
        {
            hasReal = true;
        }
    }

}