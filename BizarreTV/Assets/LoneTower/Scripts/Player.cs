using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LT
{
    public class Player : MonoBehaviour
    {
        public float speed;

        public GameObject arrow;
        public Transform arrowPos;
        public Animator animator;

        public bool isAiming = false;
        public bool isShooting = false;
        public bool isReloading = false;

        private Vector2 clampRadius;
        public Pointer pointer;
        public Vector3 target;
        void Start()
        {
            clampRadius = new Vector2(PointManager.Instance.GetPoint("LeftSide").x, PointManager.Instance.GetPoint("RightSide").x);
            pointer.Point(arrowPos, transform.position.x);
        }

        void FixedUpdate()
        {
            speed = GameObject.Find("Game").GetComponent<Game>().upgrades["archers_speed"].value;
            animator.SetFloat("Speed", Mathf.Abs(Input.GetAxis("Horizontal")));
            Logic();
        }

        public void Logic()
        {
            if (isShooting)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                if (!Input.GetKey(KeyCode.Space) && !isReloading)
                {
                    animator.SetTrigger("Cancel");
                }

                if (!isReloading && !isAiming)
                {
                    Move(0.5f);
                    target = pointer.Point(arrowPos, transform.position.x);
                }
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    animator.Play("Aim");
                }

                if (!isReloading && !isAiming)
                {
                    Move();
                }
                transform.rotation = Quaternion.Euler(0, -90f * Input.GetAxis("Horizontal"), 0);
            }
        }

        public void Move(float speedScale = 1)
        {
            transform.position += Vector3.right * -Input.GetAxis("Horizontal") * (speed + 0.02f) * speedScale;
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, clampRadius[1], clampRadius[0]),
                transform.position.y, transform.position.z);
        }

        void MakeShoot()
        {
            var a = Instantiate(arrow, arrowPos.position, Quaternion.identity);
            a.transform.LookAt(target);
        }

        //Вызываются из PlayerAnimationHandler
        public void Aim(bool isStarted)
        {
            isShooting = true;
            isAiming = isStarted;
        }
        public void Shoot()
        {
            MakeShoot();
            isReloading = true;
        }
        public void Ready()
        {
            isShooting = false;
            isReloading = false;
        }


    }
}