using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Manager/Event Manager")]
public class EventManager : ScriptableObject{
    public delegate void TrackCallBack(Track track);
    public delegate void DifficultyChangingCallBack(string difficulty);
    public delegate void LevelIntCallBack(int scene);
    public delegate void LevelStringCallBack(string scene);
    public delegate void RythmCallBack(int beat);
    public delegate void CheckpointCallBack(Vector3 position);
    public delegate void RespawnCallBack();
    public delegate void SoundEffectCallBack(AudioClip sound, bool isPitchable);
    public delegate void PausingCallBack(bool isPaused);

    public TrackCallBack OnTrackStarted;
    public DifficultyChangingCallBack OnDifficultyChanged;
    public LevelIntCallBack OnLevelIntChanged;
    public LevelStringCallBack OnLevelStringChanged;
    public RythmCallBack OnBeatSended;
    public CheckpointCallBack OnCheckpointOverride;
    public RespawnCallBack OnRespawn;
    public SoundEffectCallBack OnSoundEffectPlayed;
    public PausingCallBack OnGamePaused;
    public void StartTrack(Track track){
        OnTrackStarted.Invoke(track);
    }
    public void ChangeDifficulty(string difficulty){
        OnDifficultyChanged.Invoke(difficulty);
    }
    public void ChangeIntLevel(int scene){
        OnLevelIntChanged.Invoke(scene);
    }
    public void ChangeStringLevel(string scene){
        OnLevelStringChanged.Invoke(scene);
    }
    public void SendBeat(int beat){
        OnBeatSended.Invoke(beat);
    }
    public void OverrideCheckpoint(Vector3 position){
        OnCheckpointOverride.Invoke(position);
    }
    public void Respawn(){
        OnRespawn.Invoke();
    }
    public void PlaySoundEffect(AudioClip sound, bool isPitchable){
        OnSoundEffectPlayed.Invoke(sound, isPitchable);
    }
    public void PauseGame(bool isPaused){
        OnGamePaused.Invoke(isPaused);
    }
}
