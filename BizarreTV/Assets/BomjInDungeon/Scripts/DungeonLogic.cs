using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BID
{
    public class Room
    {
        public int id;

        public Vector2 size = new Vector2(7, 4);
        public string tag = "none";
        public List<int> doors = new List<int>() { 0, 0, 0, 0};
        public int danger = 0;

        public Dictionary<int, List<Vector2>> EnemiesPositions = new Dictionary<int, List<Vector2>>();
    }

    public class DungeonLogic : MonoBehaviour
    {
        public string[] enemies;

        public RoomSpawner room;
        public DungeonGenerator generator;
        private PlayerMover player;

        public List<Room> rooms = new List<Room>();


        public int idRoomIn = 0;

        private void Start()
        {
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerMover>();
            }
            EventManager.Instance.Subscribe("MoveUp", MoveUp);
            EventManager.Instance.Subscribe("MoveRight", MoveRight);
            EventManager.Instance.Subscribe("MoveDown", MoveDown);
            EventManager.Instance.Subscribe("MoveLeft", MoveLeft);

            rooms = generator.GenerateMap();
            rooms = generator.GenerateObjects(rooms);
            idRoomIn = generator.GetPlayerStartRoom();
            room.PlaceRoom(rooms[idRoomIn], this);
        }
        public void MoveUp()
        {
            idRoomIn += (int)generator.dungeonSize.x;
            room.ClearRoom();
            room.gameObject.transform.position = player.transform.position + new Vector3(0, rooms[idRoomIn].size.y - 1) * 4;
            room.PlaceRoom(rooms[idRoomIn], this);

}
        public void MoveRight()
        {
            idRoomIn += 1;
            room.ClearRoom();
            room.gameObject.transform.position = player.transform.position + new Vector3(rooms[idRoomIn].size.x - 1, 0) * 4;
            room.PlaceRoom(rooms[idRoomIn], this);
        }
        public void MoveDown()
        {
            idRoomIn -= (int)generator.dungeonSize.x;
            room.ClearRoom();
            room.gameObject.transform.position = player.transform.position + new Vector3(0, -rooms[idRoomIn].size.y + 2) * 4;
            room.PlaceRoom(rooms[idRoomIn], this);
        }
        public void MoveLeft()
        {
            idRoomIn -= 1;
            room.ClearRoom();
            room.gameObject.transform.position = player.transform.position + new Vector3(-rooms[idRoomIn].size.x + 1, 0) * 4;
            room.PlaceRoom(rooms[idRoomIn], this);
        }

        public void DeleteEnemy(int _roomID, int _type, Vector2 position)
        {
            rooms[_roomID].EnemiesPositions[_type].Remove(position);
        }
    }
}
