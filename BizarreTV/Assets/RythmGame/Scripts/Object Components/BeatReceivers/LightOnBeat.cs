using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightOnBeat : BeatReceiver
{
    Light light;
    [SerializeField] Color[] colors; 
    public void Start(){
        light = GetComponent<Light>();
    }

    public override void OnBeatSended(int beat){
        light.color = colors[(beat % 4)];
    }
}