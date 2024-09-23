using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BID
{
    public class PlayerMover : MonoBehaviour
    {
        public CharacterController player;
        public Vector2 velik;

        public KeyCode Up;
        public KeyCode Down;
        public KeyCode Right;
        public KeyCode Left;
        public KeyCode Attack;
        public KeyCode Dash;

        public float speed = 0.025f;

        public GameObject dagger;


        

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            velik = Vector2.zero;
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            Vector3 side1 = mousePosition - player.transform.position;
            Vector3 side2 = player.transform.position + Vector3.right - player.transform.position;

            Vector3 side1cos = Quaternion.AngleAxis(-90, Vector3.forward) * side1;

            Debug.DrawRay(player.transform.position,side1cos);

            if (Input.GetKey(Up)) { velik.y += 1; }
            if (Input.GetKey(Down)) { velik.y += -1; }
            if (Input.GetKey(Right)) { velik.x += 1; }
            if (Input.GetKey(Left)) { velik.x += -1; }

            Debug.DrawLine(player.transform.position, mousePosition);
            //print(Vector2.SignedAngle(player.transform.position, mousePosition));
            //print(player.transform.position);

            if (Input.GetKeyDown(Attack))
            {
                var a = Instantiate(dagger, player.transform.position + side1cos * Random.Range(-1f, 2f) * 0.03f, Quaternion.identity);
                a.transform.rotation = Quaternion.AngleAxis(-Vector2.SignedAngle(mousePosition - a.transform.position, Vector3.right), Vector3.forward);

                //Quaternion.AngleAxis(-Vector2.SignedAngle(side1, side2) + Random.Range(-5, 5), Vector3.forward)
                //
            }


            player.Move(velik.normalized * speed);
        }
    }
}
