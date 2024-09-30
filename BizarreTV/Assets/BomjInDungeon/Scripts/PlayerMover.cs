using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BID
{
    public class PlayerMover : MonoBehaviour
    {
        public Rigidbody2D pl_tmp;
        public Vector2 velik;
        public KeyConfig key = new KeyConfig();

        public float speed = 0.025f;

        private RaycastHit2D hit;

        public GameObject dagger;
        public float AttackSpreadness = 0.03f;

        void Update()
        {
            velik = Vector2.zero;

            if (Input.GetKey(key.Up)) { velik.y += 1; }
            if (Input.GetKey(key.Down)) { velik.y += -1; }
            if (Input.GetKey(key.Right)) { velik.x += 1; }
            if (Input.GetKey(key.Left)) { velik.x += -1; }

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            Vector3 side1 = mousePosition - transform.position;

            Vector3 side1cos = Quaternion.AngleAxis(-90, Vector3.forward) * side1;

            if (Input.GetKeyDown(key.Attack))
            {
                var a = Instantiate(dagger, transform);
                a.transform.position = transform.position + side1cos * Random.Range(-1f, 1f) * AttackSpreadness;
                a.transform.rotation = Quaternion.AngleAxis(-Vector2.SignedAngle(mousePosition - a.transform.position, Vector3.right), Vector3.forward);
            }
        }

        private void FixedUpdate()
        {
            hit = Physics2D.BoxCast(transform.position, Vector2.one, 0, velik, Vector2.Distance(transform.position, new Vector2(transform.position.x, transform.position.y) + velik.normalized * speed), LayerMask.GetMask("Actor", "Blocking"));

            if (hit.collider == null) { pl_tmp.MovePosition(new Vector2(transform.position.x, transform.position.y) + velik.normalized * speed); } 
        }
    }
}
