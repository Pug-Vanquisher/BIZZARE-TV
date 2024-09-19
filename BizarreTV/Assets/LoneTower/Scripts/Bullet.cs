using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float strenght, time = 1f;

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 5f;
    public bool setKnockback = false;
    private float damage;

    private Transform target;

    private void Start()
    {
        StartCoroutine(DestroyAfterSeconds());
    }

    private void Update()
    {
        Tower tower = GameObject.FindGameObjectWithTag("Tower").GetComponent<Tower>();
        damage = tower.damage;
    }

    public void SetTarget(Transform _target)
    {
        this.target = _target;
    }

    private void FixedUpdate()
    {
        if (!target) return;
        Vector3 direction = (target.position - transform.position).normalized;
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ+180f);
        rb.velocity = direction * bulletSpeed;
    }

    private IEnumerator Reset(Rigidbody2D enemy)
    {
        if (enemy != null)
        {
            yield return new WaitForSeconds(time);
            enemy.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyMove>().TakeDamage(damage);
            if (setKnockback)
            {
                Vector2 direction = (other.transform.position - transform.position).normalized;
                other.rigidbody.AddForce(direction * strenght, ForceMode2D.Impulse);
                StartCoroutine(Reset(other.rigidbody));
            }
            Destroy(this.gameObject);
        }
    }

    IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(5f);
        Object.Destroy(gameObject);
    }

}
