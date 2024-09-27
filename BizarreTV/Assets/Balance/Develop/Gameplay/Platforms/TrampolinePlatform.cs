using UnityEngine;

namespace Balance
{
    public class TrampolinePlatform : MonoBehaviour
    {
        [SerializeField] private float _pushForce;

        private void OnTriggerEnter(Collider collider)
        {
            if (collider.CompareTag("Player") && collider.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                Push(rigidbody);
        }

        private void Push(Rigidbody rigidbody)
        {
            float fallVelocity = -rigidbody.velocity.y;
            //rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0f, rigidbody.velocity.z);
            rigidbody.AddForce(Vector2.up * _pushForce * fallVelocity, ForceMode.Impulse);
        }
    }
}
