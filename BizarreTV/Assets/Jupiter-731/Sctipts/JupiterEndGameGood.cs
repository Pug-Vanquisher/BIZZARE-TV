using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jupiter731
{
    public class JupiterEndGameGood : MonoBehaviour
    {

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Player")
            StartCoroutine(Exit());
        }

        IEnumerator Exit()
        {
            yield return new WaitForSecondsRealtime(2);
            SceneManager.LoadScene("MainMenuTest");
        }
    }
}
