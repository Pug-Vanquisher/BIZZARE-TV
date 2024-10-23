using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BID
{
    public class CameraScript : MonoBehaviour
    {
        public RoomSpawner rs;
        public Transform pl;
        private void Update()
        {

            transform.position = new Vector3(Mathf.Clamp(pl.transform.position.x, 0, 0), 
                Mathf.Clamp(pl.transform.position.y, -rs.roomsize.y * 4 + 16, rs.roomsize.y * 4 - 16), -50);
        }
    }

}