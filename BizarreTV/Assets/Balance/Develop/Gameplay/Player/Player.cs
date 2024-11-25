using UnityEngine;

namespace Balance
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        private PlayerMovement _movement;
        private Gravity _gravity;

        private Rigidbody _rigidbody;
        private IInput _input;

        private AudioSourcer _audioSourcer;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            _movement = new PlayerMovement(_rigidbody);
            _gravity = new Gravity(_rigidbody);

            _input = DIContainer.Resolve<IInput>();
            AudioPlayer audioPlayer = DIContainer.Resolve<AudioPlayer>();
            _audioSourcer = audioPlayer.PlayLoop(audioPlayer.Config.PlayerMovementSound);
        }

        private void FixedUpdate()
        {
            _movement.Move(Time.fixedDeltaTime);
            _gravity.Use();

            PlaySound();
        }

        private void PlaySound()
        {
            if (_input.IsNotZero())
                _audioSourcer.Enable();
            else
                _audioSourcer.Disable();
        }
    }
}
