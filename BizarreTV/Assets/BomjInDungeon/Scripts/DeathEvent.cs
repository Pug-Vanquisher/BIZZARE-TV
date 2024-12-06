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
        public GameObject Corpse;
        public void CollectEvent(DungeonLogic _logic, int _roomID, int _type, Vector2 _position)
        {
            logic = _logic;
            roomID = _roomID;
            type = _type;
            position = _position;
        }
        public void Death()
        {
            var a = Instantiate(Corpse, logic.gameObject.transform.GetChild(2));
            a.transform.position = transform.position;
            logic.DeleteEnemy(roomID, type, position);
            
        }
    }
}
