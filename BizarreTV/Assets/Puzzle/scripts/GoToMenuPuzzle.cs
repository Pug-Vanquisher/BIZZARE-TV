using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMenuPuzzle : MonoBehaviour
{
    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenuTest");
    }
}
