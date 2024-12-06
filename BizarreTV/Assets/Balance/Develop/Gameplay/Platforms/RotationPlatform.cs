using System.Collections;
using UnityEngine;

namespace Balance
{
    public class RotationPlatform : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Vector3 _direction;

        [Space]

        [SerializeField] private float _pauseDuration;

        private Vector3 _initialAngle;
        private Vector3 _targetAngle;
        private bool _canRotate;

        private void Awake()
        {
            _canRotate = true;

            _initialAngle = transform.eulerAngles;
            _targetAngle = _initialAngle + _direction * 180f;
        }

        private void Update()
        {
            Rotate(Time.deltaTime);

            if (transform.rotation == Quaternion.Euler(_targetAngle))
            {
                transform.rotation = Quaternion.Euler(_initialAngle);
                StartCoroutine(Pause());
            }
        }

        private void Rotate(float delta)
        {
            if (!_canRotate) return;

            Quaternion rotation = Quaternion.Euler(_targetAngle);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _speed * delta);
        }

        private IEnumerator Pause()
        {
            _canRotate = false;

            yield return new WaitForSeconds(_pauseDuration);

            _canRotate = true;
        }
    }
}
