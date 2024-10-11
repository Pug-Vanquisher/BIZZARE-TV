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
        private float _centreOffset;
        private bool _isReflect = false;


        private void Awake()
        {
            _centreOffset = Screen.width / 2;
        }

        void Update()
        {
            _centreOffset = _centreOffset + (MainCamera.transform.position.normalized.x - player.transform.position.normalized.x) * ;
            _mousePosition = Input.mousePosition;
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
            player.transform.localScale = -player.transform.localScale;
        }
    }
}

