using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plusSize : MonoBehaviour
{
    [SerializeField] gameMAnager gameManager;
    [SerializeField] List<Sprite> sprites;
    private bool hasReal;
    public float speed = 2;

    public Vector2 direction = new Vector2(0, 1);
    public Vector2 velocity;

    public int randomBuff;

    private void Awake()
    {
        gameManager = FindObjectOfType<gameMAnager>();
        randomBuff = Random.Range(0, 6);
        randomBuff = gameManager.ckeckBuff(randomBuff);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[randomBuff];
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
        if(collision.gameObject.tag == "Player")
        {
            switch (randomBuff)
            { 
                case 0:
                    gameManager.plusSize();
                    break;
                case 1: 
                    gameManager.minusSize();
                    break;
                case 2:
                    gameManager.resetSize();
                    break;
                case 3:
                    gameManager.addSpeed();
                    break;
                case 4:
                    gameManager.removeSpeed();
                    break;
                case 5:
                    gameManager.resetSpeed();
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
