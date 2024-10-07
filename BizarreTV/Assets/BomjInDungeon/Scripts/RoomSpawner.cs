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
                        var a = Instantiate(wall, transform);
                        a.GetComponent<SpriteRenderer>().sprite = tiles[0];
                        a.transform.position = new Vector2(x, y) * multiplyer;
                    }
                    else if (x == roomsize.x & y == roomsize.y)
                    {
                        var a = Instantiate(wall, transform);
                        a.GetComponent<SpriteRenderer>().sprite = tiles[1];
                        a.transform.position = new Vector2(x, y) * multiplyer;
                    }
                    else if (x == -roomsize.x & y == -roomsize.y)
                    {
                        var a = Instantiate(wall, transform);
                        a.GetComponent<SpriteRenderer>().sprite = tiles[2];
                        a.transform.position = new Vector2(x, y) * multiplyer;
                    }
                    else if (x == roomsize.x & y == -roomsize.y)
                    {
                        var a = Instantiate(wall, transform);
                        a.GetComponent<SpriteRenderer>().sprite = tiles[3];
                        a.transform.position = new Vector2(x, y) * multiplyer;
                    }
                    else if (y == roomsize.y)
                    {
                        var a = Instantiate(wall, transform);
                        a.GetComponent<SpriteRenderer>().sprite = tiles[4];
                        a.transform.position = new Vector2(x, y) * multiplyer;
                    }
                    else if (y == -roomsize.y)
                    {
                        var a = Instantiate(wall, transform);
                        a.GetComponent<SpriteRenderer>().sprite = tiles[5];
                        a.transform.position = new Vector2(x, y) * multiplyer;
                    }
                    else if (x == roomsize.x)
                    {
                        var a = Instantiate(wall, transform);
                        a.GetComponent<SpriteRenderer>().sprite = tiles[6];
                        a.transform.position = new Vector2(x, y) * multiplyer;
                    }
                    else if (x == -roomsize.x)
                    {
                        var a = Instantiate(wall, transform);
                        a.GetComponent<SpriteRenderer>().sprite = tiles[7];
                        a.transform.position = new Vector2(x, y) * multiplyer;
                    }


                    else if (y == roomsize.y - 1)
                    {
                        var a = Instantiate(wall, transform);
                        a.GetComponent<SpriteRenderer>().sprite = tiles[Random.Range(11, 16)];
                        a.transform.position = new Vector2(x, y) * multiplyer;
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
                        a.transform.position = new Vector2(x, y) * multiplyer;
                    }

                }
            }
        }
    }

}