using UnityEngine;
using UnityEngine.SceneManagement;

public class BallControl : MonoBehaviour
{

    public Vector2 startingVelocity;
    public float correctionSpeed = 0.1f;

    AudioSource source;
    Rigidbody2D rigidbody;

    Vector2 bufferSpeed;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        
        stp();
    }

    public void stp()
    {
        this.gameObject.isStatic = true;
    }
    public void str()
    {
        rigidbody.velocity = startingVelocity;
        this.gameObject.isStatic = false;
    }

    private void FixedUpdate()
    {
        this.gameObject.GetComponent<Animation>().Play();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        source.Play();

        bufferSpeed = rigidbody.velocity;

        if (collision.gameObject.name == "LoseBorder")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        if (collision.gameObject.name.Contains("Block"))
        {
            collision.gameObject.GetComponent<Block>().Hit();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (rigidbody.velocity.x == 0 || rigidbody.velocity.y == 0)
        {
            if (rigidbody.velocity.x == 0)
                rigidbody.velocity = new Vector2(-(bufferSpeed.x + correctionSpeed), rigidbody.velocity.y);
            else
                rigidbody.velocity = new Vector2(rigidbody.velocity.x, -(bufferSpeed.y + correctionSpeed));
        }
    }
}
