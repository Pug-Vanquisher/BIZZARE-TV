using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LT
{
    public class Upgrade
    {
        public int id;
        public float value;
        public string type;
        public float add;
        public int cost;
        public float min;
        public float max;

        public Upgrade(int _id, float _value, string _type, float _add, int _cost, float _min, float _max)
        {
            id = _id;
            value = _value;
            type = _type;
            add = _add;
            cost = _cost;
            min = _min;
            max = _max;
        }

        public bool levelUp()
        {
            if (value + add <= max) {
                value += add;
                return true;
            }
            return false;
        }
        public bool levelDown()
        {
            if (value - add >= min)
            {
                value -= add;
                return true;
            }
            return false;
        }

    }

    public class Game : MonoBehaviour
    {
        public Dictionary<string, Upgrade> upgrades = new Dictionary<string, Upgrade>()
        {
            { "archers_count", new Upgrade(
                _id: 0,
                _value: 1,
                _type: "archers count",
                _add: 1f,
                _cost: 100,
                _min: 1f,
                _max: 10f
                ) },
            { "archers_speed", new Upgrade(
                _id: 1,
                _value: 0.2f,
                _type: "archers' speed",
                _add: 0.02f,
                _cost: 25,
                _min: 1.2f,
                _max: 2.4f
                ) },
            { "arrow_speed", new Upgrade(
                _id: 2,
                _value: 1.5f,
                _type: "arrows' speed",
                _add: 0.85f,
                _cost: 30,
                _min: 1.5f,
                _max: 10f
                ) },
            { "ricoshet_count", new Upgrade(
                _id: 3,
                _value: 0f,
                _type: "ricoshets count",
                _add: 1f,
                _cost: 100,
                _min: 0f,
                _max: 3f
                ) },
            {  "lighting_chance", new Upgrade(
                _id: 4,
                _value: 100,
                _type: "lighting chance",
                _add: 5f,
                _cost: 40,
                _min: 0f,
                _max: 100f
                ) },
            {  "slowdown_time", new Upgrade(
                _id: 5,
                _value: 0,
                _type: "slowdown time",
                _add: 0.1f,
                _cost: 40,
                _min: 0f,
                _max: 3f
                ) }
        };

        public float points;
        public float score;

        public float spawntime;
        public GameObject[] enemies;
        public List<GameObject> archers;

        public GameObject ArcherPrefab;

        void Start()
        {
            transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }

        // Update is called once per frame
        void Update()
        {
            
            if (!IsInvoking("Spawn"))
            {
                Invoke("Spawn", spawntime);
            }
            Recruit();
        }

        void Recruit()
        {
            if(archers.Count < (int)upgrades["archers_count"].value)
            {
                for(int i = 0; i < (int)upgrades["archers_count"].value - archers.Count; i++)
                {
                    var a = Instantiate(ArcherPrefab, new Vector3(
                            Random.Range(PointManager.Instance.GetPoint("RightSide").x, PointManager.Instance.GetPoint("LeftSide").x),
                            PointManager.Instance.GetPoint("Archer").y,
                            PointManager.Instance.GetPoint("Archer").z), 
                        Quaternion.identity);
                    archers.Add(a);
                }
            }

            if(archers.Count > (int)upgrades["archers_count"].value)
            {
                for(int i = 0; i < archers.Count - (int)upgrades["archers_count"].value; i++)
                {
                    archers[-i].GetComponent<Archer>().Death();
                    archers.RemoveAt(-i);
                }
            }
        }

        void Spawn()
        {
            Vector3 spawnpoint = PointManager.Instance.GetPoint("EnemySpawn");
            spawnpoint.x = PointManager.Instance.GetPoint("LeftSide").x;
            var a = Instantiate(enemies[Random.Range(0, enemies.Length)],
                new Vector3(Random.Range(spawnpoint.x, PointManager.Instance.GetPoint("RightSide").x), spawnpoint.y, spawnpoint.z), Quaternion.identity);
            a.transform.parent = GameObject.Find("EnemyList").transform;
            //var b = Instantiate(lighting, new Vector3(-22f, 0f, 22f), Quaternion.identity);
        }

        public void LevelUp(string name)
        {
            if (upgrades[name] != null)
            {
                if (upgrades[name].cost <= points)
                {
                    if (upgrades[name].levelUp())
                    {
                        points -= upgrades[name].cost;
                        return;
                    }
                    throw new System.Exception("max");
                }
                throw new System.Exception("points");
            }
        }
        public void LevelDown(string name)
        {
            if (upgrades[name] != null)
            {
                if (upgrades[name].levelDown())
                {
                    points += upgrades[name].cost;
                    return;
                }
            }
            throw new System.Exception("min");
        }
    }

}