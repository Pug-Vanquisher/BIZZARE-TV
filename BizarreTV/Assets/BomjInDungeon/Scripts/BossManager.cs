using BID;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    private static GameObject ancor;
    private GameObject player;

    public GameObject boss;
    public GameObject clone;

    private int random;

    // Start is called before the first frame update
    void Start()
    {
        random = Random.Range(1, 5);
        Debug.Log(random);

        player = GameObject.FindGameObjectWithTag("Player");

        for (int i = 1; i < 5; i++)
        {
            if (i != random)
            {
                var a = Instantiate(clone, transform);
                a.GetComponent<Goblin>().player = player;
                a.transform.position = transform.position + new Vector3(i * 7, 0, 0);
            }

            else
            {
                var a = Instantiate(boss, transform);
                a.GetComponent<Goblin>().player = player;
                a.transform.position = transform.position + new Vector3(i * 7, 0, 0);
            }

        }

        ancor = gameObject;
    }

    public static void ChangePhase()
    {
        Debug.Log("balls");
        ancor.transform.position += new Vector3(0, 5, 0);

    }

    public static void EndFight()
    {
        Destroy(ancor);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
