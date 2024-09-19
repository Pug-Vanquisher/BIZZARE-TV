using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform playerTransform;
    public float positionSpeed = 5f;
    int layer;
    GameObject previousWall = null;
    GameObject currentWall = null;

    void Awake()
    {
        layer = LayerMask.NameToLayer("Walls");
    }

    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, playerTransform.position - transform.position, out RaycastHit hit, 3f)){
            if (hit.collider.gameObject.layer == layer){
                previousWall = currentWall;
                currentWall = hit.collider.gameObject;
                WallSink(currentWall);
                if (previousWall != null) {WallRaise(previousWall);}
            }
        } 
    }

    void WallSink(GameObject wall){
        if (wall.transform.position.y != 0){
            wall.transform.position = wall.transform.position + Vector3.down * 4;
        }
    }
    void WallRaise(GameObject wall){
        if (wall.transform.position.y != 4){
            wall.transform.position = wall.transform.position + Vector3.up * 4;
        }
    }
}
