using UnityEngine;

namespace Balance
{
    public class Gravity
    {
        private Rigidbody _rigidbody;

        public Gravity(Rigidbody rigidbody)
        {
            _rigidbody = rigidbody;
            _rigidbody.useGravity = false;
        }

        public void Use()
        {
            float m = _rigidbody.mass;
            float g = Physics.gravity.y;
            Vector3 gravity = m * g * Vector3.up;

            _rigidbody.AddForce(gravity, ForceMode.Acceleration);
        }
    }
}
