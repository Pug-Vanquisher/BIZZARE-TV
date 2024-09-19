using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeRotate : MonoBehaviour
{
    public GameObject lookAtWhere;
    Vector3 dir;
    void FixedUpdate()
    {
        dir = lookAtWhere.transform.position - transform.position;
        float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }
}
