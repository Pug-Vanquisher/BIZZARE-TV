using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gener : MonoBehaviour
{
    public GameObject WatchSmall;
    public GameObject WatchBig;

    public Transform Spawn1;
    public Transform Spawn2;
    public Transform Spawn3;
    public Transform Spawn4;
    public Transform Spawn5;
    public Transform Spawn6;
    public Transform Spawn7;
    public Transform Spawn8;
    public Transform Spawn9;
    public Transform Spawn10;
    public Transform Spawn11;
    public Transform Spawn12;

    public int fLag = 0;

    void Update()
    {
        if (fLag == 0)
        {
            fLag = 1;
            for (int spawn = 1; spawn < 13; spawn++)
            {
                if (spawn == 1)
                {
                
                    int bigORsmall = Random.Range(0, 2);
                    if (bigORsmall == 0)
                    {
                        Instantiate(WatchSmall, Spawn1.position, Quaternion.Euler(90, 180, 0));
                       
                    }
                    if (bigORsmall == 1)
                    {
                        Instantiate(WatchBig, Spawn1.position, Quaternion.Euler(90, 180, 0));
                    }
       
                }
                if (spawn == 2)
                {
                  
                    int bigORsmall = Random.Range(0, 2);
                    if (bigORsmall == 0)
                    {
                        Instantiate(WatchSmall, Spawn2.position, Quaternion.Euler(90, 180, 0));
                    }
                    if (bigORsmall == 1)
                    {
                        Instantiate(WatchBig, Spawn2.position, Quaternion.Euler(90, 180, 0));
                    }
                }
                if (spawn == 3)
                {
                   
                    int bigORsmall = Random.Range(0, 2);
                    if (bigORsmall == 0)
                    {
                        Instantiate(WatchSmall, Spawn3.position, Quaternion.Euler(90, 180, 0));
                    }
                    if (bigORsmall == 1)
                    {
                        Instantiate(WatchBig, Spawn3.position, Quaternion.Euler(90, 180, 0));
                    }
                }
                if (spawn == 4)
                {
                  
                    int bigORsmall = Random.Range(0, 2);
                    if (bigORsmall == 0)
                    {
                        Instantiate(WatchSmall, Spawn4.position, Quaternion.Euler(90, 180, 0));
                    }
                    if (bigORsmall == 1)
                    {
                        Instantiate(WatchBig, Spawn4.position, Quaternion.Euler(90, 180, 0));
                    }
                }
                if (spawn == 5)
                {
                    
                    int bigORsmall = Random.Range(0, 2);
                    if (bigORsmall == 0)
                    {
                        Instantiate(WatchSmall, Spawn5.position, Quaternion.Euler(90, 180, 0));
                    }
                    if (bigORsmall == 1)
                    {
                        Instantiate(WatchBig, Spawn5.position, Quaternion.Euler(90, 180, 0));
                    }
                }
                if (spawn == 6)
                {
                  
                    int bigORsmall = Random.Range(0, 2);
                    if (bigORsmall == 0)
                    {
                        Instantiate(WatchSmall, Spawn6.position, Quaternion.Euler(90, 180, 0));
                    }
                    if (bigORsmall == 1)
                    {
                        Instantiate(WatchBig, Spawn6.position, Quaternion.Euler(90, 180, 0));
                    }
                }
                if (spawn == 7)
                {
                   
                    int bigORsmall = Random.Range(0, 2);
                    if (bigORsmall == 0)
                    {
                        Instantiate(WatchSmall, Spawn7.position, Quaternion.Euler(90, 180, 0));
                    }
                    if (bigORsmall == 1)
                    {
                        Instantiate(WatchBig, Spawn7.position, Quaternion.Euler(90, 180, 0));
                    }
                }
                if (spawn == 8)
                {
                   
                    int bigORsmall = Random.Range(0, 2);
                    if (bigORsmall == 0)
                    {
                        Instantiate(WatchSmall, Spawn8.position, Quaternion.Euler(90, 180, 0));
                    }
                    if (bigORsmall == 1)
                    {
                        Instantiate(WatchBig, Spawn8.position, Quaternion.Euler(90, 180, 0));
                    }
                }
                if (spawn == 9)
                {
                    
                    int bigORsmall = Random.Range(0, 2);
                    if (bigORsmall == 0)
                    {
                        Instantiate(WatchSmall, Spawn9.position, Quaternion.Euler(90, 180, 0));
                    }
                    if (bigORsmall == 1)
                    {
                        Instantiate(WatchBig, Spawn9.position, Quaternion.Euler(90, 180, 0));
                    }
                }
                if (spawn == 10)
                {
                    
                    
                        int bigORsmall = Random.Range(0, 2);
                        if (bigORsmall == 0)
                        {
                            Instantiate(WatchSmall, Spawn10.position, Quaternion.Euler(90, 180, 0));
                        }
                        if (bigORsmall == 1)
                        {
                            Instantiate(WatchBig, Spawn10.position, Quaternion.Euler(90, 180, 0));
                        }
                    
                }
                if (spawn == 11)
                {
                   
                    int bigORsmall = Random.Range(0, 2);
                    if (bigORsmall == 0)
                    {
                        Instantiate(WatchSmall, Spawn11.position, Quaternion.Euler(90, 180, 0));
                    }
                    if (bigORsmall == 1)
                    {
                        Instantiate(WatchBig, Spawn11.position, Quaternion.Euler(90,180, 0));
                    }
                }
                if (spawn == 12)
                {
                    
                    int bigORsmall = Random.Range(0, 2);
                    if (bigORsmall == 0)
                    {
                        Instantiate(WatchSmall, Spawn12.position, Quaternion.Euler(90, 180, 0));
                    }
                    if (bigORsmall == 1)
                    {
                        Instantiate(WatchBig, Spawn12.position, Quaternion.Euler(90, 180, 0));
                    }
                }
            }    
        }
    }
}
