using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LT
{
    public class wave
    {
        public int id;
        public float time;
        public float spawnTime;
        public List<int> enemies;
        public wave(int _id, float _time, float _spawnTime, int[] _enemies)
        {
            id = _id;
            time = _time;
            spawnTime = _spawnTime;
            enemies = new List<int>();
            enemies.AddRange(_enemies);
        }
    }
    public class Wave : MonoBehaviour
    {

        public int count;
        public float timeLeft;
        public List<int> currentEnemies;
        public GameObject[] enemies;
        public float spawntime;

        private bool isWaveStarted = false;

        public List<wave> waves = new List<wave>
        {
            new wave(
                _id: 1,
                _time: 15f,
                _spawnTime: 5f,
                _enemies: new int[]{ 0 }
                ),
            new wave(
                _id: 2,
                _time: 15f,
                _spawnTime: 3f,
                _enemies: new int[]{ 0 }
                ),
            new wave(
                _id: 3,
                _time: 15f,
                _spawnTime: 2f,
                _enemies: new int[]{ 0 }
                ),
            new wave(
                _id: 4,
                _time: 15f,
                _spawnTime: 5f,
                _enemies: new int[]{ 0 , 1}
                ),
            new wave(
                _id: 5,
                _time: 15f,
                _spawnTime: 3f,
                _enemies: new int[]{ 0 , 1}
                ),
            new wave(
                _id: 6,
                _time: 15f,
                _spawnTime: 2f,
                _enemies: new int[]{ 0 , 1}
                ),
            new wave(
                _id: 7,
                _time: 15f,
                _spawnTime: 5f,
                _enemies: new int[]{ 0 , 1 , 2}
                ),
            new wave(
                _id: 8,
                _time: 15f,
                _spawnTime: 3f,
                _enemies: new int[]{ 0 , 1 , 2}
                ),
            new wave(
                _id: 9,
                _time: 15f,
                _spawnTime: 2f,
                _enemies: new int[]{ 0 , 1 , 2}
                ),
            new wave(
                _id: 10,
                _time: 30f,
                _spawnTime: 1f,
                _enemies: new int[]{ 0 , 1 , 2}
                ),
            new wave(
                _id: 11,
                _time: 60f,
                _spawnTime: 0.5f,
                _enemies: new int[]{ 0 , 1 , 2}
                ),
            new wave(
                _id: 12,
                _time: 60f,
                _spawnTime: 0.25f,
                _enemies: new int[]{ 0 , 1 , 2}
                ),
            new wave(
                _id: 13,
                _time: 120f,
                _spawnTime: 0.25f,
                _enemies: new int[]{ 0 , 1 , 2}
                )
        };

        // Update is called once per frame
        void Update()
        {
            if (!IsInvoking("Spawn") && isWaveStarted)
            {
                Invoke("Spawn", spawntime);
            }
            timeLeft = Mathf.Clamp(timeLeft - Time.deltaTime, 0f, Mathf.Infinity);
        }

        public void StartWave(int _count)
        {
            if(_count-1< waves.Count)
            {
                timeLeft = waves[_count-1].time;
                spawntime = waves[_count-1].spawnTime;
                currentEnemies = waves[_count-1].enemies;
            }
            else
            {
                timeLeft = waves[-1].time;
                spawntime = waves[-1].spawnTime;
                currentEnemies = waves[-1].enemies;
            }

            count = _count;
            isWaveStarted = true;
            Spawn();
            Invoke("EndWave", timeLeft);
        }

        void Spawn()
        {
            Vector3 spawnpoint = PointManager.Instance.GetPoint("EnemySpawn");
            spawnpoint.x = PointManager.Instance.GetPoint("LeftSide").x;
            var a = Instantiate(enemies[currentEnemies[Random.Range(0, currentEnemies.Count)]],
                new Vector3(Random.Range(spawnpoint.x, PointManager.Instance.GetPoint("RightSide").x), spawnpoint.y, spawnpoint.z), Quaternion.identity);
            a.transform.parent = GameObject.Find("EnemyList").transform;
        }

        void EndWave()
        {
            Destroy(gameObject);
        }
    }

}