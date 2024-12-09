using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Jupiter731
{
    public class JupiterEndGameBad : MonoBehaviour
    {
        [SerializeField] BaseUnit player;
        [SerializeField] Timer timer;
        private void Awake()
        {
            Subscribe();
        }

        void Subscribe() { player.Die += UnitDeath;  }

        void Unsubscribe() { player.Die -= UnitDeath; }

        void UnitDeath(GameObject diedUnit)
        {
            if (player.PublicHP <= 0 || timer.CurrTime <= 0)
            {
                Unsubscribe();
                var scene = SceneManager.GetActiveScene();
                var name = scene.name;
                SceneManager.UnloadSceneAsync(scene);
                SceneManager.LoadScene(name);
            }
        }

    }
}
