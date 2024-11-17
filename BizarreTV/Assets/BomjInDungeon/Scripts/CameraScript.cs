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
        private void Update()
        {
            Vector3 direction = pl.position - room.position;
            transform.position = room.position + direction.normalized * Mathf.Clamp(direction.magnitude, 0, rs.roomsize.magnitude);
            transform.position = new Vector3(transform.position.x, transform.position.y, -50f);
        }
    }

}