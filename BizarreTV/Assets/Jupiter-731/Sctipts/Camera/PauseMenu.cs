using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jupiter731
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] GameObject pauseMenu;
        private bool _isPaused = false;
        public void UnPauseWithButton()
        {
            pauseMenu.SetActive(false);
            _isPaused = false;
            Time.timeScale = 1f;
        }

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

}