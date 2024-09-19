using System.Collections;
using UnityEngine;

namespace Rogalik.Scripts.Player
{
    public class PlayerRoll : MonoBehaviour
    {
        [SerializeField] private KeyCode rollKey = KeyCode.Space;
        [SerializeField] private float rollDuration = 0.5f;
        public Animator PlayerAnims;
    
        private RoguelikePlayer _player;
        private BoxCollider2D _collider;
        private Rigidbody2D _rigidbody;
    
        private void Awake()
        {
            _player = GetComponent<RoguelikePlayer>();
            _collider = GetComponent<BoxCollider2D>();
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(rollKey))
            {
                Roll(rollDuration);
            }
        }
    
        private void Roll(float duration)
        {
            if (_player.isRolling || _player.isStaying)
                return;
        
            StartCoroutine(RollCoroutine(duration));
        }
    
        private IEnumerator RollCoroutine(float duration)
        {
            SoundManager.Instance.PlaySound(3);
            _player.isRolling = true;
            _player.currentStats.isRolling = true;
            PlayerAnims.SetTrigger("Dash");

            yield return new WaitForSeconds(duration);
        
            _player.isRolling = false;
            _player.currentStats.isRolling = false;
            _rigidbody.velocity = Vector2.zero;
        }
    }
}
