using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BID
{
    public class DeathEvent : MonoBehaviour
    {
        private DungeonLogic logic;
        private int roomID;
        private int type;
        private Vector2 position;
        public void CollectEvent(DungeonLogic _logic, int _roomID, int _type, Vector2 _position)
        {
            logic = _logic;
            roomID = _roomID;
            type = _type;
            position = _position;
        }
        public void Death()
        {
            logic.DeleteEnemy(roomID, type, position);
        }
    }
}
