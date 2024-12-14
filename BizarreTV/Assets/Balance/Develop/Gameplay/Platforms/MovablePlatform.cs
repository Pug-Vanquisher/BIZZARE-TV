using UnityEngine;

namespace Balance
{
    public class MovablePlatform : MonoBehaviour
    {
        [SerializeField] private Transform _platform;
        [SerializeField] private Transform[] _points;
        private int _pointIndex;

        [Space]

        [SerializeField] private float _speed;

        private void Update()
        {
            Move(Time.deltaTime);

            if (_platform.position == _points[_pointIndex].position)
                IncreasePointIndex();
        }

        private void Move(float delta)
        {
            Vector3 position = _points[_pointIndex].position;
            _platform.position = Vector3.MoveTowards(_platform.position, position, _speed * delta);
        }

        private void IncreasePointIndex()
        {
            _pointIndex += 1;

            if (_pointIndex >= _points.Length)
                _pointIndex = 0;
        }
    }
}
