using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationFixer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.centerOfMass = Vector3.zero;
        rb.inertiaTensorRotation = Quaternion.identity;
        
    }


}
