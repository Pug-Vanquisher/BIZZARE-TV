using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LT
{
    public class Pointer : MonoBehaviour
    {
        public float speed;

        private bool visible;

        public LineRenderer line;
        public SpriteRenderer point;

        private Vector3 pointerInit;

        private float maxRange;

        private void Start()
        {
            maxRange = PointManager.Instance.GetPoint("ShootRange").z;
            pointerInit = new Vector3(0, PointManager.Instance.GetPoint("EnemySpawn").y, PointManager.Instance.GetPoint("Attack").z);

        }
        private void Update()
        {
            line.enabled = visible;
            point.enabled = visible;
        }

        public Vector3 Point(Transform lineTail, float current_x)
        {
            CancelInvoke("disable");
            visible = true;
            line.enabled = visible;
            point.enabled = visible;

            if(transform.position.z + speed <= maxRange)
            {
                transform.position += Vector3.forward * speed;
            }
            transform.position = new Vector3(current_x, transform.position.y, transform.position.z);

            line.SetPosition(0, lineTail.position);
            RaycastHit hit;
            Physics.Raycast(lineTail.position, (transform.position - lineTail.position).normalized, out hit);
            Invoke("disable", 0.1f);
            line.SetPosition(1, hit.point);
            return hit.point;
            /*if ((transform.position - lineTail.position).magnitude < (hit.point - lineTail.position).magnitude)
            {
                line.SetPosition(1, transform.position);
                Debug.Log("123");
                return transform.position;
            }
            else
            {
            }*/

        }
        private void disable()
        {
            visible = false;
            transform.position = pointerInit;
        }
    }
}
