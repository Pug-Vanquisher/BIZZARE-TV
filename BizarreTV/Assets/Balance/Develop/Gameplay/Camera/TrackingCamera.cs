using UnityEngine;

namespace Balance
{
    public class TrackingCamera : MonoBehaviour
    {
        [SerializeField] private Transform _target; // <- Потом заменить на игрока из DI.

        private float _trackSpeed;
        private Vector3 _offset;

        private Vector3 DesiredPosition => _target.position + _offset;

        private void Awake()
        {
            _trackSpeed = DIContainer.Resolve<CameraConfig>().TrackSpeed;
            _offset = DIContainer.Resolve<CameraConfig>().OffsetFromPlayer;

            transform.position = DesiredPosition;
            transform.LookAt(_target.position);
        }

        private void FixedUpdate()
        {
            Move(Time.fixedDeltaTime);
        }

        private void Move(float delta)
        {
            transform.position = Vector3.Lerp(transform.position, DesiredPosition, _trackSpeed * delta);
        }
    }
}
