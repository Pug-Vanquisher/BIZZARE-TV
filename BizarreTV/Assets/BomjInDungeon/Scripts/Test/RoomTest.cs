using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BID
{
    public class RoomTest : MonoBehaviour
    {
        public DungeonGenerator generator;
        public GameObject Room;
        public GameObject door;

        public List<Room> rooms = new List<Room>();
        Dictionary<string, Color> strtocol = new Dictionary<string, Color>()
        {
            {"none", Color.gray },
            {"default", Color.white },
            {"playerRoom", Color.green },
            {"keyRoom", Color.yellow },
            {"bossRoom", Color.magenta }
        };

        private void Awake()
        {
            EventManager.Instance.Subscribe("GenEnd", MiniMap);
        }
        void MiniMap()
        {
            rooms = generator.rooms;

            ShowMap();
        }
        void ShowMap()
        {
            for(int i = 0; i < rooms.Count; i++)
            {
                var a = Instantiate(Room, transform);
                a.transform.localPosition = generator.IdToPos(i);
                a.GetComponent<SpriteRenderer>().color = strtocol[rooms[i].tag];
                a.GetComponent<RoomShow>().dangerous = rooms[i].danger;
                for (int j = 0; j < rooms[i].doors.Count; j++)
                {
                    if (rooms[i].doors[j] == 1)
                    {
                        if (j == 0)
                        {
                            var b = Instantiate(door, a.transform);
                            b.transform.localPosition = new Vector3(0, 0.05f, -0.5f);
                        }
                        if (j == 1)
                        {
                            var b = Instantiate(door, a.transform);
                            b.transform.localPosition = new Vector3(0.05f, 0, -0.5f);
                        }
                        if (j == 2)
                        {
                            var b = Instantiate(door, a.transform);
                            b.transform.localPosition = new Vector3(0, -0.05f, -0.5f);
                        }
                        if (j == 3)
                        {
                            var b = Instantiate(door, a.transform);
                            b.transform.localPosition = new Vector3(-0.05f, 0, -0.5f);
                        }
                    }
                }
            }
        }

        
    }
}
