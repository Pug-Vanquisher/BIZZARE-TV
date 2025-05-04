using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace LT
{
    public class Upgrade
    {
        public float value { 
            get{
                return (max - min) / maxLevel * level; 
            } 
        }

        public int id;
        public string type;
        public int cost;
        public int level;
        public float min;
        public float max;
        public int maxLevel = 10;
        public bool showLikeint;
        public Upgrade(int _id, string _type, int _level, int _cost, float _min, float _max, int _maxLevel = 10, bool _showLikeint = false)
        {
            id = _id;
            type = _type;
            level = _level;
            cost = _cost;
            min = _min;
            max = _max;
            maxLevel = _maxLevel;
            showLikeint = _showLikeint;
        }

        public bool levelUp()
        {
            if (level + 1 <= maxLevel) {
                level += 1;
                return true;
            }
            return false;
        }
        public bool levelDown()
        {
            if (level - 1 >= 1)
            {
                level -= 1;
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
                _type: "archers count",
                _level: 1,
                _cost: 100,
                _min: 0,
                _max: 10f,
                _maxLevel: 11,
                _showLikeint: true) },
            { "archers_speed", new Upgrade(
                _id: 1,
                _type: "archers' speed",
                _level: 1,
                _cost: 25,
                _min: 0.2f,
                _max: 2.4f
                ) },
            { "arrow_speed", new Upgrade(
                _id: 2,
                _type: "arrows' speed",
                _level: 1,
                _cost: 30,
                _min: 1.5f,
                _max: 10f
                ) },
            { "ricoshet_count", new Upgrade(
                _id: 3,
                _type: "ricoshets count",
                _level: 1,
                _cost: 100,
                _min: 0f,
                _max: 3f,
                _maxLevel: 4,
                _showLikeint: true) },
            {  "lighting_chance", new Upgrade(
                _id: 4,
                _type: "lighting chance",
                _level: 1,
                _cost: 40,
                _min: 0f,
                _max: 100f,
                _showLikeint: true) },
            {  "slowdown_time", new Upgrade(
                _id: 5,
                _type: "slowdown time",
                _level: 1,
                _cost: 40,
                _min: 0f,
                _max: 3f
                ) }
        };

        public float points;
        public float score;
        public int wave;

        public int maxHp;
        public int hp;

        public List<GameObject> archers;
        public GameObject wavePrefab;

        public GameObject ArcherPrefab;
        void Start()
        {
            hp = maxHp;
            transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.W) && gameObject.GetComponent<Wave>() == null)
            {
                StartWave();
            }
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

        public void StartWave()
        {
            wave += 1;
            GameObject a = Instantiate(wavePrefab, transform);
            a.gameObject.GetComponent<Wave>().StartWave(wave);
        }

        public void LevelUp(string name)
        {
            
            if (upgrades[name] != null)
            {
                if (upgrades[name].cost <= points)
                {
                    if (upgrades[name].levelUp())
                    {
                        if (name == "archers_count")
                        {
                            Recruit();
                        }
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
                    if (name == "archers_count")
                    {
                        Recruit();
                    }
                    points += upgrades[name].cost;
                    return;
                }
            }
            throw new System.Exception("min");
        }
        public void WallDamage(float damage)
        {
            if(hp - damage <= 0)
            {
                hp = 0;
                Loose();
            }
            else
            {
                hp -= Mathf.RoundToInt(damage);
            }
        }
        public void Loose()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
    }

}