using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace BID
{
    public class MinimapRoom
    {
        public int id;
        public int childId;
        public string tag;
        public MinimapRoom()
        {

        }
        public MinimapRoom(int _id, int _childId, string _tag)
        {
            id = _id;
            childId = _childId;
            tag = _tag;
        }
    }

    public class Minimap : MonoBehaviour
    {
        public DungeonGenerator generator;
        public GameObject Room;
        public GameObject door;
        private int roomId;
        public List<Room> rooms = new List<Room>();

        public List<MinimapRoom> minimapRooms = new List<MinimapRoom>();

        public TMP_Text dangerLevel;
        
        public float magnitude;
        private Vector3 _originalPosition = new Vector3(8.8f, -16.5f, 0);

        Dictionary<string, Color> strtocol = new Dictionary<string, Color>()
        {
            {"default", Color.white},
            {"playerRoom", Color.green },
            {"keyRoom", Color.yellow },
            {"bossRoom", Color.magenta }
        };

        private void Awake()
        {
            EventManager.Instance.Subscribe("GenEnd", MiniMap);
            EventManager.Instance.Subscribe("MoveUp", MoveUp);
            EventManager.Instance.Subscribe("MoveRight", MoveRight);
            EventManager.Instance.Subscribe("MoveDown", MoveDown);
            EventManager.Instance.Subscribe("MoveLeft", MoveLeft);
        }
        
        private void Start()
        {
            if(generator == null)
            {
                generator = GameObject.FindGameObjectWithTag("Brain")?.GetComponent<DungeonGenerator>();
            }
            
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                ShowMap();
            }
        }

        public void MoveUp()
        {
            roomId += (int)generator.dungeonSize.x;
            PointRoom(roomId);
        }
        public void MoveRight()
        {
            roomId += 1;
            PointRoom(roomId);
        }
        public void MoveDown()
        {
            roomId -= (int)generator.dungeonSize.x;
            PointRoom(roomId);
        }
        public void MoveLeft()
        {
            roomId -= 1;
            PointRoom(roomId);
        }
        void MiniMap()
        {
            if (generator == null)
            {
                generator = GameObject.FindGameObjectWithTag("Brain")?.GetComponent<DungeonGenerator>();
            }
            rooms = generator.rooms;
            roomId = generator.GetPlayerStartRoom();
            PointRoom(roomId);
        }

        void PointRoom(int id)
        {
            dangerLevel.text = rooms[id].danger.ToString();
            dangerLevel.color = new Color(1f, 1f - (rooms[id].danger - 1) / 9f, 1f - (rooms[id].danger - 1) / 9f);
            magnitude = 5 * ((rooms[id].danger - 1) / 9f);
            Shake();

            foreach (MinimapRoom room in minimapRooms)
            {
                transform.GetChild(room.childId).gameObject.GetComponent<Image>().color = new Color(strtocol[room.tag].r * 0.5f, 
                    strtocol[room.tag].g * 0.5f, 
                    strtocol[room.tag].b * 0.5f );
            }
            if (RoomExists(id))
            {
                foreach (MinimapRoom room in minimapRooms)
                {
                    if(room.id == id)
                    {
                        transform.GetChild(room.childId).gameObject.GetComponent<Image>().color = strtocol[rooms[id].tag];
                    }
                }
            }
            else
            {
                minimapRooms.Add(new MinimapRoom(id, transform.childCount, rooms[id].tag));
                var a = Instantiate(Room, transform);
                a.transform.localPosition = generator.IdToPos(id) * 20 - Vector2.one * 40;
                a.GetComponent<Image>().color = strtocol[rooms[id].tag];

                for (int j = 0; j < rooms[id].doors.Count; j++)
                {
                    if (rooms[id].doors[j] == 1)
                    {
                        var b = Instantiate(door, a.transform);
                        if (j == 0)
                        {
                            b.transform.localPosition = new Vector3(0, 2.5f, -0.5f);
                            b.transform.Rotate(0f, 180f, 90f);
                        }
                        if (j == 1)
                        {
                            b.transform.localPosition = new Vector3(2.5f, 0, -0.5f);
                            b.transform.Rotate(0, 0, 0);
                        }
                        if (j == 2)
                        {
                            b.transform.localPosition = new Vector3(0, -2.5f, -0.5f);
                            b.transform.Rotate(0, 180, -90);
                        }
                        if (j == 3)
                        {
                            b.transform.localPosition = new Vector3(-2.5f, 0, -0.5f);
                            b.transform.Rotate(0, 0, 180);
                        }
                    }
                }
            }
        }
        void ShowMap()
        {
            for(int i = 0; i < rooms.Count; i++)
            {
                PointRoom(i);
            }
            PointRoom(roomId);
        }
        bool RoomExists(int id)
        {
            foreach(MinimapRoom room in minimapRooms)
            {
                if(room.id == id)
                {
                    return true;
                }
            }
            return false;
        }
        public void Shake()
        {
            StartCoroutine(_Shake());
        }
        IEnumerator _Shake()
        {
            float x;
            float y;
            float timeLeft = Time.time;
            while ((timeLeft + 0.2f) > Time.time)
            {
                x = Random.Range(-magnitude, magnitude) * timeLeft / Time.time;
                y = Random.Range(-magnitude, magnitude) * timeLeft / Time.time;
                dangerLevel.transform.localPosition = new Vector3(x + _originalPosition.x, y + _originalPosition.y, _originalPosition.z); yield return new WaitForSeconds(0.025f);
            }

            dangerLevel.transform.localPosition = _originalPosition;
        }
    }
}
