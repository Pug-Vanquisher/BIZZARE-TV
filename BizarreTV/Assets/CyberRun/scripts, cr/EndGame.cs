using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public GameObject win;

    private void Start()
    {
        win.SetActive(false);
    }

    public string scenenametogo;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            win.SetActive(true);
            Invoke("Menu", 2f);
        }
    }

    void Menu()
    {
        CheckpointManager.ResetCheckpoint();
        SceneManager.LoadScene(scenenametogo);
    }
}
