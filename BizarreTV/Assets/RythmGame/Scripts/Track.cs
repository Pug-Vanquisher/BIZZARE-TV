using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "TrackFile")]
public class Track : ScriptableObject
{
    public int BPM;
    public int BeatsPerBar;
    public float songDelay;
    public AudioClip AudioClip;
}

