using UnityEngine;

namespace Rogalik.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        private RoguelikePlayer _player;
    
        private Vector2 _direction;

        private void Awake()
        {
            _player = GetComponent<RoguelikePlayer>();
        }

        private void Update()
        {
            Move();
            FlipSpriteBasedOnDirection();
        }

        private void Move()
        {
            _direction.x = Input.GetAxis("Horizontal");
            _direction.y = Input.GetAxis("Vertical");
        
            if (_direction.magnitude > 1)
                _direction.Normalize();
        
            if (_player.isRolling)
                transform.Translate(_direction * (_player.currentStats.rollSpeed * Time.deltaTime));
            else
                transform.Translate(_direction * (_player.currentStats.speed * Time.deltaTime));
        
            _player.isStaying = _direction.magnitude < 0.1f;
        }
        private void FlipSpriteBasedOnDirection()
        {
            if (_direction.x > 0)
                transform.localScale = new Vector3(-3, 3, 1);
            else if (_direction.x < 0)
                transform.localScale = new Vector3(3, 3, 1);
        }
    }
}