using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    private Vector3 moveDirection;

    void Awake()
    {
        Destroy(gameObject, 5f); // Самоуничтожение через 5 секунд
    }

    public void SetDirection(Vector3 dir)
    {
        moveDirection = dir;
    }

    void Update()
    {
        transform.position += moveDirection * (speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Debug.Log("Hit");
            Rogalik.Scripts.Player.RoguelikePlayer playerScript = other.gameObject.GetComponent<Rogalik.Scripts.Player.RoguelikePlayer>();
            if (playerScript != null)
            {
                playerScript.GetDamage(1);
            }
            Destroy(gameObject);
        }
    }
}
