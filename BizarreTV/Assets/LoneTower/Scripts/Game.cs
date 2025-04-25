using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LT
{
    public class Game : MonoBehaviour
    {
        public float spawntime;
        public GameObject[] enemies;
        public GameObject lighting;
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
        }

        void Spawn()
        {
            Vector3 spawnpoint = PointManager.Instance.GetPoint("EnemySpawn");
            spawnpoint.x = PointManager.Instance.GetPoint("LeftSide").x;
            var a = Instantiate(enemies[Random.Range(0, enemies.Length)],
                new Vector3(Random.Range(spawnpoint.x, PointManager.Instance.GetPoint("RightSide").x), spawnpoint.y, spawnpoint.z), Quaternion.identity);
            var b = Instantiate(lighting, new Vector3(-22f, 0f, 22f), Quaternion.identity);
        }
    }

}