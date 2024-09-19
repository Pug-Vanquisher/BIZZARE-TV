using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoCube : BeatReceiver
{
    [SerializeField] Color[] colors; 
    [SerializeField] MeshRenderer _mesh;
    [SerializeField] GameObject cube;
    public void Start(){
        _mesh = cube.GetComponent<MeshRenderer>();   
    }
    public void FixedUpdate(){
        transform.Rotate(new Vector3(1.05f, 2.15f, 1.5f) * 0.2f);
    }
    public override void OnBeatSended(int beat){
        _mesh.material.color = colors[beat % 4];
    }
}
