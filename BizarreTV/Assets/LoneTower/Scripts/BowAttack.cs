using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BowAttack : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] private bool setActive;
    [SerializeField] private bool secondTarget;
    [SerializeField] public GameObject Luchnick;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5.4f;
    private float bps=1f;
    private float rotZ;

    private Transform target;
    private float TimeUntilFire;

    private void Update()
    {
        Tower tower = GameObject.FindGameObjectWithTag("Tower").GetComponent<Tower>();
        bps = tower.speed;
        if (target == null)
        {
            FindTarget();
            return;
        }

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            TimeUntilFire += Time.deltaTime;
            if ((TimeUntilFire >= 1f / bps) && (setActive == true))
            {
                TimeUntilFire = 0f;
                Shoot();
            }
        }

    }

    private void Shoot()
    {
        GameObject bulletObj = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);
        AudioManagerTower.Instance.PlaySFX("Shoot");
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange,
            (Vector2)transform.position, 0f, enemyMask);

        if (hits.Length == 1)
        {
            target = hits[0].transform;
        }
        else if (hits.Length > 1)
        {
            if (secondTarget)
            {
                target = hits[1].transform;
            }
            else
            {
                target = hits[0].transform;
            }
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }
/*
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
*/
    public void SetActive(bool val)
    {
        setActive = val;
    }

}
