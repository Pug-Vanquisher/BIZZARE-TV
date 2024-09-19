using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MovementController : MonoBehaviour
{
    [SerializeField] private EventManager m_EventManager; 
    [SerializeField] private SpriteController spriteController;
    public float Cooldown;
    public float MovementSpeed = 8f;
    public int StepScale = 3;

    public void Awake(){
        spriteController = GetComponent<SpriteController>();
    }
    public Vector3 ChangePos(Vector3 Diff, Vector3 CurrentPos, Vector3 EndPos) {
        if (Obstaclecheck(Diff * StepScale, CurrentPos)){
            return CurrentPos;
        }
        if (CurrentPos == EndPos){
            EndPos = CurrentPos + Diff * StepScale;
            Cooldown = 0;
            gameObject.GetComponent<SpriteController>()?.ChangeSpriteFromMovement(Diff);
            return EndPos;
        } else {
            return EndPos;
        }
    }

    bool Obstaclecheck(Vector3 Diff, Vector3 CurrentPos) {
        if (Physics.Raycast(CurrentPos, Diff, out RaycastHit hit, 2f)){
            if (hit.collider.gameObject.tag == "Wall"){
                return true;
            }
            if ((hit.collider.gameObject.tag == "Enemy") | (hit.collider.gameObject.tag == "Boss")){
                gameObject.GetComponent<Player>()?.Attack(hit.collider.gameObject);    
                return false;
            }
        }
        return false;
    }
    public void Move(Vector3 CurrentPos, Vector3 EndPos) {
        Cooldown += Time.deltaTime;
        transform.position = Vector3.Lerp(CurrentPos, EndPos, Mathf.SmoothStep(0, 1, (Cooldown * MovementSpeed)));
    }
}