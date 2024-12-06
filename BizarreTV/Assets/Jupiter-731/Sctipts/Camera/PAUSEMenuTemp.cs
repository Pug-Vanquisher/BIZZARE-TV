using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAUSEMenuTemp : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    private bool _isPaused = false;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !_isPaused)
        {
            pauseMenu.SetActive(true);
            _isPaused = true;
            Time.timeScale = 0f;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && _isPaused)
        {
            pauseMenu.SetActive(false);
            _isPaused = false;
            Time.timeScale = 1f;
        }
    }
}
