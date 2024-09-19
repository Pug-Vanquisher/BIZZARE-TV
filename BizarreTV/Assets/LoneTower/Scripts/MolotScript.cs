using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MolotScript : MonoBehaviour
{
    [SerializeField] private bool setAtack, setRoad;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] public float speed;

    [SerializeField] public GameObject molot;
    [SerializeField] private GameObject startPoint, endPoint;
    [SerializeField] private Transform[] points;

    private int pointIndex = 0;
    public float uron = 0;
    private bool back = true;

    private void Start()
    {
        molot.transform.position = startPoint.transform.position;
        endPoint.transform.position = startPoint.transform.position;
    }

    private void Update()
    {
        if (setAtack)
        {
            Shoot();
        }
        else if (setRoad)
        {
            MoveByPoints();
        }
    }

    private void Shoot()
    {
        if (back)
        {
            molot.transform.position = Vector3.MoveTowards(molot.transform.position, endPoint.transform.position,
            speed * Time.deltaTime);
        }
        else
        {
            molot.transform.position = Vector3.MoveTowards(molot.transform.position, startPoint.transform.position,
                speed * Time.deltaTime);
        }
        if (molot.transform.position == endPoint.transform.position)
        {
            back = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.gameObject.tag == "Enemy") && (molot.transform.position == startPoint.transform.position))
        {
            endPoint.transform.position = col.transform.position;
            back = true;
        }
    }

    private void MoveByPoints()
    {
        molot.SetActive(true);
        if (pointIndex <= points.Length - 1)
        {
            molot.transform.position = Vector3.MoveTowards(molot.transform.position, points[pointIndex].transform.position,
                speed * Time.deltaTime);
            if (molot.transform.position == points[pointIndex].transform.position)
            {
                pointIndex++;
            }
            if (pointIndex == points.Length)
            {
                pointIndex = 0;
            }
        }
    }

    public void SetAtack(bool value)
    {
        setAtack = value;
    }

    public void SetRoad(bool value)
    {
        setRoad = value;
    }

}
