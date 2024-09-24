using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BID
{
    public class PlayerMover : MonoBehaviour
    {
        //public CharacterController player;
        public Rigidbody2D pl_tmp;
        public Vector2 velik;
        public KeyConfig key = new KeyConfig();

        public float speed = 0.025f;

        public GameObject dagger;
        public float AttackSpreadness = 0.03f;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            velik = Vector2.zero;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            Vector3 side1 = mousePosition - transform.position;
            Vector3 side2 = transform.position + Vector3.right - transform.position;

            Vector3 side1cos = Quaternion.AngleAxis(-90, Vector3.forward) * side1;

            Debug.DrawRay(transform.position,side1cos);

            if (Input.GetKey(key.Up)) { velik.y += 1; }
            if (Input.GetKey(key.Down)) { velik.y += -1; }
            if (Input.GetKey(key.Right)) { velik.x += 1; }
            if (Input.GetKey(key.Left)) { velik.x += -1; }

            Debug.DrawLine(transform.position, mousePosition);
            //print(Vector2.SignedAngle(player.transform.position, mousePosition));
            //print(player.transform.position);

            if (Input.GetKeyDown(key.Attack))
            {
                var a = Instantiate(dagger, transform); //transform.position + side1cos * Random.Range(-1f, 2f) * 0.03f, Quaternion.identity);
                a.transform.position = transform.position + side1cos * Random.Range(-1f, 1f) * AttackSpreadness;
                a.transform.rotation = Quaternion.AngleAxis(-Vector2.SignedAngle(mousePosition - a.transform.position, Vector3.right), Vector3.forward);
                
                //Quaternion.AngleAxis(-Vector2.SignedAngle(side1, side2) + Random.Range(-5, 5), Vector3.forward)
            }


            //player.Move(velik.normalized * speed);
            pl_tmp.MovePosition(new Vector2(transform.position.x, transform.position.y) + velik.normalized * speed);
        }
    }
}
