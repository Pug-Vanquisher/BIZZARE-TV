using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : MonoBehaviour
{
    public float lifetime = 1f;
    private float startime;
    public int speed;
    void Start()
    {
        startime = Time.time;
        //print(startime);
    }

    // Update is called once per frame
    void Update()
    {
        //gameObject.transform.position += transform.right * Time.deltaTime * speed;
        if (Time.time >= startime + lifetime) { Destroy(gameObject); }
    }
}
