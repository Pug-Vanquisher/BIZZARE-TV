using System.Collections;
using UnityEngine;

namespace Rogalik.Scripts.Player.Attack
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        
        public BaseBullet currentBullet;
        
        private RoguelikePlayer _player;
        private bool _isCooldown;

        private void Awake()
        {
            _player = GetComponent<RoguelikePlayer>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !_isCooldown)
            {
                Attack();
            }
        }

        private void Attack()
        {
            SoundManager.Instance.PlaySound(0);
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = (mousePosition - transform.position).normalized;
            
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            currentBullet = bullet.GetComponent<BaseBullet>();
            currentBullet.Init(direction);
            
            StartCoroutine(AttackCooldown(_player.currentStats.attackSpeed * currentBullet.speedMultiplier));
        }
        
        private IEnumerator AttackCooldown(float cooldown)
        {
            _isCooldown = true;
            yield return new WaitForSeconds(cooldown);
            _isCooldown = false;
        }
    }
}