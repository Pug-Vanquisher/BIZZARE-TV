using UnityEngine;

namespace Balance
{
    public class TrackingCamera : MonoBehaviour
    {
        [SerializeField] private Transform _target; // <- Потом заменить на игрока из DI.

        private float _trackSpeed;

        private Vector3 _initialPosition;
        private Quaternion _initialRotation;

        private void Awake()
        {
            _trackSpeed = DIContainer.Resolve<CameraConfig>().TrackSpeed;

            _initialPosition = transform.position;
            _initialRotation = transform.rotation;
        }

        private void FixedUpdate()
        {
            Move(Time.fixedDeltaTime);
            LookAt();
        }

        private void Move(float delta)
        {
            Vector3 position = _target.position + _initialPosition;
            transform.position = Vector3.Lerp(transform.position, position, _trackSpeed * delta);
        }

        private void LookAt()
        {
            transform.LookAt(_target.position);
        }
    }
}
