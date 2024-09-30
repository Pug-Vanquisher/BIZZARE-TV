using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public Vector2 velik;

    private RaycastHit2D hit;

    public float attackRange;
    public float vlframes;
    private float current_vlframes;

    

    // Start is called before the first frame update
    void Start()
    {
        current_vlframes = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, player.transform.position) <= attackRange & current_vlframes == 0)
        {
            Attack();
        }
        else if (current_vlframes == 0)
        {
            //transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, speed * Time.deltaTime);
            //velik = player.transform.position - transform.position;

            Vector2 Distance = player.transform.position - transform.position;

            if(Mathf.Abs(Distance.x) > Mathf.Abs(Distance.y))
            {
                if (Distance.x < 0) { velik.x = -1; }
                else { velik.x = 1; }
            }
            else
            {
                if (Distance.y < 0) { velik.y = -1; }
                else { velik.y = 1; }
            }

            /*
            if (player.transform.position.x >= transform.position.x) { velik.x = 1; }
            if (player.transform.position.x < transform.position.x) { velik.x = -1; }
            if (player.transform.position.y >= transform.position.y) { velik.y = 1; }
            if (player.transform.position.y < transform.position.y) { velik.y = -1; }
            */

            if (Mathf.Abs(player.transform.position.x - transform.position.x) <= 0.2 ) { velik.x = 0; }
            if (Mathf.Abs(player.transform.position.y - transform.position.y) <= 0.2) { velik.y = 0; }


            hit = Physics2D.BoxCast(transform.position, Vector2.one, 0, velik, Vector2.Distance(transform.position, new Vector2(transform.position.x, transform.position.y) + velik.normalized * speed), LayerMask.GetMask("Actor", "Blocking"));

            if (hit.collider == null) { gameObject.GetComponent<Rigidbody2D>().MovePosition(new Vector2(transform.position.x, transform.position.y) + velik.normalized * speed); }

        }
        else if (current_vlframes < 0) { current_vlframes = 0; }

        else { current_vlframes -= Time.deltaTime; }
    }

    void Attack()
    {
        Debug.Log("палучай!!!");
        
        current_vlframes = vlframes;
    }
}
