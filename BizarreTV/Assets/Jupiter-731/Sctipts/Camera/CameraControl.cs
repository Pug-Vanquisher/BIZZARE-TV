using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jupiter731
{
    public class CameraControl : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] float speed = 10f;
        [SerializeField] private float tickRate = 100f;
        [SerializeField] private float xBorderPos = 2f;
        [SerializeField] private float zBorderPos = 2f;

        private Vector3 mousePos;
        private Vector3 smooth;
        private Vector3 offset;
        private WaitForSeconds TickRateTime;



        private void Awake()
        {
            offset = target.position - transform.position;
            TickRateTime = new WaitForSeconds(1 / tickRate);
            StartCoroutine(Control());
        }



        IEnumerator Control()
        {
            while (true)
            {

                smooth = Vector3.Lerp(transform.position, target.position - offset, speed);
                smooth = MouseTrack(smooth);
                transform.position = smooth;
                yield return TickRateTime;
            }
        }

        private Vector3 MouseTrack(Vector3 Changed)
        {
            mousePos = Input.mousePosition;
            mousePos.x *= xBorderPos / Screen.width;
            mousePos.y *= zBorderPos / Screen.height;
            mousePos -= 0.5f * (Vector3.right * xBorderPos + Vector3.up * zBorderPos);
            Changed.x += mousePos.x;
            Changed.y += mousePos.y;

            return Changed;
        }
    }
}
