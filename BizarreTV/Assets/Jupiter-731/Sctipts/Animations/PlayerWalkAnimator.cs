using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkAnimator : MonoBehaviour
{
    [SerializeField] Animator walkAnimator;
    private Vector2 _prevPoint = Vector2.zero;
    private Vector2 _currPoint = Vector2.zero;


    private void FixedUpdate()
    {
        _prevPoint = _currPoint;
        _currPoint = transform.position;
        if ((_prevPoint - _currPoint).magnitude > 0.1f)
        {
            PlayAnimation(true);
        }
        else
        {
            PlayAnimation(false);
        }
    }
    public void PlayAnimation(bool isMove)
    {
        walkAnimator.SetBool("isWalk", isMove);
    }

}
