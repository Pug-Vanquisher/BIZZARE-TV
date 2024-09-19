using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCR : MonoBehaviour
   {
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            CheckpointManager.lastCheckpointPosition = transform.position;
        }
    }
}
