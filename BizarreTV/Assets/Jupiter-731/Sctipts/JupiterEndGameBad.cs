using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jupiter731
{
    public class JupiterEndGameBad : MonoBehaviour
    {
        [SerializeField] GameObject player;
        private void FixedUpdate()
        {
            if (player == null)
            {
                var scene = SceneManager.GetActiveScene();
                var name = scene.name;
                SceneManager.UnloadSceneAsync(scene);
                SceneManager.LoadScene(name);
            }
        }

    }
}
