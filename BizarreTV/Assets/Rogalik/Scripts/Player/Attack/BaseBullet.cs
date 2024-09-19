using UnityEngine;

namespace Rogalik.Scripts.Player.Attack
{
    public class BaseBullet : MonoBehaviour
    {
        public float damageMultiplier = 1f;
        public float speedMultiplier = 1f;
        public float rangeMultiplier = 1f;
        public float bulletSpeedMultiplier = 1f;

        protected PlayerStats Stats => FindObjectOfType<RoguelikePlayer>().currentStats;
        
        private Vector3 _startPosition;
        private Vector3 _direction;
        
        public virtual void Init(Vector3 direction)
        {
            _direction = direction;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            _startPosition = transform.position;
        }

        private void Update()
        {
            Move(_direction, Stats.bulletSpeed * bulletSpeedMultiplier);
        }

        public virtual void Move(Vector3 direction, float speed)
        {
            // print(speed);
            transform.Translate(Vector3.up * (speed * Time.deltaTime));
            
            if (Vector3.Distance(_startPosition, transform.position) > Stats.attackRange * rangeMultiplier)
            {
                Destroy(gameObject);
            }
        }
        
        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("RoguelikeEnemy"))
            {
                EnemyHP enemy = other.gameObject.GetComponent<EnemyHP>();
                HitEnemy(enemy);
            }
        }

        protected virtual void HitEnemy(EnemyHP enemy)
        {
            if (enemy == null) return;
            
            enemy.TakeDamage((int) (Stats.damage * damageMultiplier));
            Destroy(gameObject);
        }
    }
}