using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WaveFile")]
public class Wave : ScriptableObject
{
    public string waveName;
    public int[] enemyCounts;
    public int spawnRate;
}