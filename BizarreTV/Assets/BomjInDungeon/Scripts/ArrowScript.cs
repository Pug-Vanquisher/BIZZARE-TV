using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

namespace BID
{
    public class ArrowScript : MonoBehaviour
    {
        public float lifetime;
        private float startime;
        public int speed;


        void Start()
        {
            startime = Time.time;
        }

        void Update()
        {

            if (Time.time >= startime + lifetime) { Destroy(gameObject); }
            transform.position += transform.right * Time.deltaTime * speed;
            //else { gameObject.GetComponent<Rigidbody2D>().MovePosition(new Vector2(transform.right.x, transform.right.y) - new Vector2(transform.position.x, transform.position.y)  * speed); }
        }
    }
}
