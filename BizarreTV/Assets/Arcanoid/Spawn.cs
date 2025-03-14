using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawn : MonoBehaviour
{
    public List<GameObject> list;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = -11; i <= 11; i++)
        {
            for (int j = 0; j <= 6; j++)
            {
                Instantiate(list[Random.Range(0, list.Count)], new Vector3(i * 0.8f, j * 0.8f, 0f), Quaternion.identity, gameObject.transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
