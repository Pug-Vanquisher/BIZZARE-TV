using UnityEngine;
using System.Collections;

namespace Rogalik
{

    public class EnemyRanged : MonoBehaviour
    {
        public GameObject projectilePrefab;
        public Transform player;
        public float shootInterval = 2f;
        public float moveSpeed = 5f;
        public int projectilesCount = 5;
        public float spreadAngle = 15f;
        public GameObject square; // Объект фона/пола, в пределах которого должен перемещаться враг
        public Animator MageAnims;
        private Vector3 targetPosition;
        private Vector2 lastPosition;
        private bool isMoving = false;
        private SpriteRenderer squareRenderer;

        private void Start()
        {
            squareRenderer = square.GetComponent<SpriteRenderer>();
            StartCoroutine(BehaviorLoop());
        }

        private IEnumerator BehaviorLoop()
        {
            while (true)
            {
                if (!isMoving)
                {
                    MoveToRandomPointWithinBounds();
                }
                yield return new WaitUntil(() => isMoving == false);
                Shoot();
                yield return new WaitForSeconds(shootInterval);
            }
        }

        void MoveToRandomPointWithinBounds()
        {
            if (squareRenderer != null)
            {
                Bounds bounds = squareRenderer.bounds;
                float randomX = Random.Range(bounds.min.x, bounds.max.x);
                float randomY = Random.Range(bounds.min.y, bounds.max.y);

                MageAnims.SetTrigger("Attack");
                targetPosition = new Vector3(randomX, randomY, transform.position.z);
                isMoving = true;
            }
        }

        void Update()
        {
            if (isMoving)
            {
                MoveTowardsTarget();
            }

            // Поворот спрайта в зависимости от направления движения
            if (transform.position.x > lastPosition.x)
                transform.localScale = new Vector3(4, 4, 1);
            else if (transform.position.x < lastPosition.x)
                transform.localScale = new Vector3(-4, 4, 1);

            lastPosition = transform.position;
        }

        void MoveTowardsTarget()
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);

            if (Vector3.Distance(transform.position, targetPosition) < 0.001f)
            {
                isMoving = false;
            }
        }

        void Shoot()
        {
            SoundManager.Instance.PlaySound(1);
            Vector2 direction = (player.position - transform.position).normalized;
            for (int i = 0; i < projectilesCount; i++)
            {
                float angle = Mathf.Lerp(-spreadAngle, spreadAngle, i / (projectilesCount - 1f));
                Vector2 spreadDirection = Quaternion.Euler(0, 0, angle) * direction;
                GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
                projectile.GetComponent<Projectile>().SetDirection(new Vector3(spreadDirection.x, spreadDirection.y, 0));
            }
        }
    }

}