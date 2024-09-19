using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundOnBeat : BeatReceiver
{
    Camera cam;
    [SerializeField] Color[] colors; 
    public void Start(){
        cam = GetComponent<Camera>();
    }

    public override void OnBeatSended(int beat){
        cam.backgroundColor = colors[beat / 4];
    }
}
