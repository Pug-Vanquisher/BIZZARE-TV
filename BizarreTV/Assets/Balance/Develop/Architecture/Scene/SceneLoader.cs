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
                new SceneLoader().LoadBoot();
            }
        }

        public void LoadBoot()
        {
            Coroutines.StopAllRoutines();
            Coroutines.StartRoutine(
                LoadScene<BootEntryPoint>(Scenes.BOOT));
        }

        public void LoadGameplay()
        {
            Coroutines.StopAllRoutines();
            Coroutines.StartRoutine(
                LoadScene<GameplayEntryPoint>(Scenes.GAMEPLAY));
        }

        public void LoadLevelList()
        {
            Coroutines.StopAllRoutines();
            Coroutines.StartRoutine(
                LoadScene<LevelListEntryPoint>(Scenes.LEVEL_LIST));
        }

        private IEnumerator LoadScene<T>(string sceneName) where T : EntryPoint
        {
            //yield return _uiRoot.ShowLoadingScreen();

            yield return SceneManager.LoadSceneAsync(sceneName);

            EntryPoint sceneEntryPoint = Object.FindFirstObjectByType<T>();
            yield return sceneEntryPoint.Run();

            //yield return _uiRoot.HideLoadingScreen();
        }
    }
}
