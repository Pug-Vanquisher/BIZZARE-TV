using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BID
{
    public class RoomSpawner : MonoBehaviour
    {
        public GameObject player;

        public GameObject wall;
        public GameObject door;
        public GameObject NewRoomTrigger;
        public GameObject bossPrefab;
        public GameObject keyStone;
        public GameObject doorBoss;
        public Sprite[] tiles;
        public Sprite[] doors;
        public GameObject CorpsePrefab;

        public int multiplyer;

        public int roomID;
        public Vector2 roomsize;


        Dictionary<string, string> dirToEvent = new Dictionary<string, string>()
        {
            {"10", "MoveRight" },
            {"-10", "MoveLeft" },
            {"01", "MoveUp" },
            {"0-1", "MoveDown" }
        };

        public void ClearRoom()
        {
            foreach (Transform child in transform)
            {
                if(child.gameObject.name != "Decor" && child.gameObject.name != "Doors" && child.gameObject.name != "Enemies")
                {
                    Destroy(child.gameObject);
                }
                else
                {
                    foreach(Transform lilbro in child)
                    {
                        Destroy(lilbro.gameObject);
                    }
                }
            }

        }
        public void PlaceRoom(Room room, DungeonLogic placer = null)
        {
            roomsize = room.size;
            roomID = room.id;
            List<int> doorIds = room.doors;

            List<Vector2> mainPoints = new List<Vector2>();

            if (doorIds[0] == 1)
            {
                CreateDoor(0, roomsize.y - 1, new Vector2(0, roomsize.y - 1).normalized, transform.GetChild(1));
                mainPoints.Add(new Vector2(0, roomsize.y - 1));
            }
            if (doorIds[1] == 1)
            {
                CreateDoor(roomsize.x, 0, new Vector2(roomsize.x, 0).normalized, transform.GetChild(1));
                mainPoints.Add(new Vector2(roomsize.x, 0));
            }
            if (doorIds[2] == 1)
            {
                CreateDoor(0, -roomsize.y, new Vector2(0, -roomsize.y).normalized, transform.GetChild(1));
                mainPoints.Add(new Vector2(0, -roomsize.y));
            }
            if (doorIds[3] == 1)
            {
                CreateDoor(-roomsize.x, 0, new Vector2(-roomsize.x, 0).normalized, transform.GetChild(1));
                mainPoints.Add(new Vector2(-roomsize.x, 0));
            }
            GenerateEmptyRoom(roomsize, mainPoints);
            GenerateEnemies(room.EnemiesPositions, placer);
            if (room.tag == "keyRoom")
            {
                GenerateKey(Vector2.zero);
            }
            if (room.tag == "bossRoom")
            {
                GenerateBossEnt(Vector2.zero);
            }
        }
        void ToString(Dictionary<int, List<Vector2>> dict)
        {
            for (int i = 0; i < dict.Count; i++)
            {
                string str = i.ToString();
                if (dict.ContainsKey(i))
                {
                    for (int j = 0; j < dict[i].Count; j++)
                    {
                        str += "(" + dict[i][j].x + ", " + dict[i][j].y + ")";
                    }
                }
                Debug.Log(str);
            }
        }
        void GenerateEmptyRoom(Vector2 roomsize, List<Vector2> mainPoints)
        {
            for (float y = -roomsize.y; y <= roomsize.y; y++)
            {
                for (float x = -roomsize.x; x <= roomsize.x; x++)
                {
                    if(mainPoints.Contains(new Vector2(x, y)) || mainPoints.Contains(new Vector2(x, y) - new Vector2(x, y).normalized))
                    {
                        if(y != roomsize.y)
                        {
                            CreateTile(x, y, tiles[Random.Range(8, 11)], true);
                        }
                    }
                    else if (x == -roomsize.x & y == roomsize.y)
                    {
                        CreateTile(x, y, tiles[0]);
                    }
                    else if (x == roomsize.x & y == roomsize.y)
                    {
                        CreateTile(x, y, tiles[1]);
                    }
                    else if (x == -roomsize.x & y == -roomsize.y)
                    {
                        CreateTile(x, y, tiles[2]);
                    }
                    else if (x == roomsize.x & y == -roomsize.y)
                    {
                        CreateTile(x, y, tiles[3]);
                    }
                    else if (y == roomsize.y)
                    {
                        CreateTile(x, y, tiles[4]);
                    }
                    else if (y == -roomsize.y)
                    {
                        CreateTile(x, y, tiles[5]);
                    }
                    else if (x == roomsize.x)
                    {
                        CreateTile(x, y, tiles[6]);
                    }
                    else if (x == -roomsize.x)
                    {
                        CreateTile(x, y, tiles[7]);
                    }
                    else if (y == roomsize.y - 1)
                    {
                        CreateTile(x, y, tiles[Random.Range(11, 16)]);
                    }
                    else
                    {
                        CreateTile(x, y, tiles[Random.Range(8, 11)], true);
                    }
                }
            }
        }
        void GenerateEnemies(Dictionary<int, List<Vector2>> enemies, DungeonLogic logic = null)
        {
            for(int i = 0; i < enemies.Count; i++)
            {
                if (enemies.ContainsKey(i))
                {
                    for(int j =  0; j < enemies[i].Count; j++)
                    {
                        var a = Instantiate(PrefabStorage.enemiesPrefabs[i], transform.GetChild(2));
                        a.transform.position = new Vector3(enemies[i][j].x, enemies[i][j].y) * multiplyer + transform.position;
                        a.AddComponent<DeathEvent>().CollectEvent(logic, roomID, i, enemies[i][j]);
                        a.GetComponent<DeathEvent>().Corpse = CorpsePrefab;
                    }
                }
            }

        }
        void GenerateKey(Vector2 position)
        {
            var a = Instantiate(keyStone, transform.GetChild(0));
            a.transform.position = transform.position + new Vector3(position.x, position.y, 0);
        }
        void GenerateBossEnt(Vector2 position)
        {
            CreateTile(-1, 2, tiles[16]);
            CreateTile(0, 2, tiles[5]);
            CreateTile(1, 2, tiles[17]);
            CreateTile(-1, 1, tiles[6]);
            CreateTile(1, 1, tiles[7]);
            CreateTile(-1, 0, tiles[18]);
            CreateTile(1, 0, tiles[19]);
            CreateTile(-1, -1, tiles[20]);
            CreateBossDoor(0, -1, transform.GetChild(0));
            CreateTile(1, -1, tiles[20]);
        }
        void CreateTile(float x, float y, Sprite sprite, bool noCollider = false)
        {
            var a = Instantiate(wall, transform);
            a.GetComponent<SpriteRenderer>().sprite = sprite;
            a.GetComponent<SpriteRenderer>().sortingOrder = -32750;
            a.transform.position = new Vector3(x, y) * multiplyer + transform.position;
            if (noCollider)
            {
                a.layer = 0;
                var bc = a.GetComponent<BoxCollider>();
                Destroy(bc);
            }
            else
            {
                a.GetComponent<SpriteRenderer>().sortingOrder = -(int)(y * multiplyer*10 + transform.position.y * 10);
            }
        }
        void CreateDoor(float x, float y, Vector2 direction, Transform parent)
        {
            var a = Instantiate(door, parent);
            a.transform.position = new Vector3(x, y) * multiplyer + transform.position;
            a.GetComponent<SpriteRenderer>().sprite = doors[(int)(1 - Mathf.Abs(direction.y)) * (int)(1.5f - (direction.x / 2f))];
            a.GetComponent<SpriteRenderer>().sortingOrder = (int)((y * multiplyer + transform.position.y)   * -10 + 10);

            var b = Instantiate(NewRoomTrigger, transform);
            if(Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
            {
                b.GetComponent<SpriteRenderer>().sortingOrder = (int)((roomsize.x * direction.x + transform.position.y + 9) * multiplyer * -10);
            }
            else
            {
                b.GetComponent<SpriteRenderer>().sortingOrder = (int)((roomsize.y * direction.y + transform.position.y + 9) * multiplyer * -10);
            }
            b.transform.position = new Vector3(x, y) * multiplyer + transform.position;
            b.transform.right = -direction;
            b.GetComponent<NewRoomTriggerScript>().type = dirToEvent[direction.x.ToString() + direction.y.ToString()];
        }
        void CreateBossDoor(float x, float y, Transform parent)
        {
            var a = Instantiate(doorBoss, parent);
            a.transform.position = new Vector3(x, y) * multiplyer + transform.position;
            a.GetComponent<BossDoorScript>().orderZ = (int)((y * multiplyer + transform.position.y) * -10 + 10);

            var b = Instantiate(NewRoomTrigger, transform);
            b.GetComponent<SpriteRenderer>().sortingOrder = (int)((y + transform.position.y + 9) * multiplyer * -10);
            b.transform.position = new Vector3(x, y) * multiplyer + transform.position;
            b.transform.right = Vector2.down;
            b.GetComponent<NewRoomTriggerScript>().type = "boss";
        }
    }

}