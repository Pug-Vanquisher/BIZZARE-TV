using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Arcanoid
{
    public class Spawn : MonoBehaviour
    {
        [SerializeField] gameMAnager gameManager;
        
        public List<GameObject> list;

        // Start is called before the first frame update
        void Start()
        {
            gameManager = FindObjectOfType<gameMAnager>();
            for (int i = -8; i <= 9; i++)
            {
                for (int j = 0; j <= 5; j++)
                {
                    Instantiate(list[Random.Range(0, list.Count)], new Vector3(i - 0.5f, j - 0.5f, 0f), Quaternion.identity, gameObject.transform);
                    gameManager.numberOfBlocks += 1;
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
