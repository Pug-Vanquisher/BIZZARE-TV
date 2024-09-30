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
    }

    void Update()
    {
        if (Time.time >= startime + lifetime) { Destroy(gameObject); }
    }
}
