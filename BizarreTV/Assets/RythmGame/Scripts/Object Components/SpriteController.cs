using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] GameObject sprt;
    public Sprite[] sprites;
    [SerializeField] int lastMovementSpriteIndex;
    public void Start(){
        _spriteRenderer = sprt.GetComponent<SpriteRenderer>();
    }
    public void ChangeSpriteFromMovement(Vector3 diff){
        switch(diff){
            case Vector3 v when v.Equals(Vector3.forward):
                _spriteRenderer.sprite = sprites[0];
                lastMovementSpriteIndex = 0;
                break;
            case Vector3 v when v.Equals(Vector3.back):
                _spriteRenderer.sprite = sprites[3];
                lastMovementSpriteIndex = 3;
                break;
            case Vector3 v when v.Equals(Vector3.left):
                if (lastMovementSpriteIndex == 0){
                    _spriteRenderer.sprite = sprites[1];
                    lastMovementSpriteIndex = 1;
                    break;
                } else {
                    _spriteRenderer.sprite = sprites[4];
                    lastMovementSpriteIndex = 4;
                    break;
                }
            case Vector3 v when v.Equals(Vector3.right):
                if (lastMovementSpriteIndex == 1){
                    _spriteRenderer.sprite = sprites[2];
                    lastMovementSpriteIndex = 2;
                    break;
                } else {
                    _spriteRenderer.sprite = sprites[5];
                    lastMovementSpriteIndex = 5;
                    break;
                }
        }
    }
    public void ChangeSpriteFromAttack(int sprite){
        _spriteRenderer.sprite = sprites[sprite];
    }
}
