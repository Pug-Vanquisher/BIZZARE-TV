using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BID
{
    public class ChainScript : MonoBehaviour
    {
        public LineRenderer chain;
        public float slRatio;
        public int corners;
        public Vector3 holdPoint;
        public bool destroyed;
        public GameObject broke;
        void Start()
        {
            chain = GetComponent<LineRenderer>();
            if (!destroyed)
            {
                if (transform.position.y < holdPoint.y)
                {
                    chain.sortingOrder = (int)(transform.position.y * -10);
                }
                else
                {
                    chain.sortingOrder = (int)(holdPoint.y * -10);
                }
                chain.positionCount = corners + 2;
            }
            else
            {
                Destroy(chain);
                chain = null;
            }


            GetComponent<SpriteRenderer>().sortingOrder = (int)(transform.position.y * -10);
        }

        // Update is called once per frame
        void Update()
        {
            if(chain != null)
            {
                float length = (transform.position - holdPoint).magnitude;
                for (int i = 0; i < chain.positionCount; i++)
                {
                    if (i == 0)
                    {
                        chain.SetPosition(i, transform.position + Vector3.up * 0.7f);
                    }
                    else
                    {
                        chain.SetPosition(i, holdPoint);
                        float iMult = chain.positionCount - i;
                        float sinMult = Mathf.Sin(Mathf.PI / iMult);
                        chain.SetPosition(i, transform.position + (holdPoint - transform.position).normalized * length / iMult + Vector3.down * slRatio * sinMult);
                    }
                }
                if (destroyed)
                {
                    for (int i = 0; i < chain.positionCount; i++)
                    {
                        Instantiate(broke, chain.GetPosition(i), Quaternion.identity);
                    }
                    chain = null;
                    Destroy(GetComponent<LineRenderer>());
                }
            }
        }
    }

}