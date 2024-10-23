using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BID
{
    public class PlayerMover : MonoBehaviour
    {
        public CharacterController pl_tmp;
        public Vector2 velik;
        public KeyConfig key = new KeyConfig();

        public float speed = 0.025f;

        private RaycastHit2D hit;

        public GameObject dagger;
        public float AttackSpreadness = 0.03f;

        public Collider2D HitCollider;

        void Update()
        {
            var sprRender = GetComponent<SpriteRenderer>().sortingOrder = (int)(transform.position.y * -10);
            velik = Vector2.zero;

            if (Input.GetKey(key.Up)) { velik.y += 1; }
            if (Input.GetKey(key.Down)) { velik.y += -1; }
            if (Input.GetKey(key.Right)) { velik.x += 1; }
            if (Input.GetKey(key.Left)) { velik.x += -1; }

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            Vector3 side1cos = Quaternion.AngleAxis(-90, Vector3.forward) * (mousePosition - transform.position);

            if (Input.GetKeyDown(key.Attack) || Input.GetKeyDown(key.AltAttack))
            {
                var a = Instantiate(dagger, transform);
                a.GetComponent<Dagger>().creator = HitCollider;
                a.transform.position = transform.position + side1cos * Random.Range(-1f, 1f) * AttackSpreadness + Vector3.up;
                a.transform.rotation = Quaternion.AngleAxis(-Vector2.SignedAngle(mousePosition - a.transform.position, Vector3.right), Vector3.forward);
            }
        }

        private void FixedUpdate()
        {
            pl_tmp.Move(velik.normalized * speed);

            hit = Physics2D.BoxCast(transform.position, Vector2.one, 0, velik, Vector2.Distance(transform.position, new Vector2(transform.position.x, transform.position.y) + velik.normalized * speed), LayerMask.GetMask("Actor", "Blocking"));
            
            if (hit.collider == null) { } 
        }
    }
}
