using System.Collections;
using System.Collections.Generic;
using UnityEditor.Localization.Plugins.XLIFF.V12;
using UnityEngine;

namespace BID
{
    public class RoomSpawner : MonoBehaviour
    {
        public GameObject wall;
        public GameObject door;
        public GameObject vaseDecor;
        public GameObject boneDecor;

        public Sprite[] tiles;
        public Sprite[] decorations;

        public int multiplyer;

        public Vector2 DefaultRoomSize;
        public Vector2 RandomErr;
        public Vector2 roomsize;

        void Start()
        {
            GenerateRoom();
        }
        void GenerateRoom()
        {
            roomsize = new Vector2(Random.Range((int)(DefaultRoomSize.x - RandomErr.x), (int)(DefaultRoomSize.x + RandomErr.x)),
                Random.Range((int)(DefaultRoomSize.y - RandomErr.y), (int)(DefaultRoomSize.y + RandomErr.y)));

            float frontadd = -transform.position.z + Camera.main.transform.position.z + 1;
            List<Vector2> mainPoints = new List<Vector2>();
            mainPoints.Add(Vector2.zero);
            mainPoints.Add(new Vector2(0, -roomsize.y));
            mainPoints.Add(new Vector2(0, roomsize.y-1));
            mainPoints.Add(new Vector2(roomsize.x, 0));
            mainPoints.Add(new Vector2(-roomsize.x, 0));
            for (float y = -roomsize.y; y <= roomsize.y; y++)
            {
                for (float x = -roomsize.x; x <= roomsize.x; x++)
                {
                    if (x == -roomsize.x & y == roomsize.y)
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
                    else if(mainPoints.Contains(new Vector2(x,y)) && new Vector2(x,y) != mainPoints[0])
                    {
                        CreateDoor(x, y, new Vector2(x,y));
                    }

                    else
                    {
                        CreateTile(x, y, tiles[Random.Range(8, 11)], true);
                        int bababa = Random.Range(0, 6);
                        bool flag = true;
                        foreach (Vector2 point in mainPoints)
                        {
                            if ((point - new Vector2(x, y)).magnitude < 3.5)
                            {
                                flag = false;
                            }
                        }
                        if (bababa >= 4 && flag)
                        {
                            if (bababa == 4)
                            {
                                CreateDecoration(x, y, vaseDecor, decorations[Random.Range(0, 2)]);
                            }
                            else
                            {
                                CreateDecoration(x, y, boneDecor, decorations[Random.Range(2, 4)]);
                            }
                        }
                    }

                }
            }

        }
        void CreateTile(float x, float y, Sprite sprite, bool noCollider = false)
        {
            var a = Instantiate(wall, transform);
            a.GetComponent<SpriteRenderer>().sprite = sprite;
            a.GetComponent<SpriteRenderer>().sortingOrder = (int)(roomsize.y * multiplyer * -10);
            a.transform.position = new Vector3(x, y) * multiplyer;
            if (noCollider)
            {
                a.layer = 0;
                var bc = a.GetComponent<BoxCollider>();
                Destroy(bc);
            }
        }
        void CreateDecoration(float x, float y, GameObject prefab, Sprite sprite)
        {
            var a = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            a.transform.SetParent(transform);
            a.transform.position = new Vector3(x, y) * multiplyer;
            a.GetComponent<SpriteRenderer>().sprite = sprite;
            a.GetComponent<SpriteRenderer>().sortingOrder = (int)(y * multiplyer * -10);
        }
        void CreateDoor(float x, float y, Vector2 direction)
        {
            var a = Instantiate(door, transform);
            a.transform.position = new Vector3(x, y) * multiplyer;
            a.GetComponent<SpriteRenderer>().sortingOrder = (int)(y * multiplyer  * -10);

        }
    }

}