using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BID
{
    public class DungeonGenerator : MonoBehaviour
    {

        public List<Room> rooms = new List<Room>();
        public Vector2 DefaultRoomSize;
        public Vector2 RandomErr;

        public Vector2 dungeonSize;

        public int GetPlayerStartRoom()
        {
            for(int i = 0; i < rooms.Count; i++)
            {
                if(rooms[i].tag == "playerRoom")
                {
                    return i;
                }
            }
            return 0;
        }
        public List<Room> GenerateMap()
        {
            rooms.Clear();
            int count = (int)(dungeonSize.x) * (int)(dungeonSize.y);
            for (int i = 0; i < count; i++)
            {
                rooms.Add(new Room());
                rooms[i].id = i;
            }

            List<int> mainIds = GenerateUniq(3, 0, rooms.Count);
            GenerateMainRooms(mainIds);
            MakeRoad(Random.Range(0, rooms.Count), Random.Range(0, rooms.Count));
            while (RoomExists("none"))
            {
                SpawnNearRooms();
            }

            DoorsNormalize();

            while (RoomExists(0))
            {
                Dangerize();
            }

            EventManager.Instance.TriggerEvent("GenEnd");
            return rooms;
        }
        void GenerateMainRooms(List<int> ids)
        {
            rooms[ids[0]].tag = "playerRoom";
            rooms[ids[0]].danger = 1;

            rooms[ids[1]].tag = "keyRoom";
            rooms[ids[1]].size = new Vector2(6, 6);

            rooms[ids[2]].tag = "bossRoom";
            rooms[ids[2]].size = new Vector2(6, 6);

            MakeRoad(ids[0], ids[1]);
            MakeRoad(ids[1], ids[2]);
            MakeRoad(ids[0], ids[2]);
        }
        void SpawnNearRooms()
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].tag != "none")
                {
                    Vector2 position = IdToPos(i);
                    List<int> freeDim = new List<int>() { 0, 0, 0, 0 };
                    if (rooms[(int)Mathf.Clamp(i + 1, position.y * (int)dungeonSize.x, (position.y + 1) * (int)dungeonSize.x - 1)].tag == "none")
                    {
                        freeDim[1] = Random.Range(0, 2);
                    }
                    if (rooms[(int)Mathf.Clamp(i - 1, position.y * (int)dungeonSize.x, (position.y + 1) * (int)dungeonSize.x - 1)].tag == "none")
                    {
                        freeDim[3] = Random.Range(0, 2);
                    }

                    if (rooms[(int)Mathf.Clamp(i + (int)dungeonSize.x, position.x, ((int)dungeonSize.y - 1) * (int)dungeonSize.x + position.x)].tag == "none")
                    {
                        freeDim[0] = Random.Range(0, 2);
                    }
                    if (rooms[(int)Mathf.Clamp(i - (int)dungeonSize.x, position.x, ((int)dungeonSize.y - 1) * (int)dungeonSize.x + position.x)].tag == "none")
                    {
                        freeDim[2] = Random.Range(0, 2);
                    }

                    //freeDim = ArrayRandomize(freeDim);
                    for (int j = 0; j < freeDim.Count; j++)
                    {
                        if (freeDim[j] == 1)
                        {
                            MakeRoomToward(i, j);
                        }
                    }
                }
            }
        }
        void DoorsNormalize()
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].tag != "none")
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (rooms[i].doors[j] == 1)
                        {
                            if (j == 0)
                            {
                                rooms[i + (int)dungeonSize.x].doors[2] = 1;
                            }
                            if (j == 1)
                            {
                                rooms[i + 1].doors[3] = 1;
                            }
                            if (j == 2)
                            {
                                rooms[i - (int)dungeonSize.x].doors[0] = 1;
                            }
                            if (j == 3)
                            {

                                rooms[i - 1].doors[1] = 1;
                            }
                        }
                    }
                }

            }
        }
        void Dangerize()
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].danger != 0)
                {
                    Vector2 position = IdToPos(i);
                    if (rooms[(int)Mathf.Clamp(i + 1, position.y * (int)dungeonSize.x, (position.y + 1) * (int)dungeonSize.x - 1)].danger == 0)
                    {
                        rooms[i + 1].danger = rooms[i].danger + 1;
                    }
                    if (rooms[(int)Mathf.Clamp(i - 1, position.y * (int)dungeonSize.x, (position.y + 1) * (int)dungeonSize.x - 1)].danger == 0)
                    {
                        rooms[i - 1].danger = rooms[i].danger + 1;
                    }

                    if (rooms[(int)Mathf.Clamp(i + (int)dungeonSize.x, position.x, ((int)dungeonSize.y - 1) * (int)dungeonSize.x + position.x)].danger == 0)
                    {
                        rooms[i + (int)dungeonSize.x].danger = rooms[i].danger + 1;
                    }
                    if (rooms[(int)Mathf.Clamp(i - (int)dungeonSize.x, position.x, ((int)dungeonSize.y - 1) * (int)dungeonSize.x + position.x)].danger == 0)
                    {
                        rooms[i - (int)dungeonSize.x].danger = rooms[i].danger + 1;
                    }
                }
            }
        }
        void MakeRoad(int idFrom, int idTo)
        {
            int currentId = idFrom;
            while (currentId != idTo)
            {
                if (rooms[currentId].tag == "none")
                {
                    rooms[currentId].tag = "default";
                    rooms[currentId].size = new Vector2((int)DefaultRoomSize.x + Random.Range((int)-RandomErr.x / 2, (int)RandomErr.x/2), (int)DefaultRoomSize.y + Random.Range((int)-RandomErr.x / 2, (int)RandomErr.x / 2));
                }
                Vector2 vector = IdToPos(idTo) - IdToPos(currentId);
                if (Mathf.Abs(vector.x) > Mathf.Abs(vector.y))
                {
                    if (vector.x > 0)
                    {
                        rooms[currentId].doors[1] = 1;
                        currentId += 1;
                    }
                    else
                    {
                        rooms[currentId].doors[3] = 1;
                        currentId -= 1;
                    }
                }
                else
                {
                    if (vector.y > 0)
                    {
                        rooms[currentId].doors[0] = 1;
                        currentId += (int)dungeonSize.x;
                    }
                    else
                    {
                        rooms[currentId].doors[2] = 1;
                        currentId -= (int)dungeonSize.x;
                    }
                }
            }
        }
        public List<Room> GenerateObjects(List<Room> rooms)
        {
            for(int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].tag == "default")
                {
                    float monsterScale = rooms[i].size.x * rooms[i].size.y;
                    int monsterCount = Random.Range((int)(monsterScale * 0.2f), (int)(monsterScale * 0.25f));

                    List<Vector2> positions = GenerateUniq(monsterCount,
                        new Vector2(-rooms[i].size.x + 1, -rooms[i].size.y + 1),
                        new Vector2(rooms[i].size.x - 1, rooms[i].size.y - 2));

                    for (int j = 0; j < monsterCount; j++)
                    {
                        int MonsterName = Random.Range(0, PrefabStorage.enemiesPrefabs.Count);
                        if (rooms[i].EnemiesPositions.ContainsKey(MonsterName))
                        {
                            rooms[i].EnemiesPositions[MonsterName].Add(positions[j]);
                        }
                        else
                        {
                            rooms[i].EnemiesPositions.Add(MonsterName, new List<Vector2>() { positions[j] });
                        }
                    }
                }
            }
            return rooms;
        }


        public bool RoomExists(string tag)
        {
            bool flag = false;
            foreach (Room room in rooms)
            {
                if (room.tag == tag)
                {
                    flag = true;
                }
            }
            return flag;
        }
        bool RoomExists(int danger)
        {
            bool flag = false;
            foreach (Room room in rooms)
            {
                if (room.danger == 0)
                {
                    flag = true;
                }
            }
            return flag;
        }

        public void MakeRoomToward(int id, int dirId)
        {
            if (dirId == 1)
            {
                rooms[id + 1].tag = "default";
                rooms[id + 1].size = new Vector2((int)DefaultRoomSize.x + Random.Range((int)-RandomErr.x / 2, (int)RandomErr.x / 2), (int)DefaultRoomSize.y + Random.Range((int)-RandomErr.x / 2, (int)RandomErr.x / 2));
                rooms[id + 1].doors[3] = 1;
            }
            if (dirId == 3)
            {
                rooms[id - 1].tag = "default";
                rooms[id - 1].size = new Vector2((int)DefaultRoomSize.x + Random.Range((int)-RandomErr.x / 2, (int)RandomErr.x / 2), (int)DefaultRoomSize.y + Random.Range((int)-RandomErr.x / 2, (int)RandomErr.x / 2));
                rooms[id - 1].doors[1] = 1;
            }

            if (dirId == 0)
            {
                rooms[id + (int)dungeonSize.x].tag = "default";
                rooms[id + (int)dungeonSize.x].size = new Vector2((int)DefaultRoomSize.x + Random.Range((int)-RandomErr.x / 2, (int)RandomErr.x / 2), (int)DefaultRoomSize.y + Random.Range((int)-RandomErr.x / 2, (int)RandomErr.x / 2));
                rooms[id + (int)dungeonSize.x].doors[2] = 1;
            }
            if (dirId == 2)
            {
                rooms[id - (int)dungeonSize.x].tag = "default";
                rooms[id - (int)dungeonSize.x].size = new Vector2((int)DefaultRoomSize.x + Random.Range((int)-RandomErr.x / 2, (int)RandomErr.x / 2), (int)DefaultRoomSize.y + Random.Range((int)-RandomErr.x / 2, (int)RandomErr.x / 2));
                rooms[id - (int)dungeonSize.x].doors[0] = 1;
            }
        }

        public List<int> GenerateUniq(int count, int min, int max)
        {
            List<int> values = new List<int>();
            for (int i = 0; i < count; i++)
            {
                bool flag = true;
                while (flag)
                {
                    int value = Random.Range(min, max);
                    if (!values.Contains(value))
                    {
                        flag = false;
                        values.Add(value);
                    }
                }
            }
            return values;
        }
        public List<Vector2> GenerateUniq(int count, Vector2 min, Vector2 max)
        {
            List<Vector2> values = new List<Vector2>();
            for (int i = 0; i < count; i++)
            {
                bool flag = true;
                while (flag)
                {
                    Vector2 value = new Vector2(Random.Range((int)min.x, (int)max.x), Random.Range((int)min.y, (int)max.y));
                    if (!values.Contains(value))
                    {
                        flag = false;
                        values.Add(value);
                    }
                }
            }
            return values;
        }
        public Vector2 IdToPos(int id)
        {
            return new Vector2(id % dungeonSize.x, id / (int)dungeonSize.x);
        }
    }
}
