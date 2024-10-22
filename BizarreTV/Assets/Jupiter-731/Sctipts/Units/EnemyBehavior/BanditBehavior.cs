using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jupiter731
{
    public class BanditBehavior : BaseBehavior
    {
        [SerializeField, Range(0, 15)] float walkingRadius;
        [SerializeField, Range(0, 15)] float agrRadius;
        [SerializeField, Range(0, 15)] float evadeRadius;
        [SerializeField, Range(0, 1)] float targetPositionAccuracy;
        [SerializeField, Range(0, 10)] float speed;
        [SerializeField] Fire gun;
        [SerializeField] Transform weapon;
        [SerializeField] Rigidbody2D body;
        [SerializeField] Transform centre;
        private Vector2 _targetPosition;
        private Vector2 _startPosition;
        private bool _isWalking = false;

        private void Start()
        {
            _startPosition = body.position;
        }

        override protected void Attack()
        {
            if (gun != null)
            {
                if (((Vector2)centre.position -
                    (Vector2)player.transform.position).magnitude < agrRadius)
                {
                    TrackPlayer();
                    gun.OpenFire();
                }
            }
        }

        override protected void Chill()
        {
            if (!_isWalking)
            {
                var xTarget = Random.Range(-(walkingRadius + _startPosition.x - centre.position.x),
                    walkingRadius - _startPosition.x - centre.position.x);
                var yTarget = Random.Range(-(walkingRadius + _startPosition.y - centre.position.y),
                    walkingRadius - _startPosition.y - centre.position.y);
                Debug.Log( "новая цель выбрана"+xTarget.ToString() + " " + yTarget);
                _targetPosition = new Vector2(xTarget, yTarget);
                _isWalking = true;
                _targetPosition -= _startPosition;
            }
            //else if (_targetPosition.x - _startPosition.x> walkingRadius || _targetPosition.y - _startPosition.y > walkingRadius)
            //{
            //    while (_targetPosition.x - _startPosition.x > walkingRadius || _targetPosition.y - _startPosition.y > walkingRadius)
            //    {
            //        if (_targetPosition.x - centre.position.x > walkingRadius && _targetPosition.y - centre.position.y > walkingRadius)
            //        {
            //            _targetPosition.x -= 0.05f * Mathf.Sign(_targetPosition.x);
            //            _targetPosition.y -= 0.05f * Mathf.Sign(_targetPosition.y);
            //        }
            //        else if (_targetPosition.x - centre.position.x > walkingRadius)
            //        {
            //            _targetPosition.x -= 0.05f * Mathf.Sign(_targetPosition.x);
            //        }
            //        else if (_targetPosition.y - centre.position.y > walkingRadius)
            //        {
            //            _targetPosition.y -= 0.05f * Mathf.Sign(_targetPosition.y);
            //        }
            //    }
            //    Debug.Log((_targetPosition + (Vector2)centre.position - _startPosition).magnitude.ToString() +" " + _targetPosition + " " + (Vector2)centre.position + " " + _startPosition);

            //}
            else  
            {
                //Debug.Log(_targetPosition);
                if (Vector2.Distance((Vector2)centre.position, _startPosition) >= walkingRadius)
                {
                    Move(_startPosition - (Vector2)centre.position);
                    _isWalking = false;
                }
                else if (Vector2.Distance(_targetPosition, (Vector2)centre.position - _startPosition) < targetPositionAccuracy)
                {
                    Stop();
                    Debug.Log("eeeeeeeeeeeeeeeeee");
                    _isWalking = false;
                }
                else
                {
                    Move(_targetPosition.normalized);
                }
            }
                    //Debug.Log(_targetPosition.ToString() + ' ' + (Vector2)transform.position);
        }

        override protected void MoveAndAttack()
        {

        }

        override protected void Evade()
        {
            
        }

        override protected void FindTheWay()
        {

        }

        private void TrackPlayer()
        {
            transform.rotation = Quaternion.LookRotation(player.transform.position
                - centre.position);
        }

        private void Move(Vector2 target)
        {
            if (_isWalking)
            {
                body.velocity = target * speed;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_startPosition, walkingRadius);
        }

        IEnumerator Delay()
        {
            yield return new WaitForEndOfFrame();
        }

        private void Stop()
        {
            body.velocity = Vector2.zero;
        }
    }
}
