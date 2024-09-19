using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class MeleeEnemy : MonoBehaviour
{
    public Transform player;  // Ссылка на трансформ игрока
    public float attackDistance = 2f;  // Дистанция атаки
    public float attackCooldown = 0.45f;  // Кулдаун между атаками
    public Animator MeleeAnim;  // Аниматор для врага
    private NavMeshAgent agent;  // Компонент NavMeshAgent для навигации
    private bool isAttacking = false;  // Флаг, указывающий, что враг сейчас атакует
    private bool canAttack = true;  // Флаг, можно ли начать новую атаку
    private Vector2 lastPosition;  // Последняя позиция врага для определения направления движения
    public Collider2D colliderMy;
    public Collider2D colliderPlayer;

    void Awake()
    {
        Physics2D.IgnoreCollision(colliderMy, colliderPlayer);
        agent = GetComponent<NavMeshAgent>();  // Получаем компонент NavMeshAgent
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        lastPosition = transform.position;
    }

    void Update()
    {
        MoveTowardsPlayer();

        // Поворот спрайта в зависимости от направления движения
        if (transform.position.x > lastPosition.x)
            transform.localScale = new Vector3(5, 5, 1);
        else if (transform.position.x < lastPosition.x)
            transform.localScale = new Vector3(-5, 5, 1);

        lastPosition = transform.position;
    }

    void MoveTowardsPlayer()
    {
        if (isAttacking)
        {
            agent.isStopped = true;
            return;
        }

        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, attackDistance);
        bool playerNearby = false;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                playerNearby = true;
                break;
            }
        }

        if (playerNearby)
        {
            agent.isStopped = true;
            if (canAttack)
            {
                StartCoroutine(AttackPlayer());
            }
        }
        else
        {
            agent.isStopped = false;
            agent.SetDestination(player.position);
        }
    }

    private IEnumerator AttackPlayer()
    {
        isAttacking = true;
        canAttack = false;
        MeleeAnim.SetTrigger("Attack");  // Активация анимации атаки
        yield return new WaitForSeconds(attackCooldown);  // Ожидание в течение кулдауна
        SoundManager.Instance.PlaySound(2);

        // Проверяем, все еще ли игрок в радиусе действия после атаки
        Collider2D[] collidersInAttackRange = Physics2D.OverlapCircleAll(transform.position, attackDistance);
        foreach (Collider2D collider in collidersInAttackRange)
        {
            if (collider.CompareTag("Player"))  // Проверка на тег "Player"
            {
                collider.GetComponent<Rogalik.Scripts.Player.RoguelikePlayer>().GetDamage(1);  // Нанесение урона игроку
                break;  // Если мы нашли игрока и нанесли урон, дальнейший поиск не нужен
            }
        }

        yield return new WaitForSeconds(attackCooldown);  // Ожидание перед следующей возможной атакой

        canAttack = true;
        isAttacking = false;
    }
}
