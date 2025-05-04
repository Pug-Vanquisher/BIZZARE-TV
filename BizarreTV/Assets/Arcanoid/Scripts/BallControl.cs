using UnityEngine;
using UnityEngine.SceneManagement;

namespace Arcanoid
{
    public class BallControl : MonoBehaviour
    {
        public Vector2 startingVelocity;

        AudioSource source;

        Vector2 bufferSpeed;
        public float speed = 3f;

        Rigidbody2D rgb;

        void Start()
        {
            rgb = GetComponent<Rigidbody2D>();
            source = GetComponent<AudioSource>();

            stp();
        }

        public void stp()
        {
            gameObject.isStatic = true;
        }
        public void str()
        {
            bufferSpeed = startingVelocity.normalized;
            gameObject.isStatic = false;
            rgb.velocity = bufferSpeed * speed; 
        }

        private void FixedUpdate()
        {
            Vector2 movement = bufferSpeed * speed * Time.deltaTime;
            rgb.MovePosition(rgb.position + movement);

            float rotationSpeed = rgb.velocity.magnitude * 100f; 
            rgb.angularVelocity = -rotationSpeed;
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            Vector2 norm = Vector2.zero;

            norm += collision.contacts[0].normal;

            norm.Normalize();

            bufferSpeed = Vector2.Reflect(bufferSpeed, norm);

            if (collision.gameObject.name.Contains("Player"))
            {
                bufferSpeed += collision.gameObject.GetComponent<Platform>().currspeed * 0.1f;
                bufferSpeed.Normalize();
            }

            source.Play();

            if (collision.gameObject.name == "LoseBorder")
            {
                //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //удалить.
                Destroy(gameObject);
            }

            if (collision.gameObject.name.Contains("Block"))
            {
                collision.gameObject.GetComponent<Block>().Hit();
            }

        }


    }
}


