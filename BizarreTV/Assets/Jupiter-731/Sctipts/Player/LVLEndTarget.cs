using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVLEndTarget : MonoBehaviour
{
    [SerializeField] GameObject target;
    private void Update()
    {
        Track();
    }

    void Track()
    {
        gameObject.transform.rotation = Quaternion.FromToRotation(Vector3.up,target.transform.position - transform.position);
        Debug.Log(Quaternion.LookRotation(target.transform.position));
    }
}
