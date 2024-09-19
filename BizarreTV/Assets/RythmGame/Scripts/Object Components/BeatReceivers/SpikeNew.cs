using System;
using UnityEngine;

public class SpikeNew : BeatReceiver
{
    [SerializeField] GameObject Hitbox;
    [SerializeField] int[] ActiveBeat;
    [SerializeField] Animator animator;
    void Start(){
        animator = GetComponent<Animator>();
    }

    public override void OnBeatSended(int beat){
        //animator.SetBool("isActive", (Array.FindIndex(ActiveBeat, item => item == beat) != -1));
        //if (Array.FindIndex(ActiveBeat, item => item == beat) != -1){
        //    animator.SetBool("isActive", true);
        //} else {
        //    animator.SetBool("isActive", false);
        //}
    }
}
