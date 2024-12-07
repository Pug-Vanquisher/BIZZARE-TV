using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jupiter731
{
    public class TrackMouseWeapon : MonoBehaviour
    {
        [SerializeField] GameObject rotationsTarget;
        [SerializeField] Camera mainCamera;
        private Vector2 _mousePosition;
        private Vector2 _rotDirect;
        private Vector2 _playerToCamera;
        private bool _isReflect = false;
        private RaycastHit2D _hit;


        private void Awake()
        {
            
        }

        private void FixedUpdate()
        {
            Track();
        }

        private void Track()
        {
            _mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            _playerToCamera = new Vector2(mainCamera.transform.position.x - rotationsTarget.transform.position.x,
                mainCamera.transform.position.y - rotationsTarget.transform.position.y);
            _rotDirect = _mousePosition - (Vector2)rotationsTarget.transform.position;
            _rotDirect = _rotDirect.normalized;

            //_hit = Physics2D.Raycast(_mousePosition, mainCamera.transform.forward);

            //if (_hit.collider != null)
            //{
                //_rotDirect = _hit.point - (Vector2)rotationsTarget.transform.position;
                //Debug.Log(_hit.collider.transform.position);
                var rotation = new Quaternion();
                var angle = Vector2.SignedAngle(Vector2.right, _rotDirect);
                if ((angle > 90 || angle < -90) && !_isReflect)
                {
                    Reflect();
                    _isReflect = true;
                }
                else if (_isReflect && (angle < 90 && angle > 0 || angle > -90 && angle < 0))
                {
                    Reflect();
                    _isReflect = false;
                }
                rotation.eulerAngles = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, angle);
                //Debug.Log(angle.ToString());
                if ((angle < 91 && angle > 89 || angle < -91 && angle > -89))
                {
                    //Debug.Log("YEBAAAAAL");
                }
                else
                    transform.rotation = rotation;
           // }
        }

        private void Reflect()
        {
            var scale = transform.localScale;
            scale.y *= -1;
            transform.localScale = scale;
        }
    }
}
