using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grater : MonoBehaviour
{
    public bool isOnPlace;
    pickableObject po;
    void Start()
    {
        po = GetComponent<pickableObject>();
    }

    // Update is called once per frame
    void Update()
    {
        isOnPlace = po.isTouchingGoal;
    }
}
