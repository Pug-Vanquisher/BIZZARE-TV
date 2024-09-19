using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AlertTrigger : MonoBehaviour
{
    public AudioSource AlertSound;
    private GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            AlertSound.Play();
            Invoke("RespawnPlayerOnAlert", 1.5f);
        }
    }
    void RespawnPlayerOnAlert()
    {
        if (CheckpointManager.lastCheckpointPosition != Vector3.zero && player != null)
        {
            AlertSound.Stop();
            // Перемещаем игрока к последнему активированному чекпоинту
            player.transform.position = CheckpointManager.lastCheckpointPosition;
        }
        else
        {
            CheckpointManager.ResetCheckpoint();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
