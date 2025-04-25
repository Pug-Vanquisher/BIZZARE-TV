using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LT
{
    public class Arrow : MonoBehaviour
    {
        public float speed;
        public Rigidbody rb;
        void Start()
        {
            Invoke("Death", 5f);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            rb.MovePosition(transform.position + transform.forward * speed);
        }
        void Death()
        {
            Destroy(gameObject);
        }
    }

}