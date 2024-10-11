using System.Collections;
using UnityEngine;

namespace Jupiter731
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] Rigidbody2D playerBody;
        [SerializeField] float speed;
        [SerializeField] KeyCode dashKey = KeyCode.LeftShift;
        [SerializeField] float dashMulti;
        [SerializeField] float dashTime;
        private float _currDashTime;
        protected Vector3 direct;
        private WaitForEndOfFrame _waiter = new WaitForEndOfFrame();
        void Update()
        {
            direct.x = Input.GetAxis("Horizontal");
            direct.y = Input.GetAxis("Vertical");
            if (Input.GetKeyDown(dashKey))
            {
                _currDashTime = 0;
                StartCoroutine(Dash(direct.normalized));
            }
            else
            {
                Move(direct.normalized);
            }
        }

        private void Move(Vector3 direct)
        {
            playerBody.velocity = direct * speed;
        }

        private IEnumerator Dash(Vector3 direct)
        {
            while (_currDashTime < dashTime) 
            {
                _currDashTime += Time.deltaTime;
                playerBody.velocity = direct * speed * dashMulti;
                yield return _waiter;
            }
            yield return null;
        }
    }
}
