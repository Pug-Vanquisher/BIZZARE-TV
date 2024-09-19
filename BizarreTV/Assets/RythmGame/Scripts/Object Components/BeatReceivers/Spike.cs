using System;
using UnityEngine;

public class Spike : BeatReceiver
{
    [SerializeField] GameObject Hitbox;
    [SerializeField] int[] ActiveBeat;
    bool currentState;
    bool previousState;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip sound;

    public override void OnBeatSended(int beat){
        currentState = IsSpikeActive(beat);
        animator.SetBool("isActive", currentState);
        SpikeSound(currentState, previousState);
        previousState = currentState;
    }

    bool IsSpikeActive(int beat){
        if (Array.FindIndex(ActiveBeat, item => item == beat) != -1){
            return true;
        } else {
            return false;
        }
    }

    void SpikeSound(bool curr, bool prev){
        if (curr == true && prev == false){
            source.clip = sound;
            source.PlayOneShot(sound, 1f);
        }
    }
}
