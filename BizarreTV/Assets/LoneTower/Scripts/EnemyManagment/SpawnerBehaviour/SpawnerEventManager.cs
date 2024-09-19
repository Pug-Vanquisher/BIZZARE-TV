using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Manager/Spawner Manager")]
public class SpawnerEventManager : ScriptableObject {
    public delegate void WaveCallBack(Wave wave);

    public WaveCallBack OnWaveChanged;

    public void LoadNextWave(Wave wave){
        OnWaveChanged.Invoke(wave);
    }
}
