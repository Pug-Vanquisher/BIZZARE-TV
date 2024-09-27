using UnityEngine;

namespace Balance
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        private PlayerMovement _movement;
        private Gravity _gravity;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();

            _movement = new PlayerMovement(_rigidbody);
            _gravity = new Gravity(_rigidbody);
        }

        private void FixedUpdate()
        {
            _movement.Move(Time.fixedDeltaTime);
            _gravity.Use();
        }
    }
}
