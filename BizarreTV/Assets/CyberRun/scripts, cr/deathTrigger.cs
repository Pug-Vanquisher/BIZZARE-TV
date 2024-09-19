using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class deathTrigger : MonoBehaviour
{
    public AudioSource DeathSound;
    private GameObject player; // ������ �� ������

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject; // ��������� ������ �� ������
            DeathSound.Play();
            Invoke("RespawnPlayer", 0.2f);
        }
    }

    void RespawnPlayer()
    {
        if (CheckpointManager.lastCheckpointPosition != Vector3.zero && player != null)
        {
            // ���������� ������ � ���������� ��������������� ���������
            player.transform.position = CheckpointManager.lastCheckpointPosition;
        }
        else
        {
            CheckpointManager.ResetCheckpoint();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
