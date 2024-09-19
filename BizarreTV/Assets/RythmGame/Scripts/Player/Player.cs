using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    MovementController movementController;
    [SerializeField] private EventManager m_EventManager;
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] AudioClip death;
    [SerializeField] Animator animator;
    public float Cooldown;

    void Awake()
    {
        movementController = GetComponent<MovementController>();
        player = GetComponent<Transform>();
        endPosition = player.position;

        m_EventManager.OnRespawn += OnRespawn;
        m_EventManager.OnCheckpointOverride += OnCheckpointOverride;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)){
            endPosition = movementController.ChangePos(Vector3.forward, player.position, endPosition);
            animator.SetBool("standing", false);
            return;
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)){
            endPosition = movementController.ChangePos(Vector3.left, player.position, endPosition);
            animator.SetBool("standing", false);
            return;
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)){
            endPosition = movementController.ChangePos(Vector3.back, player.position, endPosition);
            animator.SetBool("standing", false);
            return;
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)){
            endPosition = movementController.ChangePos(Vector3.right, player.position, endPosition);
            animator.SetBool("standing", false);
            return;
        }
    }

    void FixedUpdate()
    {
        movementController.Move(player.position, endPosition);
        animator.SetBool("standing", true);
    }

    void OnCheckpointOverride(Vector3 CheckpointPosition){
        startPosition = CheckpointPosition;
    }
    void OnRespawn(){
        endPosition = startPosition + Vector3.up;
        player.position = startPosition + Vector3.up;
        m_EventManager.PlaySoundEffect(death, true);
    }
    public void Attack(GameObject Enemy){
        if (Enemy.gameObject.tag == "Enemy"){
            Enemy.GetComponent<Enemy>().Die();
        }
        if (Enemy.gameObject.tag == "Enemy"){
            Enemy.GetComponent<Enemy>().Die();
        }
    }

    public void Teleport(Vector3 spawnpoint){
        endPosition = spawnpoint;
    }
}
