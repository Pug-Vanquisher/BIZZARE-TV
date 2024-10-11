using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jupiter731
{
    public class TrackMouseWeapon : MonoBehaviour
    {
        [SerializeField] GameObject player;
        [SerializeField] Camera MainCamera;
        private Vector2 _mousePosition;
        private Vector2 _cameraPlayerOffset;
        private Vector3 _cameraPlayerOffset3D;
        private Vector2 _rotDirect;
        private bool _isReflect = false;

        private void Awake()
        {
            _cameraPlayerOffset3D = player.transform.position - MainCamera.transform.position;
            _cameraPlayerOffset.x = _cameraPlayerOffset3D.x;
            _cameraPlayerOffset.y = _cameraPlayerOffset3D.y;
        }

        private void FixedUpdate()
        {
            Track();
        }

        private void Track()
        {
            _mousePosition = Input.mousePosition;
            _mousePosition.x /= Screen.width;
            _mousePosition.y /= Screen.height;
            _mousePosition -= Vector2.one * 0.5f;
            _rotDirect = new Vector2(MainCamera.transform.position.x - player.transform.position.x, MainCamera.transform.position.y - player.transform.position.y);
            var rotation = new Quaternion();
            var angle = Vector2.SignedAngle(Vector2.right, _rotDirect);
            if ((angle > 90 || angle < -90) && !_isReflect)
            {
                Reflect();
                _isReflect = true;
            }
            else if(_isReflect && (angle < 90 && angle > 0 || angle > -90 && angle < 0))
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
        }

        private void Reflect()
        {
            var scale = transform.localScale;
            scale.y *= -1;
            transform.localScale = scale;
        }
    }
}
