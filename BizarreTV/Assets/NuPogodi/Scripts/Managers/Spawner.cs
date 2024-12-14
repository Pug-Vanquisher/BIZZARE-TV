using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

namespace Pogodi
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] protected GameManager gm;
        [SerializeField] float width;
        [SerializeField] float spawnRate;
        [SerializeField] private ObjectList objects;
        [SerializeField] Transform spawnerTran;
        [SerializeField] bool canSpawn = true;
        void Awake()
        {
            gm.OnGameEnded += OnGameEnded;
            gm.OnGameRestarted += OnGameRestarted;
            spawnerTran = GetComponent<Transform>();
            width = spawnerTran.lossyScale.x;
        }

        void Start()
        {
            StartCoroutine(Spawn());
        }

        void OnGameEnded()
        {
            this.canSpawn = false;
        }

        void OnGameRestarted()
        {
            this.canSpawn = true;
        }

        private void SpawnAnObject()
        {
            Random random = new Random();
            int objectType = random.Next(0, objects.getLength());
            float PosX = (float)((random.NextDouble()) * width - width / 2);
            Instantiate(objects.getObject(objectType), new Vector3(PosX, spawnerTran.position.y, spawnerTran.position.z), Quaternion.identity);
        }

        private IEnumerator Spawn()
        {
            WaitForSeconds wait = new WaitForSeconds((1 / spawnRate));
            for (; ; )
            {
                if (canSpawn) SpawnAnObject();
                spawnRate += 0.025f;
                yield return wait;
            }
        }
    }
}