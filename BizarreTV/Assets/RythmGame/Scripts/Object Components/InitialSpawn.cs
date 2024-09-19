using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialSpawn : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform spawnPos;
    void Start(){
        spawnPos = GetComponent<Transform>();
        Debug.Log(spawnPos.position);
        if (GameObject.FindWithTag("Player") == null){
            Instantiate (playerPrefab, spawnPos.position + Vector3.up * 2f, Quaternion.identity);
        } else {
            GameObject.FindWithTag("Player").GetComponent<Player>().Teleport(spawnPos.position + Vector3.up * 2f);
        }
    }
}
