using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] public GameObject panel;

    public void Pause()
    {
        panel.SetActive(true);
        Time.timeScale = 0;
    }

    public void Continue()
    {
        panel.SetActive(false);
        Time.timeScale = 1;
    }

}
