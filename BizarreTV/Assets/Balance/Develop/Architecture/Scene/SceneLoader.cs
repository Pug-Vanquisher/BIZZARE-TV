using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Balance
{
    public class SceneLoader
    {
        // Ёто костыль из-за структуры проекта.
        // «апускает сцены в нужном пор€дке при дебаге данной мини игры.
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutostartGame()
        {
            string sceneName = SceneManager.GetActiveScene().name;
            if (Scenes.IsBalanceGameScene(sceneName))
            {
                new SceneLoader().LoadAndStartGameplay();
            }
        }

        // ќсновна€ точка запуска всей мини игры.
        public void LoadAndStartGameplay()
        {
            Coroutines.StartRoutine(LoadAndStartGameplayRoutine());
        }

        private IEnumerator LoadAndStartGameplayRoutine()
        {
            yield return LoadScene(Scenes.BOOT);

            BootEntryPoint bootEntryPoint = Object.FindFirstObjectByType<BootEntryPoint>();
            yield return bootEntryPoint.Run();

            yield return LoadScene(Scenes.GAMEPLAY);

            GameplayEntryPoint sceneEntryPoint = Object.FindFirstObjectByType<GameplayEntryPoint>();
            yield return sceneEntryPoint.Run();
        }

        private IEnumerator LoadScene(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
            yield return null;
        }
    }
}
