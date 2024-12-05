using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Rogalik
{

    public class SlashingEnemy : MonoBehaviour
    {
        public Transform player;
        public float moveSpeed = 3.5f;
        public float dashSpeed = 10f;
        public float stopDistance = 3f;
        public float dashDistance = 2f;
        public Animator DemonAnims;
        private NavMeshAgent agent;
        private bool isDashing = false;
        private bool canMove = true;
        private Vector2 lastPosition;
        private Collider2D bodyCollider; // Обычный коллайдер 
        private Collider2D triggerCollider; // Коллайдер-триггер


        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            agent.updateRotation = false;
            agent.updateUpAxis = false;
            Collider2D[] colliders = GetComponents<Collider2D>();
            foreach (var collider in colliders)
            {
                if (collider.isTrigger)
                    triggerCollider = collider;
                else
                    bodyCollider = collider;
            }
            agent.speed = moveSpeed;
        }

        void Update()
        {
            if (canMove && !isDashing)
            {
                agent.SetDestination(player.position);
            }

            if (transform.position.x > lastPosition.x)
                transform.localScale = new Vector3(-2, 2, 1);
            else if (transform.position.x < lastPosition.x)
                transform.localScale = new Vector3(2, 2, 1);

            lastPosition = transform.position;
        }

        void LateUpdate()
        {
            if (Vector2.Distance(transform.position, player.position) <= stopDistance && !isDashing)
            {
                StartCoroutine(DashThroughPlayer());
            }
        }

        IEnumerator DashThroughPlayer()
        {
            DemonAnims.SetTrigger("Attack");
            canMove = false;
            isDashing = true;
            agent.isStopped = true;
            bodyCollider.enabled = false;

            yield return new WaitForSeconds(0.5f);  // Пауза перед рывком

            Vector2 startPosition = transform.position;
            SoundManager.Instance.PlaySound(3);
            Vector2 targetPosition = new Vector2(player.position.x, player.position.y) + ((new Vector2(player.position.x, player.position.y) - new Vector2(transform.position.x, transform.position.y)).normalized * dashDistance);
            float stuckTime = 0;
            float stuckThreshold = 0.2f;  // Время в секундах, после которого считается, что враг застрял

            while ((targetPosition - (Vector2)transform.position).magnitude > 0.1f)
            {
                if ((Vector2)transform.position == startPosition)
                {
                    stuckTime += Time.deltaTime;
                    if (stuckTime >= stuckThreshold)
                    {
                        break;  // Прерываем рывок, если враг застрял
                    }
                }
                else
                {
                    stuckTime = 0;  // Сброс таймера застревания
                }

                startPosition = transform.position;
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, dashSpeed * Time.deltaTime);
                yield return null;
            }

            bodyCollider.enabled = true;
            yield return new WaitForSeconds(1.5f);  // Задержка после рывка

            isDashing = false;
            agent.isStopped = false;
            canMove = true;
            DemonAnims.SetTrigger("Go");
        }


        void OnTriggerEnter2D(Collider2D collision)
        {
            if (isDashing && collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<Rogalik.Scripts.Player.RoguelikePlayer>().GetDamage(2);
            }
        }
    }

}