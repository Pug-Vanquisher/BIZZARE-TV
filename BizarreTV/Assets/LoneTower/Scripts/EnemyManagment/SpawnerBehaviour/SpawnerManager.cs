using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] SpawnerEventManager m_SpawnerEventManager;
    [SerializeField] EnemySpawn[] m_Spawners;
    [SerializeField] Wave[] m_Waves;
    [SerializeField] TextMeshProUGUI m_WaveText;
    [SerializeField] GameObject winPanel;
    int waveNumber = 0;
    bool needToReload = true; 
    public void Update(){
        needToReload = true;
        for (int i = 0; i < m_Spawners.Length; i++){
            if (m_Spawners[i].canSpawn){
                needToReload = false;
            }
        }
        if (needToReload){
            if (waveNumber + 1 == m_Waves.Length) {
                winPanel.SetActive(true);
            } else {
                Timer(15.0f);
                m_SpawnerEventManager.LoadNextWave(m_Waves[waveNumber]);
                m_WaveText.text = m_Waves[waveNumber].waveName;
                waveNumber++;
                needToReload = false;
            }
        }
    }

    public void Timer(float time){
        while (time > 0){
            time = time - Time.fixedDeltaTime;
        }
    }
}
