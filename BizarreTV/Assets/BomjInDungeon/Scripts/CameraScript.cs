using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BID
{
    public class CameraScript : MonoBehaviour
    {
        public RoomSpawner rs;
        public Transform pl;
        public Transform room;
        public float _shakeDuration;
        public float _magnitude;
        public Vector3 _shakePosition;
        private void Start()
        {
            EventManager.Instance.Subscribe("CrystalDestroyed", CrystalDestroyed);
        }
        private void Update()
        {
            Vector3 direction = pl.position - room.position;
            transform.position = room.position + direction.normalized * Mathf.Clamp(direction.magnitude, 0, rs.roomsize.magnitude) + _shakePosition;
            transform.position = new Vector3(transform.position.x, transform.position.y, -50f);
        }
        public void CrystalDestroyed()
        {
            StartCoroutine(_Shake());
            EventManager.Instance.Unsubscribe("CrystalDestroyed", CrystalDestroyed);
        }
        IEnumerator _Shake()
        {

            float x;
            float y;
            float timeLeft = Time.time;

            while ((timeLeft + _shakeDuration) > Time.time)
            {
                x = Random.Range(-_magnitude, _magnitude) * timeLeft / Time.time;
                y = Random.Range(-_magnitude, _magnitude) * timeLeft / Time.time;
                _shakePosition = new Vector3(x , y); yield return new WaitForSeconds(0.025f);
            }
        }

    }

}