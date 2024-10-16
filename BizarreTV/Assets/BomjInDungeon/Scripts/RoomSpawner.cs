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

        public Sprite[] tiles;

        public int multiplyer;

        public Vector2 roomsize;

        void Start()
        {
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

                    else
                    {
                        var a = Instantiate(wall, transform);
                        a.layer = 0;
                        var bc = a.GetComponent<BoxCollider2D>();
                        var rb = a.GetComponent<Rigidbody2D>();
                        Destroy(bc);
                        Destroy(rb);
                        a.GetComponent<SpriteRenderer>().sprite = tiles[Random.Range(8, 11)];
                        a.transform.position = new Vector3(x, y) * multiplyer + transform.position;
                    }

                }
            }
        }

        void CreateTile(float x, float y, Sprite sprite)
        {
            var a = Instantiate(wall, transform);
            a.GetComponent<SpriteRenderer>().sprite = sprite;
            a.transform.position = new Vector3(x, y) * multiplyer + transform.position;
        }
    }

}