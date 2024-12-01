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
        [SerializeField, Range(0, 100)] float chillingChancePercent = 75;
        [SerializeField, Range(0, 5)] float chillingTime = 2;
        [SerializeField, Range(0, 5)] float noChillingTime = 2;
        [SerializeField, Range(0.05f, 20), Tooltip("Сколько секунд надо на 1 выстрел")] float SecondPerBullet;
        [SerializeField] Fire gun;
        [SerializeField] Transform weapon;
        [SerializeField] Rigidbody2D body;
        [SerializeField] Transform centre;
        [SerializeField] LayerMask maskToBypass;
        private Vector2 _targetPosition;
        private Vector2 _startPosition;
        private bool _isWalking = false;
        private bool _isChilling = false;
        private bool _isFlipped = false;
        private float _currChillTime = 100000;
        private float _currNoChillTime = 0;
        private float _currFireSecond = 0;
        private float _angleToTarget = 0;

        private void Awake()
        {
            Application.targetFrameRate =1000;
            _startPosition = centre.position;
        }

        override protected void Attack()
        {
            if (gun != null)
            {
                if (((Vector2)centre.position -
                    (Vector2)player.transform.position).magnitude < agrRadius)
                {
                    TrackPlayer();
                    if (_currFireSecond >= SecondPerBullet)
                    {
                        _currFireSecond = 0;
                        gun.OpenFire();
                    }
                    else
                    {
                        _currFireSecond += Time.fixedDeltaTime;
                    }
                }
                Chill();
            }
        }

        override protected void Chill()
        {
            if (((Random.value > chillingChancePercent / 100 || _isChilling) || 
                _currNoChillTime > noChillingTime) && _currNoChillTime > noChillingTime) // всратенько, переделать мб
            {
                if (_currChillTime > chillingTime && _isChilling)
                {
                    _isChilling = false;
                    _currNoChillTime = chillingTime;
                    _currChillTime = 0;
                }
                else
                {
                    if (!_isChilling)
                    {
                        _currNoChillTime = 0;
                        _isChilling = true;
                    }
                    else
                    {
                        _currChillTime += Time.fixedDeltaTime;
                    }
                }
            }
            else if (!_isWalking)
            {
                var xTarget = Random.Range(-(walkingRadius),
                    walkingRadius);
                var yTarget = Random.Range(-(walkingRadius),
                    walkingRadius);
                //Debug.Log( "новая цель выбрана"+xTarget.ToString() + " " + yTarget);
                _targetPosition = new Vector2(xTarget, yTarget) * 0.97f + _startPosition;

                _isWalking = true;
            }

            else if (_isWalking)
            {
                _currNoChillTime += Time.fixedDeltaTime;
                //Debug.Log(_targetPosition);
                if (Vector2.Distance((Vector2)centre.position, _startPosition) > walkingRadius)
                {
                    Move(_startPosition - (Vector2)centre.position);
                    _isWalking = false;
                    //Debug.Log(_startPosition - (Vector2)centre.position);
                }
                else if (Vector2.Distance(_targetPosition, (Vector2)centre.position - _startPosition) < targetPositionAccuracy)
                {
                    Stop();

                    _isWalking = false;
                }
                else
                {
                    //Debug.Log(_targetPosition);
                    Move(_targetPosition);
                }
            }
            FindTheWay();

            //Debug.Log(_targetPosition.ToString() + ' ' + (Vector2)transform.position);
            //Debug.Log(centre.position + "   " + _startPosition);
        }

        override protected void MoveAndAttack()
        {
            Attack();
            if (((Vector2)centre.position - (Vector2)player.transform.position).magnitude <= evadeRadius)
            {
                Evade();
            }
            //else if(((Vector2)centre.position - (Vector2)player.transform.position).magnitude <= evadeRadius - walkingRadius/3)
            //{
            //    Chill();
            //}
            else if (((Vector2)centre.position - (Vector2)player.transform.position).magnitude > agrRadius)
            {
                Chill();
            }
        }

        override protected void Evade()
        {
            
            if (player != null)
            {
                if (((Vector2)centre.position - (Vector2)player.transform.position).magnitude < evadeRadius)
                {
                    _isWalking = true;
                    Move((Vector2)(player.transform.position - centre.position) * -1);
                }
                else
                {
                    _isWalking = false;
                }

                _startPosition = transform.position;
                centre.position = _startPosition;

            }

        }

        override protected void FindTheWay()
        {

            var obstacle = Physics2D.CircleCast((Vector2)transform.position, walkingRadius, _targetPosition, evadeRadius/2, maskToBypass);
            if (obstacle.collider != null && (obstacle.collider.transform.position - transform.position).magnitude < walkingRadius * 1.2f)
            {
                Move((Vector2)(obstacle.collider.transform.position - transform.position) * -1.5f);
                _isWalking = false;
            }
            else if (!_isWalking && obstacle.collider == null)
            {
                _startPosition = transform.position;
            }
        }

        private void TrackPlayer()
        {
            weapon.rotation = Quaternion.FromToRotation(Vector3.right, player.transform.position - weapon.position);
            _angleToTarget = Vector2.SignedAngle(Vector3.right, (Vector2)player.transform.position - _startPosition);
            Flip();
        }

        private void Move(Vector2 target)
        {
            if (_isWalking)
            {
                body.velocity = target.normalized * speed;
            }

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(_startPosition, walkingRadius);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, agrRadius);
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, evadeRadius);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(centre.position, walkingRadius);
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(_targetPosition, 0.25f);
        }

        private void Flip()
        {
            if ((_angleToTarget > 90 || _angleToTarget < -90) && !_isFlipped)
            {
                _isFlipped = true;
                var scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
                scale = weapon.transform.localScale;
                scale.x *= -1;
                scale.y *= -1;
                weapon.transform.localScale = scale;
            }
            else if (_isFlipped&& (_angleToTarget < 90 && _angleToTarget > -90))
            {
                _isFlipped= false;
                var scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
                scale = weapon.transform.localScale;
                scale.x *= -1;
                scale.y *= -1;
                weapon.transform.localScale = scale;
            }

        }

        private void Stop()
        {
            body.velocity = Vector2.zero;
        }
    }
}
