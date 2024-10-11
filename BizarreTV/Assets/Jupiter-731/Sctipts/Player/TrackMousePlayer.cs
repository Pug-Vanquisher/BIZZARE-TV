using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jupiter731
{
    public class TrackMousePlayer : MonoBehaviour
    {
        [SerializeField] GameObject player;
        [SerializeField] Camera MainCamera;
        private Vector2 _mousePosition;
        private float _startCentreOffset;
        private float _centreOffset;
        private bool _isReflect = false;


        private void Awake()
        {
            _startCentreOffset = Screen.width / 2;
        }

        void Update()
        {
            _centreOffset = _startCentreOffset ;
            _mousePosition = Input.mousePosition;
            //Debug.Log(_centreOffset.ToString() + _mousePosition);
            if (_mousePosition.x - _centreOffset < 0 && !_isReflect)
            {
                _isReflect = true;
                Reflect();
            }
            else if (_mousePosition.x - _centreOffset >= 0 && _isReflect)
            {
                _isReflect = false;
                Reflect();
            }

        }

        private void Reflect()
        {
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }
}

