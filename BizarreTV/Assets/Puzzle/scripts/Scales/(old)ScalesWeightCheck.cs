using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScalesWeightCheck : MonoBehaviour
{
    [SerializeField]
    private float crntWeightSum;

    List<ResizeObject> crntObjectsOn = new List<ResizeObject>();

    private void Start()
    {
        crntWeightSum = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<ResizeObject>())
        {
            crntObjectsOn.Add(other.GetComponent<ResizeObject>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<ResizeObject>())
        {
            crntObjectsOn.Remove(other.GetComponent<ResizeObject>());
        }
    }

    private void Update()
    {
        crntWeightSum = 0;
        for (int i = 0; i < crntObjectsOn.Count; i++)
        {
            crntWeightSum += crntObjectsOn[i].GetWeight();
        } 
    }


}
