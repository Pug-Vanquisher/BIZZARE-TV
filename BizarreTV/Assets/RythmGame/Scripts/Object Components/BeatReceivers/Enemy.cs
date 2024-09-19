using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : BeatReceiver
{
    [SerializeField] MovementController movementController;
    [SerializeField] GameObject Warning;
    [SerializeField] GameObject Hitbox;
    [SerializeField] Transform PlayerPos;
    [SerializeField] Transform EnemyPos;
    [SerializeField] Vector3 EnemyEndPos;
    [SerializeField] Vector3[] direction;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip[] steps;
    [SerializeField] AudioSource source;
    [SerializeField] int CurrentStep;
    [SerializeField] Animator animator;
    void Start(){
        movementController = GetComponent<MovementController>();
        PlayerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
        EnemyPos = GetComponent<Transform>();
        EnemyEndPos = EnemyPos.position;
        CurrentStep = 0;
    }
    public override void OnBeatSended(int beat){
        if (Vector3.Distance(EnemyPos.position, PlayerPos.position) > 2.5f){
            EnemyEndPos = movementController.ChangePos(direction[(CurrentStep) % direction.Length], EnemyPos.position, EnemyEndPos);
            source.PlayOneShot(steps[CurrentStep % 4], 1f);
            CurrentStep++;
            Warning.SetActive(false);
            Hitbox.SetActive(false);
        } else {
            Attack();
        }
    }

    void Attack (){
        gameObject.GetComponent<SpriteController>().ChangeSpriteFromAttack(6);
        if (Warning.activeSelf) {
            Hitbox.SetActive(true);
            gameObject.GetComponent<SpriteController>().ChangeSpriteFromAttack(7);
        }
        Warning.SetActive(true);
    }
    public void Die(){
        m_EventManager.PlaySoundEffect(death, true);
        Destroy(gameObject);
    }
    void FixedUpdate()
    {
        if (Mathf.Abs(EnemyPos.position.x - EnemyEndPos.x) < 0.1f && Mathf.Abs(EnemyPos.position.z - EnemyEndPos.z) < 0.1f){
            animator.SetBool("standing", true);
        } else {
            animator.SetBool("standing", false);
        }
        movementController.Move(EnemyPos.position, EnemyEndPos);
    }
}