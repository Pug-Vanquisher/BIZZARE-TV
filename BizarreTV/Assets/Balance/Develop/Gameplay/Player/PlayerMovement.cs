using UnityEngine;

namespace Balance
{
    public class PlayerMovement
    {
        private Rigidbody _rigidbody;

        private float _moveSpeed;
        private IInput _input;

        private AudioPlayer _audioPlayer;

        public PlayerMovement(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;

            _moveSpeed = DIContainer.Resolve<PlayerConfig>().MoveSpeed;
            _input = DIContainer.Resolve<IInput>();
            _audioPlayer = DIContainer.Resolve<AudioPlayer>();
        }

        public void Move(float delta)
        {
            if (!_input.IsNotZero()) return;

            Vector3 force = _input.GetAxis() * _moveSpeed * delta;
            _rigidbody.AddForce(force, ForceMode.VelocityChange);
        }
    }
}
