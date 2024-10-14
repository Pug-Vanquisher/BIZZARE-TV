using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Jupiter731
{
    public class CameraControl : MonoBehaviour
    {
        [SerializeField] Transform target;
        [SerializeField] float speed = 10f;
        [SerializeField] private float tickRate = 60f;
        [SerializeField] private float xBorderPos = 2f;
        [SerializeField] private float zBorderPos = 2f;

        private Vector3 mousePos;
        private Vector3 smooth;
        private Vector3 offset;
        private WaitForSeconds TickRateTime;

        //private void Start()
        //{
        //    Application.targetFrameRate = 60;
        //}

        private void Awake()
        {
            var startCoord = transform.position;
            startCoord.x = target.position.x;
            startCoord.y = target.position.y;
            transform.position = startCoord;
            offset = target.position - transform.position;
        }


        private void Update()
        {       
            smooth = Vector3.Lerp(transform.position, target.position - offset, speed);
            smooth = MouseTrack(smooth);
            transform.position = smooth;

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
