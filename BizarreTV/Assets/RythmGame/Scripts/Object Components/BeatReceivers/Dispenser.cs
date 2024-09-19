using System;
using UnityEngine;

public class Dispenser : BeatReceiver
{
    [SerializeField] GameObject Hitbox;
    [SerializeField] int[] ActiveBeat;
    bool currentState;
    bool previousState;
    [SerializeField] Animator animator;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip sound;
    public override void OnBeatSended(int beat){
        currentState = IsLazerActive(beat);
        animator.SetBool("isActive", currentState);
        LazerSound(currentState, previousState);
        previousState = currentState;
    }

    bool IsLazerActive(int beat){
        if (Array.FindIndex(ActiveBeat, item => item == beat) != -1){
            return true;
        } else {
            return false;
        }
    }

    void LazerSound(bool curr, bool prev){
        if (curr == true && prev == false){
            source.clip = sound;
            source.PlayOneShot(sound, 1f);
        }
        if (curr == false && prev == true){
            source.Stop();
        }
    }
}

