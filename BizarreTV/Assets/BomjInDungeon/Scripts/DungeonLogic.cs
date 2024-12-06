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
        public GameObject Hud;
        public GameObject DeathHud;
        public GameObject WinHud;
        public GameObject PlayerPrefab;

        private GameObject player;
        private GameObject hud;

        public List<Room> rooms = new List<Room>();

        public static string keystone = "0000";
        public int idRoomIn = 0;
        private void Awake()
        {
            player = Instantiate(PlayerPrefab);
            player.name = "Bomj";
            room.player = player;

            EventManager.Instance.Subscribe("MoveUp", MoveUp);
            EventManager.Instance.Subscribe("MoveRight", MoveRight);
            EventManager.Instance.Subscribe("MoveDown", MoveDown);
            EventManager.Instance.Subscribe("MoveLeft", MoveLeft);
            EventManager.Instance.Subscribe("PlayerDead", PlayerDead);
            EventManager.Instance.Subscribe("Retry", StartDungeon);
            EventManager.Instance.Subscribe("boss", BossEntered);
        }

        private void Start()
        {
            StartDungeon();
        }
        
        public void PlayerDead()
        {
            Instantiate(DeathHud);
            Destroy(hud);
        }
        public void BossEntered()
        {
            Instantiate(WinHud);
            Destroy(player);
            Destroy(hud);
        }
        public void StartDungeon()
        {
            if(player == null)
            {
                player = Instantiate(PlayerPrefab);
                player.name = "Bomj";
                room.player = player;
            }

            player.transform.position = Vector3.zero;
            hud = Instantiate(Hud);

            rooms = generator.GenerateMap();
            rooms = generator.GenerateObjects(rooms);
            ClearMovement();

            player.GetComponent<HpManager>().Restart();

            EventManager.Instance.TriggerEvent("GameRestarted");
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
        public void ClearMovement() 
        {
            idRoomIn = generator.GetPlayerStartRoom();
            room.ClearRoom();
            room.gameObject.transform.position = Vector3.zero;
            room.PlaceRoom(rooms[idRoomIn], this);
        }

        public void DeleteEnemy(int _roomID, int _type, Vector2 position)
        {
            rooms[_roomID].EnemiesPositions[_type].Remove(position);
        }
    }
}
